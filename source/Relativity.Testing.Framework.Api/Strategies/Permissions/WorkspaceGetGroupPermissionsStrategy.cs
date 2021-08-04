using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceGetGroupPermissionsStrategy : IWorkspaceGetGroupPermissionsStrategy
	{
		private readonly IRestService _restService;
		private readonly IGroupService _groupService;

		public WorkspaceGetGroupPermissionsStrategy(IRestService restService, IGroupService groupService)
		{
			_restService = restService;
			_groupService = groupService;
		}

		public async Task<GroupPermissions> GetAsync(int workspaceId, int groupId)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				group = new Artifact(groupId)
			};

			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				return await _restService.PostAsync<GroupPermissions>(
					"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/GetWorkspaceGroupPermissionsAsync",
					dto).ConfigureAwait(false);
			}
		}

		public async Task<GroupPermissions> GetAsync(int workspaceId, string groupName)
		{
			var groupArtifactId = _groupService.Get(groupName).ArtifactID;
			return await GetAsync(workspaceId, groupArtifactId).ConfigureAwait(false);
		}
	}
}
