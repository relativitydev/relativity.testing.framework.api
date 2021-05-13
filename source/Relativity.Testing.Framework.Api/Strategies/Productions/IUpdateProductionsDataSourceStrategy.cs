using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of productions update data source strategy.
	/// </summary>
	internal interface IUpdateProductionsDataSourceStrategy
	{
		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="productionId">The Artifact ID of a production.</param>
		/// <param name="entity">The entity of a data source.</param>
		void Update(int workspaceId, int productionId, ProductionDataSource entity);
	}
}
