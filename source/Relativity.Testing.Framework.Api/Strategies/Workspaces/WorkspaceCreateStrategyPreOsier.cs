using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class WorkspaceCreateStrategyPreOsier : WorkspaceCreateAbstractStrategy
	{
		private readonly IRestService _restService;

		public WorkspaceCreateStrategyPreOsier(
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
		}

		protected override int CreateWorkspace(object workspaceToCreate)
		{
			var result = _restService.Post<JObject>("Relativity.Workspaces/workspace/", workspaceToCreate, 5);

			return int.Parse(result[nameof(Artifact.ArtifactID)].ToString());
		}
	}
}
