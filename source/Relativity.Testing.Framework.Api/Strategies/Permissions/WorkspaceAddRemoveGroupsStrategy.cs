using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceAddRemoveGroupsStrategy : IWorkspaceAddRemoveGroupsStrategy
	{
		private readonly IRestService _restService;

		public WorkspaceAddRemoveGroupsStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public async Task AddRemoveWorkspaceGroupsAsync(int workspaceId, GroupSelector selector)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				groupSelector = selector
			};

			await _restService.PostAsync(
				"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/AddRemoveWorkspaceGroupsAsync", dto)
				.ConfigureAwait(false);
		}
	}
}
