using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ViewGetOwnersStrategyPreOsier : IGetAllWorkspaceViewOwnersStrategy<NamedArtifact>
	{
		private readonly IRestService _restService;

		public ViewGetOwnersStrategyPreOsier(
			IRestService restService)
		{
			_restService = restService;
		}

		public NamedArtifact[] GetViewOwners(int workspaceId)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
			};

			return _restService.Post<NamedArtifact[]>("Relativity.Services.View.IViewModule/View%20Manager/GetViewOwnersAsync", dto);
		}
	}
}
