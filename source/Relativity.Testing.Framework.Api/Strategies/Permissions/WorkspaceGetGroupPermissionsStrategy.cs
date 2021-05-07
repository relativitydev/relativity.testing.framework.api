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

		public GroupPermissions Get(int workspaceId, int groupId)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				group = new Artifact(groupId)
			};
			lock (GroupSelectorLocker.Locker)
			{
				return _restService.Post<GroupPermissions>(
					"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/GetWorkspaceGroupPermissionsAsync",
					dto);
			}
		}

		public GroupPermissions Get(int workspaceId, string groupName)
		{
			return Get(workspaceId, _groupService.Get(groupName).ArtifactID);
		}
	}
}
