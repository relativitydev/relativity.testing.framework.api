using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Querying;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(IWorkspaceFillRequiredPropertiesStrategy))]
	public class WorkspaceFillRequiredPropertiesStrategyFixture
	{
		private WorkspaceFillRequiredPropertiesStrategy _workspaceFillRequiredPropertiesStrategy;
		private Mock<IGetByNameStrategy<Workspace>> _mockGetWorkspaceByNameStrategy;
		private Mock<IGetAllStrategy<Workspace>> _mockGetAllWorkspacesStrategy;
		private Mock<IGetAllByNamesStrategy<Group>> _mockGetGroupsByNameStrategy;
		private Mock<IGetByNameStrategy<Client>> _mockGetClientByNameStrategy;
		private Mock<IMatterGetByNameAndClientIdStrategy> _mockMatterGetByNameAndClientIdStrategy;
		private Mock<IObjectService> _mockObjectService;

		[OneTimeSetUp]
		public void SetUp()
		{
			_mockGetWorkspaceByNameStrategy = new Mock<IGetByNameStrategy<Workspace>>();
			_mockGetAllWorkspacesStrategy = new Mock<IGetAllStrategy<Workspace>>();
			_mockGetGroupsByNameStrategy = new Mock<IGetAllByNamesStrategy<Group>>();
			_mockGetClientByNameStrategy = new Mock<IGetByNameStrategy<Client>>();
			_mockMatterGetByNameAndClientIdStrategy = new Mock<IMatterGetByNameAndClientIdStrategy>();
			_mockObjectService = new Mock<IObjectService>();

			_workspaceFillRequiredPropertiesStrategy = new WorkspaceFillRequiredPropertiesStrategy(_mockGetWorkspaceByNameStrategy.Object, _mockGetAllWorkspacesStrategy.Object, _mockGetGroupsByNameStrategy.Object, _mockGetClientByNameStrategy.Object, _mockMatterGetByNameAndClientIdStrategy.Object, _mockObjectService.Object);
		}

		[SetUp]
		public void SetDefaultMocks()
		{
			_mockGetAllWorkspacesStrategy.Setup(x => x.GetAll())
				.Returns(new[]
				{
					new Workspace
					{
						ArtifactID = 1,
						Name = "New Case Template",
						Status = "Active"
					}
				});

			var mockResourcePoolQueryExecutor = new Mock<IQueryExecutor<ResourcePool>>();
			_mockObjectService.Setup(x => x.Query<ResourcePool>())
				.Returns(new ObjectQuery<ResourcePool>(new ObjectQueryRequest(123), mockResourcePoolQueryExecutor.Object));
			mockResourcePoolQueryExecutor.Setup(x => x.Execute(It.IsAny<IQueryRequest>()))
				.Returns(new List<ResourcePool>
				{
					new ResourcePool
					{
						ArtifactID = 1001,
						Name = "Resource Pool",
						CacheLocationServers = new List<NamedArtifact>
						{
							new NamedArtifact
							{
								ArtifactID = 1011,
								Name = "Cache Location"
							}
						},
						FileRepositories = new List<NamedArtifact>
						{
							new NamedArtifact
							{
								ArtifactID = 1021,
								Name = "File Repository"
							}
						},
						SqlServers = new List<NamedArtifact>
						{
							new NamedArtifact
							{
								ArtifactID = 1031,
								Name = "SQL Server"
							}
						}
					}
				});

			var mockMatterQueryExecutor = new Mock<IQueryExecutor<Matter>>();
			_mockObjectService.Setup(x => x.Query<Matter>())
				.Returns(new ObjectQuery<Matter>(new ObjectQueryRequest(456), mockMatterQueryExecutor.Object));
			mockMatterQueryExecutor.Setup(x => x.Execute(It.IsAny<IQueryRequest>()))
				.Returns(new[]
				{
					new Matter
					{
						ArtifactID = 1,
						Name = "Sample Matter"
					}
				});
		}

		[Test]
		public void FillRequiredProperties_Name_Fills_IfNotSpecified()
		{
			Workspace workspace = new Workspace();

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.Name.Should().NotBeNullOrWhiteSpace();
		}

		[Test]
		public void FillRequiredProperties_Name_DoesNotChange_IfSpecified()
		{
			string workspaceName = "Baby's First Workspace";

			Workspace workspace = new Workspace
			{
				Name = workspaceName
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.Name.Should().Be(workspaceName);
		}

		[Test]
		public void FillRequiredProperties_SqlFullTextLanguage_Fills_IfNotSpecified()
		{
			Workspace workspace = new Workspace();

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.SqlFullTextLanguage.Should().NotBeNull();
			workspace.SqlFullTextLanguage.ArtifactID.Should().Be(1033);
		}

		[Test]
		public void FillRequiredProperties_SqlFullTextLanguage_DoesNotChange_IfSpecified()
		{
			Workspace workspace = new Workspace
			{
				SqlFullTextLanguage = new NamedArtifact
				{
					ArtifactID = 1045
				}
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.SqlFullTextLanguage.Should().NotBeNull();
			workspace.SqlFullTextLanguage.ArtifactID.Should().Be(1045);
		}

		[Test]
		public void FillRequiredProperties_DownloadHandlerUrl_Fills_IfNotSpecified()
		{
			Workspace workspace = new Workspace();

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.DownloadHandlerUrl.Should().Be("Relativity.Distributed");
		}

		[Test]
		public void FillRequiredProperties_DownloadHandlerUrl_DoesNotChange_IfSpecified()
		{
			Workspace workspace = new Workspace
			{
				DownloadHandlerUrl = "Example.Handler"
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.DownloadHandlerUrl.Should().Be("Example.Handler");
		}

		[TestCase("Base Template")]
		[TestCase("New Case Template")]
		public void FillRequiredProperties_TemplateWorkspace_Fills_IfNotSpecified(string workspaceName)
		{
			_mockGetAllWorkspacesStrategy.Setup(x => x.GetAll())
				.Returns(new[]
				{
					new Workspace
					{
						ArtifactID = 1,
						Name = "Workspace 1",
						Status = "Active"
					},
					new Workspace
					{
						ArtifactID = 2,
						Name = workspaceName,
						Status = "Active"
					}
				});

			Workspace workspace = new Workspace();

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.TemplateWorkspace.Should().NotBeNull();
			workspace.TemplateWorkspace.ArtifactID.Should().Be(2);
			workspace.TemplateWorkspace.Name.Should().Be(workspaceName);
		}

		[Test]
		public void FillRequiredProperties_TemplateWorkspace_Fills_IfDefaultTemplatesDoNotExist()
		{
			_mockGetAllWorkspacesStrategy.Setup(x => x.GetAll())
				.Returns(new[]
				{
					new Workspace
					{
						ArtifactID = 1,
						Name = "Workspace 1",
						Status = "Active"
					}
				});

			Workspace workspace = new Workspace();

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.TemplateWorkspace.Should().NotBeNull();
			workspace.TemplateWorkspace.ArtifactID.Should().Be(1);
			workspace.TemplateWorkspace.Name.Should().Be("Workspace 1");
		}

		[Test]
		public void FillRequiredProperties_TemplateWorkspace_LooksUp_IfOnlyNameSpecified()
		{
			string workspaceName = "My Workspace Template";

			Workspace workspace = new Workspace
			{
				TemplateWorkspace = new NamedArtifact
				{
					Name = workspaceName
				}
			};

			_mockGetWorkspaceByNameStrategy.Setup(x => x.Get(workspaceName))
				.Returns(new Workspace
				{
					ArtifactID = 1,
					Name = workspaceName
				});

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.TemplateWorkspace.Should().NotBeNull();
			workspace.TemplateWorkspace.ArtifactID.Should().Be(1);
			workspace.TemplateWorkspace.Name.Should().Be(workspaceName);
		}

		[Test]
		public void FillRequiredProperties_TemplateWorkspace_DoesNotChange_IfArtifactIDSpecified()
		{
			NamedArtifact originalTemplateWorkspace = new NamedArtifact
			{
				ArtifactID = 1,
				Name = "My Workspace Template"
			};

			Workspace workspace = new Workspace
			{
				TemplateWorkspace = originalTemplateWorkspace
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.TemplateWorkspace.Should().BeEquivalentTo(originalTemplateWorkspace);
		}

		[Test]
		public void FillRequiredProperties_Matter_Fills_IfNotSpecified()
		{
			Workspace workspace = new Workspace();

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.Matter.Should().NotBeNull();
			workspace.Matter.ArtifactID.Should().Be(1);
			workspace.Matter.Name.Should().Be("Sample Matter");
		}

		[Test]
		public void FillRequiredProperties_Matter_LooksUpByName_IfClientSpecifiedByID()
		{
			_mockMatterGetByNameAndClientIdStrategy.Setup(x => x.Get("My Matter", 123))
				.Returns(new Matter
				{
					ArtifactID = 1,
					Name = "My Matter"
				});

			Workspace workspace = new Workspace
			{
				Client = new NamedArtifact
				{
					ArtifactID = 123
				},
				Matter = new NamedArtifact
				{
					Name = "My Matter"
				},
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.Matter.Should().NotBeNull();
			workspace.Matter.ArtifactID.Should().Be(1);
			workspace.Matter.Name.Should().Be("My Matter");
		}

		[Test]
		public void FillRequiredProperties_Matter_LooksUpByName_IfClientSpecifiedByName()
		{
			_mockGetClientByNameStrategy.Setup(x => x.Get("My Client"))
				.Returns(new Client
				{
					ArtifactID = 123,
					Name = "My Client"
				});

			_mockMatterGetByNameAndClientIdStrategy.Setup(x => x.Get("My Matter", 123))
				.Returns(new Matter
				{
					ArtifactID = 1,
					Name = "My Matter"
				});

			Workspace workspace = new Workspace
			{
				Client = new NamedArtifact
				{
					Name = "My Client"
				},
				Matter = new NamedArtifact
				{
					Name = "My Matter"
				},
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.Matter.Should().NotBeNull();
			workspace.Matter.ArtifactID.Should().Be(1);
			workspace.Matter.Name.Should().Be("My Matter");
		}

		[Test]
		public void FillRequiredProperties_WorkspaceAdminGroup_DoesNotFill_IfBlank()
		{
			Workspace workspace = new Workspace();

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.WorkspaceAdminGroup.Should().BeNull();
		}

		[Test]
		public void FillRequiredProperties_WorkspaceAdminGroup_LooksUp_IfNameSpecified()
		{
			_mockGetGroupsByNameStrategy.Setup(x => x.GetAll(It.Is<IEnumerable<string>>(e => e.Contains("My Group"))))
				.Returns(new[]
				{
					new Group
					{
						ArtifactID = 1,
						Name = "My Group"
					}
				});

			Workspace workspace = new Workspace
			{
				WorkspaceAdminGroup = new Group
				{
					Name = "My Group"
				}
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.WorkspaceAdminGroup.Should().NotBeNull();
			workspace.WorkspaceAdminGroup.ArtifactID.Should().Be(1);
			workspace.WorkspaceAdminGroup.Name.Should().Be("My Group");
		}

		[Test]
		public void FillRequiredProperties_WorkspaceAdminGroup_DoesNotChange_IfArtifactIDSpecified()
		{
			var group = new Group
			{
				ArtifactID = 1
			};

			Workspace workspace = new Workspace
			{
				WorkspaceAdminGroup = group
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.WorkspaceAdminGroup.Should().BeEquivalentTo(group);
		}

		[Test]
		public void FillRequiredProperties_ResourcePool_Fills_IfNotSpecified()
		{
			Workspace workspace = new Workspace();

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.ResourcePool.Should().NotBeNull();
			workspace.ResourcePool.ArtifactID.Should().Be(1001);
			workspace.ResourcePool.Name.Should().Be("Resource Pool");
		}

		[Test]
		public void FillRequiredProperties_ResourcePool_LooksUp_IfArtifactIDSpecified()
		{
			var resourcePool = new NamedArtifact
			{
				ArtifactID = 1001
			};

			Workspace workspace = new Workspace
			{
				ResourcePool = resourcePool
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.ResourcePool.ArtifactID.Should().Be(resourcePool.ArtifactID);
			workspace.ResourcePool.Name.Should().Be("Resource Pool");
		}

		[Test]
		public void FillRequiredProperties_ResourcePool_LooksUp_IfNameSpecified()
		{
			var resourcePool = new NamedArtifact
			{
				Name = "Resource Pool"
			};

			Workspace workspace = new Workspace
			{
				ResourcePool = resourcePool
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.ResourcePool.Name.Should().Be(resourcePool.Name);
			workspace.ResourcePool.ArtifactID.Should().Be(1001);
		}

		[Test]
		public void FillRequiredProperties_DefaultCacheLocation_LooksUpFromResourcePool_IfNotSpecified()
		{
			var resourcePool = new NamedArtifact
			{
				ArtifactID = 1001
			};

			Workspace workspace = new Workspace
			{
				ResourcePool = resourcePool
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.DefaultCacheLocation.Should().NotBeNull();
			workspace.DefaultCacheLocation.ArtifactID.Should().Be(1011);
			workspace.DefaultCacheLocation.Name.Should().Be("Cache Location");
		}

		[Test]
		public void FillRequiredProperties_DefaultCacheLocation_DoesNotChange_IfSpecified()
		{
			var resourcePool = new NamedArtifact
			{
				ArtifactID = 1001
			};

			var defaultCacheLocation = new NamedArtifact
			{
				ArtifactID = 1234,
				Name = "My Cache Location"
			};

			Workspace workspace = new Workspace
			{
				ResourcePool = resourcePool,
				DefaultCacheLocation = defaultCacheLocation
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.DefaultCacheLocation.Should().BeEquivalentTo(defaultCacheLocation);
		}

		[Test]
		public void FillRequiredProperties_DefaultFileRepository_LooksUpFromResourcePool_IfNotSpecified()
		{
			var resourcePool = new NamedArtifact
			{
				ArtifactID = 1001
			};

			Workspace workspace = new Workspace
			{
				ResourcePool = resourcePool
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.DefaultFileRepository.Should().NotBeNull();
			workspace.DefaultFileRepository.ArtifactID.Should().Be(1021);
			workspace.DefaultFileRepository.Name.Should().Be("File Repository");
		}

		[Test]
		public void FillRequiredProperties_DefaultFileRepository_DoesNotChange_IfSpecified()
		{
			var resourcePool = new NamedArtifact
			{
				ArtifactID = 1001
			};

			var fileRepository = new NamedArtifact
			{
				ArtifactID = 1234,
				Name = "My File Repository"
			};

			Workspace workspace = new Workspace
			{
				ResourcePool = resourcePool,
				DefaultFileRepository = fileRepository
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.DefaultFileRepository.Should().BeEquivalentTo(fileRepository);
		}

		[Test]
		public void FillRequiredProperties_SqlServer_LooksUpFromResourcePool_IfNotSpecified()
		{
			var resourcePool = new NamedArtifact
			{
				ArtifactID = 1001
			};

			Workspace workspace = new Workspace
			{
				ResourcePool = resourcePool
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.SqlServer.Should().NotBeNull();
			workspace.SqlServer.ArtifactID.Should().Be(1031);
			workspace.SqlServer.Name.Should().Be("SQL Server");
		}

		[Test]
		public void FillRequiredProperties_SqlServer_DoesNotChange_IfSpecified()
		{
			var resourcePool = new NamedArtifact
			{
				ArtifactID = 1001
			};

			var sqlServer = new NamedArtifact
			{
				ArtifactID = 1234,
				Name = "My SQL Server"
			};

			Workspace workspace = new Workspace
			{
				ResourcePool = resourcePool,
				SqlServer = sqlServer
			};

			_workspaceFillRequiredPropertiesStrategy.FillRequiredProperties(workspace);

			workspace.SqlServer.Should().BeEquivalentTo(sqlServer);
		}
	}
}
