using System;
using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal abstract class WorkspaceCreateAbstractStrategy : CreateStrategy<Workspace>
	{
		private const string DefaultTemplateWorkspaceName = "New Case Template";

		private readonly IGetByIdStrategy<Workspace> _getWorkspaceByIdStrategy;

		private readonly IGetByNameStrategy<Workspace> _getWorkspaceByNameStrategy;

		private readonly IGetAllStrategy<Workspace> _getAllWorkspacesStrategy;

		private readonly IGetAllByNamesStrategy<Group> _getGroupsByNameStrategy;

		private readonly IGetByNameStrategy<Client> _getClientByNameStrategy;

		private readonly IMatterGetByNameAndClientIdStrategy _matterGetByNameAndClientIdStrategy;

		private readonly IGetAllStrategy<ResourceServer> _resourceServerGetAllStrategy;

		private readonly IGetAllStrategy<ResourcePool> _getAllResourcePoolsStrategy;

		private readonly IObjectService _objectService;

		protected WorkspaceCreateAbstractStrategy(
			IGetByNameStrategy<Workspace> getWorkspaceByNameStrategy,
			IGetByIdStrategy<Workspace> getWorkspaceByIdStrategy,
			IGetAllStrategy<Workspace> getAllWorkspacesStrategy,
			IGetAllByNamesStrategy<Group> getGroupsByNameStrategy,
			IGetByNameStrategy<Client> getClientByNameStrategy,
			IMatterGetByNameAndClientIdStrategy matterGetByNameAndClientIdStrategy,
			IGetAllStrategy<ResourceServer> resourceServerGetAllStrategy,
			IGetAllStrategy<ResourcePool> getAllResourcePoolsStrategy,
			IObjectService objectService)
		{
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

		protected override Workspace DoCreate(Workspace entity)
		{
			object workspaceToCreate = ResolveMissingFields(entity);

			int workspaceId = CreateWorkspace(workspaceToCreate);

			return _getWorkspaceByIdStrategy.Get(workspaceId)
				?? throw ObjectNotFoundException.CreateForNotFoundById<Workspace>(workspaceId);
		}

		protected abstract int CreateWorkspace(object workspaceToCreate);

		protected virtual int ResolveTemplateWorkspaceId(NamedArtifact templateWorkspace)
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

		protected virtual object ResolveMissingFields(Workspace entity)
		{
			int templateWorkspaceId = ResolveTemplateWorkspaceId(entity.TemplateWorkspace);

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

			return BuildRequest(entity, matterId, templateWorkspaceId);
		}

		private object BuildRequest(Workspace entity, int matterId, int templateWorkspaceId)
		{
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
					ResourcePool = new Securable<NamedArtifact>(entity.ResourcePool),
					SqlFullTextLanguage = entity.SqlFullTextLanguage == 0 ? 1033 : (int)entity.SqlFullTextLanguage,
					SqlServer = new Securable<Artifact>(entity.SqlServer),
					Status = new Artifact(675),
					Template = new Securable<Artifact>(new Artifact(templateWorkspaceId)),
					WorkspaceAdminGroup = entity.WorkspaceAdminGroup == null ? null : new Securable<Artifact>(entity.WorkspaceAdminGroup)
				}
			};
		}

		protected virtual Group ResolveGroup(Group group)
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

		protected virtual NamedArtifact ResolveClient(NamedArtifact client)
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

		protected virtual int ResolveMatterID(NamedArtifact matter, int? clientID)
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

		protected virtual int ResolveMatterIDForBlankMatter(int? clientID)
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
