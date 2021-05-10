using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of keyword search query.
	/// </summary>
	internal interface IKeywordSearchQueryStrategy
	{
		/// <summary>
		/// Query the keyword seaches by specified condition.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="condition">The condition.</param>
		/// <returns>The array of keyword seaches.</returns>
		KeywordSearch[] Query(int workspaceId, string condition);
	}
}
