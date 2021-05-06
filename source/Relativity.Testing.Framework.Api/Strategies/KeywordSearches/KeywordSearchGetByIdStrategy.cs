using System.Linq;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class KeywordSearchGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<KeywordSearch>
	{
		private readonly IKeywordSearchQueryStrategy _keywordSearchQueryStrategy;

		public KeywordSearchGetByIdStrategy(IKeywordSearchQueryStrategy keywordSearchQueryStrategy)
		{
			_keywordSearchQueryStrategy = keywordSearchQueryStrategy;
		}

		public KeywordSearch Get(int workspaceId, int entityId)
		{
			return _keywordSearchQueryStrategy.Query(workspaceId, $"'Artifact ID' == {entityId}").FirstOrDefault();
		}
	}
}
