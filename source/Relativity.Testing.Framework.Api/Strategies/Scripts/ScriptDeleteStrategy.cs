using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ScriptDeleteStrategy : DeleteWorkspaceEntityByIdStrategy<Script>
	{
		private readonly IRestService _restService;

		public ScriptDeleteStrategy(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			_restService.Delete($"Relativity.Scripts/workspace/{workspaceId}/Scripts/{entityId}");
		}
	}
}
