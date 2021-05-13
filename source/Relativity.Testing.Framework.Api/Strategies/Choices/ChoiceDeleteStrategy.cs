using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ChoiceDeleteStrategy : DeleteWorkspaceEntityByIdStrategy<Choice>
	{
		private readonly IRestService _restService;
		private readonly IEnsureWorkspaceEntityExistsByIdStrategy<Choice> _ensureExistsByIdStrategy;

		public ChoiceDeleteStrategy(
			IRestService restService,
			IEnsureWorkspaceEntityExistsByIdStrategy<Choice> ensureExistsByIdStrategy)
		{
			_restService = restService;
			_ensureExistsByIdStrategy = ensureExistsByIdStrategy;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			_ensureExistsByIdStrategy.EnsureExists(workspaceId, entityId);

			_restService.Delete($"Relativity.Choices/workspace/{workspaceId}/choice/{entityId}");
		}
	}
}
