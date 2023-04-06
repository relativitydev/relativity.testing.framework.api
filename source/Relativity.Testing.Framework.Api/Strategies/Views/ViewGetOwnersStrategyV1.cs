using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ViewGetOwnersStrategyV1 : IGetAllWorkspaceViewOwnersStrategy<NamedArtifact>
	{
		private readonly IRestService _restService;

		public ViewGetOwnersStrategyV1(
			IRestService restService)
		{
			_restService = restService;
		}

		public NamedArtifact[] GetViewOwners(int workspaceId)
		{
			return _restService.Get<NamedArtifact[]>($"relativity-data-visualization/V1/workspaces/{workspaceId}/eligible-owners");
		}
	}
}
