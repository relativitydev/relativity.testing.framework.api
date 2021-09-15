using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class KeywordSearchUpdateStrategy : IUpdateWorkspaceEntityStrategy<KeywordSearch>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<KeywordSearch> _getWorkspaceEntityByIdStrategy;

		public KeywordSearchUpdateStrategy(IRestService restService, IGetWorkspaceEntityByIdStrategy<KeywordSearch> getWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		public KeywordSearch Update(int workspaceId, KeywordSearch entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				searchDTO = entity
			};

			_restService.Post($"/Relativity.REST/api/Relativity.Services.Search.ISearchModule/Keyword%20Search%20Manager/UpdateSingleAsync", dto);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, entity.ArtifactID);
		}
	}
}
