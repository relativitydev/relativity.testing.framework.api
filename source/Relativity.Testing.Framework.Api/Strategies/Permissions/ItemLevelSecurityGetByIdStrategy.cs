using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ItemLevelSecurityGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<ItemLevelSecurity>
	{
		private readonly IRestService _restService;

		public ItemLevelSecurityGetByIdStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public ItemLevelSecurity Get(int workspaceId, int entityId)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				ArtifactID = entityId
			};

			return _restService.Post<ItemLevelSecurity>(
				"Relativity.Services.Permission.IPermissionModule/Permission Manager/GetItemLevelSecurityAsync",
				dto);
		}
	}
}
