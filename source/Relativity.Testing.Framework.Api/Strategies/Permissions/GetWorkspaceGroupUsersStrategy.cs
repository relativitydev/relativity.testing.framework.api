using System.Collections.Generic;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class GetWorkspaceGroupUsersStrategy : IGetWorkspaceGroupUsersStrategy
	{
		private readonly IRestService _restService;
		private readonly IGroupService _groupService;

		public GetWorkspaceGroupUsersStrategy(IRestService restService, IGroupService groupService)
		{
			_restService = restService;
			_groupService = groupService;
		}

		public async Task<List<NamedArtifact>> GetAsync(int workspaceId, string groupName)
		{
			int groupArtifactId = _groupService.Get(groupName).ArtifactID;
			return await GetAsync(workspaceId, groupArtifactId).ConfigureAwait(false);
		}

		public async Task<List<NamedArtifact>> GetAsync(int workspaceId, int groupId)
		{
			var dto = new WorkspaceGroupUserDTO(workspaceId, groupId);

			return await _restService.PostAsync<List<NamedArtifact>>(
				"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/GetWorkspaceGroupUsersAsync",
				dto).ConfigureAwait(false);
		}
	}
}
