using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class KeywordSearchCreateStrategy : CreateWorkspaceEntityStrategy<KeywordSearch>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<KeywordSearch> _getWorkspaceEntityByIdStrategy;

		public KeywordSearchCreateStrategy(
			IRestService restService,
			IGetWorkspaceEntityByIdStrategy<KeywordSearch> getWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		protected override KeywordSearch DoCreate(int workspaceId, KeywordSearch entity)
		{
			entity.FillRequiredProperties();

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				searchDTO = entity
			};

			var artifactId = _restService.Post<int>("Relativity.Services.Search.ISearchModule/Keyword%20Search%20Manager/CreateSingleAsync", dto);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, artifactId);
		}
	}
}
