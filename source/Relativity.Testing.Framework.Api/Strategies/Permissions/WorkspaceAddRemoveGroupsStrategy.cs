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

		public void AddRemoveWorkspaceGroups(int workspaceId, GroupSelector selector)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				groupSelector = selector
			};

			lock (GroupSelectorLocker.Locker)
			{
				_restService.Post(
					"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/AddRemoveWorkspaceGroupsAsync",
					dto);
			}
		}
	}
}
