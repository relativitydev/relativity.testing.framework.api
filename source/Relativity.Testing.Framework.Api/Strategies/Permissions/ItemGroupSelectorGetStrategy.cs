using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ItemGroupSelectorGetStrategy : IItemGroupSelectorGetStrategy
	{
		private readonly IRestService _restService;

		public ItemGroupSelectorGetStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public GroupSelector Get(int workspaceId, int itemId)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				artifactID = itemId
			};
			lock (GroupSelectorLocker.Locker)
			{
				return _restService.Post<GroupSelector>(
					"Relativity.Services.Permission.IPermissionModule/Permission Manager/GetItemGroupSelectorAsync",
					dto);
			}
		}
	}
}
