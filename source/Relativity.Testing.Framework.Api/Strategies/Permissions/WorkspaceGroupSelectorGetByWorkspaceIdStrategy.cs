using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceGroupSelectorGetByWorkspaceIdStrategy : IGetByWorkspaceIdStrategy<GroupSelector>
	{
		private readonly IRestService _restService;

		public WorkspaceGroupSelectorGetByWorkspaceIdStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public GroupSelector Get(int id)
		{
			var dto = new
			{
				workspaceArtifactID = id
			};

			using (GroupSelectorLocker.Locker.Lock())
			{
				return _restService.Post<GroupSelector>(
					"Relativity.Services.Permission.IPermissionModule/Permission Manager/GetWorkspaceGroupSelectorAsync",
					dto);
			}
		}
	}
}
