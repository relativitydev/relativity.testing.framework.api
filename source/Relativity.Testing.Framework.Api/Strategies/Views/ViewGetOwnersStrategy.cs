using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ViewGetOwnersStrategy : IGetAllWorkspaceViewOwnersStrategy<NamedArtifact>
	{
		private readonly IRestService _restService;

		public ViewGetOwnersStrategy(
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
