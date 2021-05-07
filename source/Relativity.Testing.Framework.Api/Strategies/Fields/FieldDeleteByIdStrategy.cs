using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FieldDeleteByIdStrategy : DeleteWorkspaceEntityByIdStrategy<Field>
	{
		private readonly IRestService _restService;

		public FieldDeleteByIdStrategy(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			_restService.Delete($"Relativity.Fields/workspace/{workspaceId}/fields/{entityId}");
		}
	}
}
