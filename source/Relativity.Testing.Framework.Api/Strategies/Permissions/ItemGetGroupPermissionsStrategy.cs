using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ItemGetGroupPermissionsStrategy : IItemGetGroupPermissionsStrategy
	{
		private readonly IRestService _restService;

		public ItemGetGroupPermissionsStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public GroupPermissions Get(int workspaceId, int itemId, int groupId)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				ArtifactID = itemId,
				group = new Artifact(groupId)
			};
			lock (GroupSelectorLocker.Locker)
			{
				return _restService.Post<GroupPermissions>(
					"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/GetItemGroupPermissionsAsync",
					dto);
			}
		}
	}
}
