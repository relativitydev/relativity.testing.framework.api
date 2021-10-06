using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class WorkspaceCreateStrategyV1 : WorkspaceCreateAbstractStrategy
	{
		private const string DefaultTemplateWorkspaceName = "New Case Template";

		private readonly IRestService _restService;

		private readonly IGetByIdStrategy<Workspace> _getWorkspaceByIdStrategy;

		private readonly IGetByNameStrategy<Workspace> _getWorkspaceByNameStrategy;

		private readonly IGetAllStrategy<Workspace> _getAllWorkspacesStrategy;

		private readonly IGetAllByNamesStrategy<Group> _getGroupsByNameStrategy;

		private readonly IGetByNameStrategy<Client> _getClientByNameStrategy;

		private readonly IMatterGetByNameAndClientIdStrategy _matterGetByNameAndClientIdStrategy;

		private readonly IGetAllStrategy<ResourceServer> _resourceServerGetAllStrategy;

		private readonly IGetAllStrategy<ResourcePool> _getAllResourcePoolsStrategy;

		private readonly IObjectService _objectService;

#pragma warning disable S107 // Methods should not have too many parameters
		public WorkspaceCreateStrategyV1(
			IRestService restService,
			IGetByNameStrategy<Workspace> getWorkspaceByNameStrategy,
			IGetByIdStrategy<Workspace> getWorkspaceByIdStrategy,
			IGetAllStrategy<Workspace> getAllWorkspacesStrategy,
			IGetAllByNamesStrategy<Group> getGroupsByNameStrategy,
			IGetByNameStrategy<Client> getClientByNameStrategy,
			IMatterGetByNameAndClientIdStrategy matterGetByNameAndClientIdStrategy,
			IGetAllStrategy<ResourceServer> resourceServerGetAllStrategy,
			IGetAllStrategy<ResourcePool> getAllResourcePoolsStrategy,
			IObjectService objectService)
			: base(getWorkspaceByNameStrategy, getWorkspaceByIdStrategy, getAllWorkspacesStrategy, getGroupsByNameStrategy, getClientByNameStrategy, matterGetByNameAndClientIdStrategy, resourceServerGetAllStrategy, getAllResourcePoolsStrategy, objectService)
		{
			_restService = restService;
			_getWorkspaceByNameStrategy = getWorkspaceByNameStrategy;
			_getWorkspaceByIdStrategy = getWorkspaceByIdStrategy;
			_getAllWorkspacesStrategy = getAllWorkspacesStrategy;
			_getGroupsByNameStrategy = getGroupsByNameStrategy;
			_getClientByNameStrategy = getClientByNameStrategy;
			_matterGetByNameAndClientIdStrategy = matterGetByNameAndClientIdStrategy;
			_resourceServerGetAllStrategy = resourceServerGetAllStrategy;
			_getAllResourcePoolsStrategy = getAllResourcePoolsStrategy;
			_objectService = objectService;
		}

		protected override int CreateWorkspace(object workspaceToCreate)
		{
			int templateWorkspaceId = ResolveTemplateWorkspaceId(entity.TemplateWorkspace);

			object workspaceToCreate = ConvertToDto(entity, templateWorkspaceId);

			int workspaceId = CreateWorkspace(workspaceToCreate);

			return _getWorkspaceByIdStrategy.Get(workspaceId)
				?? throw ObjectNotFoundException.CreateForNotFoundById<Workspace>(workspaceId);
		}

		private int CreateWorkspace(object workspaceToCreate)
		{
			var result = _restService.Post<JObject>("relativity-environment/V1/workspace", workspaceToCreate, 5);

			return int.Parse(result[nameof(Artifact.ArtifactID)].ToString());
		}

		private int ResolveTemplateWorkspaceId(NamedArtifact templateWorkspace)
		{
			if (templateWorkspace == null)
			{
				var workspaces = _getAllWorkspacesStrategy.GetAll();

				if (!workspaces.Any())
				{
					throw new Exception("There are no workspaces in the environment.");
				}

				return workspaces.Any(x => x.Name == DefaultTemplateWorkspaceName)
					? workspaces.First(x => x.Name == DefaultTemplateWorkspaceName).ArtifactID
					: workspaces.First(x => x.Status == "Active").ArtifactID;
			}
			else if (templateWorkspace.ArtifactID > 0)
			{
				return templateWorkspace.ArtifactID;
			}
			else if (templateWorkspace.Name != null)
			{
				return ResolveWorkspaceIdByName(templateWorkspace.Name);
			}
			else
			{
				throw new InvalidOperationException("Failed to create a workspace as template workspace is not specified.");
			}
		}

		private int ResolveWorkspaceIdByName(string name)
		{
			Workspace workspace = _getWorkspaceByNameStrategy.Get(name);

			return workspace != null
				? workspace.ArtifactID
				: throw ObjectNotFoundException.CreateForNotFoundByName<Workspace>(name);
		}

		private object ConvertToDto(Workspace entity, int templateWorkspaceId)
		{
			entity.Client = ResolveClient(entity.Client);

			int matterId = ResolveMatterID(entity.Matter, entity.Client?.ArtifactID);

			var servers = _resourceServerGetAllStrategy.GetAll();

			if (entity.WorkspaceAdminGroup != null)
			{
				entity.WorkspaceAdminGroup = ResolveGroup(entity.WorkspaceAdminGroup);
			}

			if (entity.ResourcePool == null)
			{
				entity.ResourcePool = _getAllResourcePoolsStrategy.GetAll().FirstOrDefault();
			}

			if (entity.DefaultCacheLocation == null)
			{
				entity.DefaultCacheLocation = servers.First(x => x.Type == ResourceServerType.CacheLocation);
			}

			if (entity.DefaultFileRepository == null)
			{
				entity.DefaultFileRepository = servers.First(x => x.Type == ResourceServerType.FileShare);
			}

			if (entity.SqlServer == null)
			{
				var resourcePool = _objectService.GetAll<ResourcePool>().FirstOrDefault(x => x.Name == entity.ResourcePool.Name);
				entity.SqlServer = resourcePool.SqlServers.FirstOrDefault();
			}

			return new
			{
				WorkspaceRequest = new
				{
					Name = entity.Name ?? Randomizer.GetString("AT_"),
					Matter = new Securable<Artifact>(new Artifact(matterId)),
					DefaultCacheLocation = new Securable<Artifact>(entity.DefaultCacheLocation),
					DefaultFileRepository = new Securable<Artifact>(entity.DefaultFileRepository),
					DownloadHandlerUrl = entity.DownloadHandlerUrl ?? "Relativity.Distributed",
					EnableDataGrid = false,
					entity.Keywords,
					entity.Notes,
					ResourcePool = new Securable<NamedArtifact>
					{
						Secured = false,
						Value = new NamedArtifact
						{
							ArtifactID = entity.ResourcePool.ArtifactID,
							Name = entity.ResourcePool.Name
						}
					},
					SqlFullTextLanguage = entity.SqlFullTextLanguage == 0 ? 1033 : (int)entity.SqlFullTextLanguage,
					SqlServer = new Securable<Artifact>(entity.SqlServer),
					Status = new Artifact(675),
					Template = new Securable<Artifact>(new Artifact(templateWorkspaceId)),
					WorkspaceAdminGroup = entity.WorkspaceAdminGroup == null ? null : new Securable<Artifact>(entity.WorkspaceAdminGroup)
				}
			};
		}

		private Group ResolveGroup(Group group)
		{
			if (group?.ArtifactID > 0)
			{
				return group;
			}
			else if (group?.Name != null)
			{
				Group gotGroup = _getGroupsByNameStrategy.GetAll(new List<string> { group.Name }).FirstOrDefault();

				return gotGroup ?? throw ObjectNotFoundException.CreateForNotFoundByName<Group>(group.Name);
			}
			else
			{
				return null;
			}
		}

		private NamedArtifact ResolveClient(NamedArtifact client)
		{
			if (client?.ArtifactID > 0)
			{
				return client;
			}
			else if (client?.Name != null)
			{
				Client gotClient = _getClientByNameStrategy.Get(client.Name);

				return gotClient ?? throw ObjectNotFoundException.CreateForNotFoundByName<Client>(client.Name);
			}
			else
			{
				return null;
			}
		}

		private int ResolveMatterID(NamedArtifact matter, int? clientID)
		{
			int matterID;

			if (matter?.ArtifactID > 0)
			{
				matterID = matter.ArtifactID;
			}
			else if (matter?.Name != null)
			{
				if (clientID is null)
				{
					throw new InvalidOperationException("Cannot resolve matter as client ID is not specified.");
				}

				Matter gotMatter = _matterGetByNameAndClientIdStrategy.Get(matter.Name, clientID.Value);

				matterID = gotMatter != null
					? gotMatter.ArtifactID
					: throw ObjectNotFoundException.CreateForNotFoundByName<Matter>(matter.Name);
			}
			else
			{
				matterID = ResolveMatterIDForBlankMatter(clientID);
			}

			return matterID;
		}

		private int ResolveMatterIDForBlankMatter(int? clientID)
		{
			Matter matter = null;

			if (clientID.HasValue)
			{
				matter = _objectService.Query<Matter>().Where(x => x.Client, clientID.Value).FirstOrDefault();
			}

			if (matter == null)
			{
				matter = _objectService.Query<Matter>().Where(x => x.Name, "Relativity Template").FirstOrDefault();
			}

			if (matter == null)
			{
				matter = _objectService.Query<Matter>().Where(x => x.Name, "Sample Matter").FirstOrDefault();
			}

			if (matter == null)
			{
				matter = _objectService.Query<Matter>().FirstOrDefault();
			}

			if (matter == null)
			{
				throw new InvalidOperationException("No matter available.");
			}

			return matter.ArtifactID;
		}
	}
}
