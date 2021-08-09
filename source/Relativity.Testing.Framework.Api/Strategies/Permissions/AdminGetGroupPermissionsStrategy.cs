using System.Threading.Tasks;
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

		public async Task<GroupPermissions> GetAsync(int groupId)
		{
			var dto = new
			{
				group = new Artifact(groupId)
			};

			return await _restService.PostAsync<GroupPermissions>(
				"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/GetAdminGroupPermissionsAsync", dto)
				.ConfigureAwait(false);
		}

		public async Task<GroupPermissions> GetAsync(string groupName)
		{
			var groupArtifactId = _groupService.Get(groupName).ArtifactID;
			return await GetAsync(groupArtifactId).ConfigureAwait(false);
		}
	}
}
