using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(TabGetAllForNavigationStrategyV1))]
	public class TabGetAllForNavigationStrategyV1Fixture
	{
		private const int _WORKSPACE_ID = 12345;
		private TabGetAllForNavigationStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IWorkspaceIdValidator> _mockWorkspaceIdValidator;
		private TabNavigationResponseV1 _mockTabNavigationResponse;

		[SetUp]
		public void SetUp()
		{
			_mockTabNavigationResponse = new TabNavigationResponseV1
			{
				ObjectIdentifier = new NamedArtifact
				{
					Name = "Workspaces",
					ArtifactID = 1
				},
				Parent = new Securable<NamedArtifact>
				{
					Secured = false,
					Value = new NamedArtifact
					{
						ArtifactID = 2
					}
				},
				Order = -1000
			};

			_mockRestService = new Mock<IRestService>();
			_mockRestService.Setup(x => x.Get<List<TabNavigationResponseV1>>(It.IsAny<string>(), null)).Returns(new List<TabNavigationResponseV1> { _mockTabNavigationResponse });
			_mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();
			_sut = new TabGetAllForNavigationStrategyV1(_mockRestService.Object, _mockWorkspaceIdValidator.Object);
		}

		[Test]
		public void Get_WithAnyParameters_ShouldCallWorkspaceIdValidator()
		{
			_sut.Get(_WORKSPACE_ID);
			_mockWorkspaceIdValidator.Verify(validator => validator.Validate(_WORKSPACE_ID), Times.Once);
		}
	}
	}
