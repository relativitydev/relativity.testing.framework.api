using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class KeywordSearchDeleteByIdStrategy : DeleteWorkspaceEntityByIdStrategy<KeywordSearch>
	{
		private readonly IRestService _restService;

		public KeywordSearchDeleteByIdStrategy(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				searchArtifactID = entityId
			};

			_restService.Post("Relativity.Services.Search.ISearchModule/Keyword%20Search%20Manager/DeleteSingleAsync", dto);
		}
	}
}
