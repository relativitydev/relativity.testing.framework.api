using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.0 <12.1")]
	internal class LayoutDeleteByIdStrategyNinebark : DeleteWorkspaceEntityByIdStrategy<Layout>
	{
		private readonly IRestService _restService;

		public LayoutDeleteByIdStrategyNinebark(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			_restService.Delete($"Relativity.Layouts/workspace/{workspaceId}/layouts/{entityId}");
		}
	}
}
