using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the keyword search API service.
	/// </summary>
	/// <example>
	/// <code>
	/// IKeywordSearchService _keywordSearchService = relativityFacade.Resolve&lt;IKeywordSearchService&gt;();
	/// </code>
	/// </example>
	public interface IKeywordSearchService
	{
		/// <summary>
		/// Creates the specified [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html).
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to add the new [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html).</param>
		/// <param name="entity">The [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html) to create.</param>
		/// <returns>The created [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html).</returns>
		/// <example> This shows how to create new keyword search using the control number to search for entities that contain "EXAMPLE_CONTROL_NUMBER", which returns Control Number fields and sorts results in descending order.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// const string controlNumber = "Control Number";
		/// var keywordSearchToCreate = new KeywordSearch
		/// {
		/// 	Name = "Keyword Search Name",
		/// 	SearchCriteria =  new CriteriaCollection
		/// 	{
		/// 		Conditions = new List&lt;BaseCriteria&gt;
		/// 		{
		/// 			new Criteria
		/// 			{
		/// 				Condition = new CriteriaCondition(new NamedArtifact { Name = controlNumber }, ConditionOperator.Contains, "EXAMPLE_CONTROL_NUMBER"),
		/// 			}
		/// 		}
		/// 	},
		/// 	Fields = new[] { new NamedArtifact { Name = controlNumber } },
		/// 	Sorts = new List&lt;Sort&gt; { new Sort { FieldIdentifier = new NamedArtifact { Name = controlNumber }, Direction = SortDirection.Descending } }
		/// };
		/// KeywordSearch createdKeywordSearch = _keywordSearchService.Create(workspaceArtifactID, keywordSearchToCreate);
		/// </code>
		/// </example>
		KeywordSearch Create(int workspaceId, KeywordSearch entity);

		/// <summary>
		/// Deletes [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html) by ArtifactID.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to delete the [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html).</param>
		/// <param name="entityId">The ArtifactID of the [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html).</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int keywordSearchArtifactID = 1016786;
		/// _keywordSearchService.Delete(workspaceArtifactID, keywordSearchArtifactID);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html) by the specified ArtifactID.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to get [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html).</param>
		/// <param name="entityId">The ArtifactID of the [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html).</param>
		/// <returns>The [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int keywordSearchArtifactID = 1016786;
		/// KeywordSearch keywordSearch = _keywordSearchService.Get(workspaceArtifactID, keywordSearchArtifactID);
		/// </code>
		/// </example>
		KeywordSearch Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html) by the specified name.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to get [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html).</param>
		/// <param name="entityName">The name of the [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html).</param>
		/// <returns>The [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int keywordSearchName = "Existing Keyword Search Name";
		/// KeywordSearch keywordSearch = _keywordSearchService.Get(workspaceArtifactID, keywordSearchName);
		/// </code>
		/// </example>
		KeywordSearch Get(int workspaceId, string entityName);

		/// <summary>
		/// Query the [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html)es by specified condition.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to get [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html).</param>
		/// <param name="condition">The condition.</param>
		/// <returns>The array of [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html)es.</returns>
		/// <example> This example shows how to query for all keyword searches that has ArtifactID greater than or equal to 1016786.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// string queryCondition = "'Artifact ID' &gt;= 1016786";
		/// KeywordSearch[] keywordSearches = _keywordSearchService.Query(workspaceArtifactID, queryCondition);
		/// </code>
		/// </example>
		KeywordSearch[] Query(int workspaceId, string condition);

		/// <summary>
		/// Updates the specified [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html).
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to update the [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html).</param>
		/// <param name="entity">The [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html) to update.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int keywordSearchName = "Existing Keyword Search Name";
		/// KeywordSearch keywordSearchToUpdate = _keywordSearchService.Get(workspaceArtifactID, keywordSearchName);
		/// keywordSearchToUpdate.Name = "Updated Keyword Search Name";
		/// keywordSearchToUpdate.Notes = "Keyword Search Notes";
		/// _keywordSearchService.Update(workspaceArtifactID, keywordSearchToUpdate);
		/// </code>
		/// </example>
		void Update(int workspaceId, KeywordSearch entity);

		/// <summary>
		/// Requires the specified [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html).
		/// <list type="number">
		/// <item><description>If [Artifact.ArtifactID](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Artifact.html#Relativity_Testing_Framework_Models_Artifact_ArtifactID) property of <paramref name="entity"/> has positive value, gets [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html) by ArtifactID and updates it.</description></item>
		/// <item><description>If [NamedArtifact.Name](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.NamedArtifact.html#Relativity_Testing_Framework_Models_NamedArtifact_Name) property of <paramref name="entity"/> has a value, gets [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html) by name and updates it if it exists.</description></item>
		/// <item><description>Otherwise creates a new [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html) entity.</description></item>
		/// </list>
		/// </summary>
		/// <returns>The [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html) entity>.</returns>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to require [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html).</param>
		/// <param name="entity">The [KeywordSearch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeywordSearch.html) to require.</param>
		/// <example> This shows how to update and get keyword search by Require method with Keyword Search entity that has ArtifactID filled.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingKeywordSearchArtifactID = 1016786;
		/// const string controlNumber = "Control Number";
		/// var keywordSearchToUpdate = new KeywordSearch
		/// {
		/// 	ArtifactID = 1016786,
		/// 	Name = "Updated Keyword Search Name",
		/// 	SearchCriteria =  new CriteriaCollection
		/// 	{
		/// 		Conditions = new List&lt;BaseCriteria&gt;
		/// 		{
		/// 			new Criteria
		/// 			{
		/// 				Condition = new CriteriaCondition(new NamedArtifact { Name = controlNumber }, ConditionOperator.Contains, "EXAMPLE_CONTROL_NUMBER"),
		/// 			}
		/// 		}
		/// 	},
		/// 	Fields = new[] { new NamedArtifact { Name = controlNumber } },
		/// 	Sorts = new List&lt;Sort&gt; { new Sort { FieldIdentifier = new NamedArtifact { Name = controlNumber }, Direction = SortDirection.Descending } }
		/// };
		/// KeywordSearch updatedKeywordSearch = _keywordSearchService.Require(workspaceArtifactID, keywordSearchToUpdate);
		/// </code>
		/// </example>
		/// <example> This shows how to update and get keyword search by Require method with Keyword Search entity that has Name filled.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// const string controlNumber = "Control Number";
		/// var keywordSearchToUpdate = new KeywordSearch
		/// {
		/// 	Name = "Existing Keyword Search Name",
		/// 	SearchCriteria =  new CriteriaCollection
		/// 	{
		/// 		Conditions = new List&lt;BaseCriteria&gt;
		/// 		{
		/// 			new Criteria
		/// 			{
		/// 				Condition = new CriteriaCondition(new NamedArtifact { Name = controlNumber }, ConditionOperator.Contains, "EXAMPLE_CONTROL_NUMBER"),
		/// 			}
		/// 		}
		/// 	},
		/// 	Fields = new[] { new NamedArtifact { Name = controlNumber } },
		/// 	Sorts = new List&lt;Sort&gt; { new Sort { FieldIdentifier = new NamedArtifact { Name = controlNumber }, Direction = SortDirection.Descending } },
		/// 	SortByRank = true,
		/// 	Notes = "Updated Keyword Search Notes"
		/// };
		/// KeywordSearch updatedKeywordSearch = _keywordSearchService.Require(workspaceArtifactID, keywordSearchToUpdate);
		/// </code>
		/// </example>
		/// <example> This shows how to create and get new keyword search by Require method with new Keyword Search entity (without ArtifactID filled and Name that does not match any existing Keyword Search Name in given workspace).
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// const string controlNumber = "Control Number";
		/// var keywordSearchToCreate= new KeywordSearch
		/// {
		/// 	Name = "New Keyword Search Name",
		/// 	SearchCriteria =  new CriteriaCollection
		/// 	{
		/// 		Conditions = new List&lt;BaseCriteria&gt;
		/// 		{
		/// 			new Criteria
		/// 			{
		/// 				Condition = new CriteriaCondition(new NamedArtifact { Name = controlNumber }, ConditionOperator.Contains, "EXAMPLE_CONTROL_NUMBER"),
		/// 			}
		/// 		}
		/// 	},
		/// 	Fields = new[] { new NamedArtifact { Name = controlNumber } },
		/// 	Sorts = new List&lt;Sort&gt; { new Sort { FieldIdentifier = new NamedArtifact { Name = controlNumber }, Direction = SortDirection.Descending } },
		/// 	SortByRank = true,
		/// 	Notes = "Keyword Search Notes"
		/// };
		/// KeywordSearch createdKeywordSearch = _keywordSearchService.Require(workspaceArtifactID, keywordSearchToCreate);
		/// </code>
		/// </example>
		KeywordSearch Require(int workspaceId, KeywordSearch entity);
	}
}
