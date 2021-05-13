using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class KeywordSearchQueryStrategy : IKeywordSearchQueryStrategy
	{
		private readonly IRestService _restService;

		public KeywordSearchQueryStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public KeywordSearch[] Query(int workspaceId, string condition)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				query = new
				{
					Condition = condition
				}
			};

			var resultAsString = _restService.Post<string>("Relativity.Services.Search.ISearchModule/Keyword%20Search%20Manager/QueryAsync", dto);

			return Convert(resultAsString);
		}

		private static KeywordSearch[] Convert(string resultAsString)
		{
			var keywordSearches = new List<KeywordSearch>();

			JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(resultAsString);

			var results = jsonResponse["Results"];

			foreach (var result in results)
			{
				var artifact = result["Artifact"].ToObject<KeywordSearch>();

				var searchCreiteria = result["Artifact"][nameof(KeywordSearch.SearchCriteria)][nameof(KeywordSearch.SearchCriteria.Conditions)];

				if (searchCreiteria != null)
				{
					artifact.SearchCriteria = new CriteriaCollection { Conditions = ConvertCriteria(searchCreiteria) };
				}

				keywordSearches.Add(artifact);
			}

			return keywordSearches.ToArray();
		}

		private static List<BaseCriteria> ConvertCriteria(JToken jsonRootProperty)
		{
			var conditions = new List<BaseCriteria>();

			if (jsonRootProperty.Type == JTokenType.Array)
			{
				foreach (var item in jsonRootProperty.Children())
				{
					conditions.AddRange(ConvertCriteria(item));
				}
			}

			if (jsonRootProperty.Type == JTokenType.Object)
			{
				ConvertJObject(jsonRootProperty, conditions);
			}

			return conditions;
		}

		private static void ConvertJObject(JToken jsonRootProperty, List<BaseCriteria> conditions)
		{
			var children = (JObject)jsonRootProperty;

			if (children.Property(nameof(Criteria.Condition)) != null)
			{
				var criteria = children.ToObject<Criteria>();

				if (criteria.Condition.ConditionType == "Criteria")
				{
					criteria.Condition = children[nameof(Criteria.Condition)].ToObject<CriteriaCondition>();
				}
				else
				{
					criteria.Condition = children[nameof(Criteria.Condition)].ToObject<CriteriaDateCondition>();
				}

				conditions.Add(criteria);
			}

			if (children.Property(nameof(CriteriaCollection.Conditions)) != null)
			{
				var criteria = children.ToObject<CriteriaCollection>();

				criteria.Conditions = ConvertCriteria(children[nameof(KeywordSearch.SearchCriteria.Conditions)]);

				conditions.Add(criteria);
			}
		}
	}
}
