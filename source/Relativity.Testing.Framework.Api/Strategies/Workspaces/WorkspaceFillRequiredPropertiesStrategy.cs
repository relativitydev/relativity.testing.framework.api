using System;
using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceFillRequiredPropertiesStrategy : IWorkspaceFillRequiredPropertiesStrategy
	{
		private readonly IGetByNameStrategy<Workspace> _getWorkspaceByNameStrategy;
		private readonly IGetAllStrategy<Workspace> _getAllWorkspacesStrategy;
		private readonly IGetAllByNamesStrategy<Group> _getGroupsByNameStrategy;
		private readonly IGetByNameStrategy<Client> _getClientByNameStrategy;
		private readonly IMatterGetByNameAndClientIdStrategy _matterGetByNameAndClientIdStrategy;
		private readonly IObjectService _objectService;

		private readonly string[] _defaultTemplateWorkspaceNames = { "New Case Template", "Base Template" };

		public WorkspaceFillRequiredPropertiesStrategy(
			IGetByNameStrategy<Workspace> getWorkspaceByNameStrategy,
			IGetAllStrategy<Workspace> getAllWorkspacesStrategy,
			IGetAllByNamesStrategy<Group> getGroupsByNameStrategy,
			IGetByNameStrategy<Client> getClientByNameStrategy,
			IMatterGetByNameAndClientIdStrategy matterGetByNameAndClientIdStrategy,
			IObjectService objectService)
		{
			_getWorkspaceByNameStrategy = getWorkspaceByNameStrategy;
			_getAllWorkspacesStrategy = getAllWorkspacesStrategy;
			_getGroupsByNameStrategy = getGroupsByNameStrategy;
			_getClientByNameStrategy = getClientByNameStrategy;
			_matterGetByNameAndClientIdStrategy = matterGetByNameAndClientIdStrategy;
			_objectService = objectService;
		}

		public Workspace FillRequiredProperties(Workspace entity)
		{
			if (entity.Name == null)
			{
				entity.Name = Randomizer.GetString("AT_");
			}

			if (entity.SqlFullTextLanguage == 0)
			{
				entity.SqlFullTextLanguage = SqlFullTextLanguage.English;
			}

			if (entity.DownloadHandlerUrl == null)
			{
				entity.DownloadHandlerUrl = "Relativity.Distributed";
			}

			entity.TemplateWorkspace = ResolveTemplateWorkspace(entity.TemplateWorkspace);

			NamedArtifact client = ResolveClient(entity.Client);
			entity.Matter = ResolveMatter(entity.Matter, client?.ArtifactID);

			if (entity.WorkspaceAdminGroup != null)
			{
				entity.WorkspaceAdminGroup = ResolveGroup(entity.WorkspaceAdminGroup);
			}

			ResourcePool resourcePool = ResolveResourcePool(entity.ResourcePool);
			entity.ResourcePool = resourcePool;

			if (entity.DefaultCacheLocation == null)
			{
				entity.DefaultCacheLocation = resourcePool.CacheLocationServers.FirstOrDefault();
			}

			if (entity.DefaultFileRepository == null)
			{
				entity.DefaultFileRepository = resourcePool.FileRepositories.FirstOrDefault();
			}

			if (entity.SqlServer == null)
			{
				entity.SqlServer = resourcePool.SqlServers.FirstOrDefault();
			}

			return entity;
		}

		private NamedArtifact ResolveTemplateWorkspace(NamedArtifact templateWorkspace)
		{
			NamedArtifact workspace;

			if (templateWorkspace == null)
			{
				workspace = GetDefaultTemplate();
			}
			else if (templateWorkspace.ArtifactID > 0)
			{
				workspace = templateWorkspace;
			}
			else if (templateWorkspace.Name != null)
			{
				workspace = GetTemplateByName(templateWorkspace.Name);
			}
			else
			{
				throw new ArgumentException("Failed to create a workspace as template workspace has neither name nor ArtifactID specified.");
			}

			return workspace;
		}

		private NamedArtifact GetDefaultTemplate()
		{
			NamedArtifact templateWorkspace;

			Workspace[] workspaces = _getAllWorkspacesStrategy.GetAll();

			if (!workspaces.Any())
			{
				throw new ObjectNotFoundException("There are no workspaces in the environment.");
			}

			templateWorkspace = workspaces.FirstOrDefault(x => _defaultTemplateWorkspaceNames.Contains(x.Name)) ?? workspaces.First(x => x.Status == "Active");

			return templateWorkspace;
		}

		private NamedArtifact GetTemplateByName(string name)
		{
			NamedArtifact templateWorkspace = _getWorkspaceByNameStrategy.Get(name);

			if (templateWorkspace == null)
			{
				throw ObjectNotFoundException.CreateForNotFoundByName<Workspace>(name);
			}

			return templateWorkspace;
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

		private NamedArtifact ResolveMatter(NamedArtifact matter, int? clientID)
		{
			if (!(matter?.ArtifactID > 0) && matter?.Name != null)
			{
				if (clientID is null)
				{
					throw new InvalidOperationException("Cannot resolve matter as client ID is not specified.");
				}

				Matter gotMatter = _matterGetByNameAndClientIdStrategy.Get(matter.Name, clientID.Value);

				if (gotMatter == null)
				{
					throw ObjectNotFoundException.CreateForNotFoundByName<Matter>(matter.Name);
				}

				matter = gotMatter;
			}
			else
			{
				matter = ResolveMatterForBlankMatter(clientID);
			}

			return matter;
		}

		private NamedArtifact ResolveMatterForBlankMatter(int? clientID)
		{
			NamedArtifact matter = null;

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

			return matter;
		}

		private ResourcePool ResolveResourcePool(NamedArtifact resourcePool)
		{
			ResourcePool pool;
			if (resourcePool == null)
			{
				pool = _objectService.Query<ResourcePool>().First();
			}
			else if (resourcePool.ArtifactID > 0)
			{
				pool = _objectService.Query<ResourcePool>().FirstOrDefault(x => x.ArtifactID == resourcePool.ArtifactID);

				if (pool == null)
				{
					throw ObjectNotFoundException.CreateForNotFoundById<ResourcePool>(resourcePool.ArtifactID);
				}
			}
			else
			{
				pool = _objectService.Query<ResourcePool>().FirstOrDefault(x => x.Name == resourcePool.Name);

				if (pool == null)
				{
					throw ObjectNotFoundException.CreateForNotFoundByName<ResourcePool>(resourcePool.Name);
				}
			}

			return pool;
		}
	}
}
