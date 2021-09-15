using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class KeywordSearchService : IKeywordSearchService
	{
		private readonly ICreateWorkspaceEntityStrategy<KeywordSearch> _createWorkspaceEntityStrategy;
		private readonly IDeleteWorkspaceEntityByIdStrategy<KeywordSearch> _deleteWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<KeywordSearch> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<KeywordSearch> _getWorkspaceEntityByNameStrategy;
		private readonly IKeywordSearchQueryStrategy _keywordSearchQueryStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<KeywordSearch> _updateWorkspaceEntityStrategy;
		private readonly IRequireWorkspaceEntityStrategy<KeywordSearch> _requireWorkspaceEntityStrategy;

		public KeywordSearchService(
			ICreateWorkspaceEntityStrategy<KeywordSearch> createWorkspaceEntityStrategy,
			IDeleteWorkspaceEntityByIdStrategy<KeywordSearch> deleteWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByIdStrategy<KeywordSearch> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<KeywordSearch> getWorkspaceEntityByNameStrategy,
			IKeywordSearchQueryStrategy keywordSearchQueryStrategy,
			IUpdateWorkspaceEntityStrategy<KeywordSearch> updateWorkspaceEntityStrategy,
			IRequireWorkspaceEntityStrategy<KeywordSearch> requireWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_keywordSearchQueryStrategy = keywordSearchQueryStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
			_requireWorkspaceEntityStrategy = requireWorkspaceEntityStrategy;
		}

		public KeywordSearch Create(int workspaceId, KeywordSearch entity)
			=> _createWorkspaceEntityStrategy.Create(workspaceId, entity);

		public void Delete(int workspaceId, int entityId)
			=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceId, entityId);

		public KeywordSearch Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);

		public KeywordSearch Get(int workspaceId, string entityName)
			=> _getWorkspaceEntityByNameStrategy.Get(workspaceId, entityName);

		public KeywordSearch[] Query(int workspaceId, string condition)
			=> _keywordSearchQueryStrategy.Query(workspaceId, condition);

		public KeywordSearch Update(int workspaceId, KeywordSearch entity)
			=> _updateWorkspaceEntityStrategy.Update(workspaceId, entity);

		public KeywordSearch Require(int workspaceId, KeywordSearch entity)
			=> _requireWorkspaceEntityStrategy.Require(workspaceId, entity);
	}
}
