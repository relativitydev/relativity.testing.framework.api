using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AdminGetGroupPermissionsStrategy : IAdminGetGroupPermissionsStrategy
	{
		private readonly IRestService _restService;
		private readonly IGroupService _groupService;

		public AdminGetGroupPermissionsStrategy(IRestService restService, IGroupService groupService)
		{
			_restService = restService;
			_groupService = groupService;
		}

		public GroupPermissions Get(int groupId)
		{
			var dto = new
			{
				group = new Artifact(groupId)
			};
			lock (GroupSelectorLocker.Locker)
			{
				return _restService.Post<GroupPermissions>(
					"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/GetAdminGroupPermissionsAsync",
					dto);
			}
		}

		public GroupPermissions Get(string groupName)
		{
			return Get(_groupService.Get(groupName).ArtifactID);
		}
	}
}
