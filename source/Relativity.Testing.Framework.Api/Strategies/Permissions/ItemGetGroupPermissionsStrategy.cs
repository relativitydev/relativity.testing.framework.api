using System.Threading.Tasks;
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

		public async Task<GroupPermissions> GetAsync(int workspaceId, int itemId, int groupId)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				ArtifactID = itemId,
				group = new Artifact(groupId)
			};

			return await _restService.PostAsync<GroupPermissions>(
				"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/GetItemGroupPermissionsAsync", dto)
				.ConfigureAwait(false);
		}
	}
}
