using System.Linq;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class KeywordSearchGetByNameStrategy : IGetWorkspaceEntityByNameStrategy<KeywordSearch>
	{
		private readonly IKeywordSearchQueryStrategy _keywordSearchQueryStrategy;

		public KeywordSearchGetByNameStrategy(IKeywordSearchQueryStrategy keywordSearchQueryStrategy)
		{
			_keywordSearchQueryStrategy = keywordSearchQueryStrategy;
		}

		public KeywordSearch Get(int workspaceId, string entityName)
		{
			return _keywordSearchQueryStrategy.Query(workspaceId, $"'{nameof(KeywordSearch.Name)}' == '{entityName}'").FirstOrDefault();
		}
	}
}
