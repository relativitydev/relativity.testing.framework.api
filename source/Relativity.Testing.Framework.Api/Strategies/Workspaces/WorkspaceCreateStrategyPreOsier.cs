using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
<<<<<<< HEAD:source/Relativity.Testing.Framework.Api/Strategies/Workspaces/WorkspaceCreateAbstractStrategy.cs
	internal abstract class WorkspaceCreateAbstractStrategy : CreateStrategy<Workspace>
=======
	[VersionRange("<12.1")]
	internal class WorkspaceCreateStrategyPreOsier : WorkspaceCreateAbstractStrategy
	{
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

<<<<<<< HEAD:source/Relativity.Testing.Framework.Api/Strategies/Workspaces/WorkspaceCreateAbstractStrategy.cs
		protected WorkspaceCreateAbstractStrategy(
=======
#pragma warning disable S107 // Methods should not have too many parameters
		public WorkspaceCreateStrategyPreOsier(
			IRestService restService,
>>>>>>> 3b0cf480cf4ea21af1f9135ca971a7ac6e1592a6:source/Relativity.Testing.Framework.Api/Strategies/Workspaces/WorkspaceCreateStrategyPreOsier.cs
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
		}

		protected override int CreateWorkspace(object workspaceToCreate)
		{
			var result = _restService.Post<JObject>("Relativity.Workspaces/workspace/", workspaceToCreate, 5);

			return int.Parse(result[nameof(Artifact.ArtifactID)].ToString());
		}
	}
}
