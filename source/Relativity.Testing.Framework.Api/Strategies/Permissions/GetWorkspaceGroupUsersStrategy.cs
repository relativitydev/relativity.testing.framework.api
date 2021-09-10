using System.Collections.Generic;
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

		public List<NamedArtifact> Get(int workspaceId, int groupId)
		{
			var dto = new WorkspaceGroupUserDTO(workspaceId, groupId);

			return _restService.Post<List<NamedArtifact>>(
				"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/GetWorkspaceGroupUsersAsync",
				dto);
		}

		public List<NamedArtifact> Get(int workspaceId, string groupName)
		{
			return Get(workspaceId, _groupService.Get(groupName).ArtifactID);
		}
	}
}
