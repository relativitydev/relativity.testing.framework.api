using System.Collections.Generic;
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

		public List<NamedArtifact> Get(int groupId)
		{
			var dto = new GroupDTO(groupId);

			return _restService.Post<List<NamedArtifact>>(_URL, dto);
		}

		public List<NamedArtifact> Get(string name)
		{
			return Get(_groupService.Get(name).ArtifactID);
		}
	}
}
