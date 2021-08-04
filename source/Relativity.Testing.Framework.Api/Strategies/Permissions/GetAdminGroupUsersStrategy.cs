using System.Collections.Generic;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class GetAdminGroupUsersStrategy : IGetAdminGroupUsersStrategy
	{
		private const string _URL = "Relativity.Services.Permission.IPermissionModule/Permission%20Manager/GetAdminGroupUsersAsync";
		private readonly IRestService _restService;
		private readonly IGroupService _groupService;

		public GetAdminGroupUsersStrategy(IRestService restService, IGroupService groupService)
		{
			_restService = restService;
			_groupService = groupService;
		}

		public async Task<List<NamedArtifact>> GetAsync(string name)
		{
			int groupArtifactId = _groupService.Get(name).ArtifactID;
			return await GetAsync(groupArtifactId).ConfigureAwait(false);
		}

		public async Task<List<NamedArtifact>> GetAsync(int groupId)
		{
			var dto = new GroupDTO(groupId);

			return await _restService.PostAsync<List<NamedArtifact>>(_URL, dto).ConfigureAwait(false);
		}
	}
}
