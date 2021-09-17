using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the production data source API service.
	/// </summary>
	public interface IProductionDataSourceService
	{
		/// <summary>
		/// Creates the specified production data source.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="productionId">The Artifact ID of production.</param>
		/// <param name="entity">The data source entity to create.</param>
		/// <returns>The created data source entity.</returns>
		ProductionDataSource Create(int workspaceId, int productionId, ProductionDataSource entity);

		/// <summary>
		/// Determines whether the production data source with the specified case artifact ID exists.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the production data source.</param>
		/// <returns><see langword="true"/> if a production data source exists; otherwise, <see langword="false"/>.</returns>
		bool Exists(int workspaceId, int entityId);

		/// <summary>
		/// Delete the production data source by the specified artifact ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the production data source.</param>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the production data source by the specified workspace ID and production set ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The entity ID.</param>
		/// <returns>The entity.</returns>
		ProductionDataSource Get(int workspaceId, int entityId);

		/// <summary>
		/// Updates the specified production data source.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the production data source.</param>
		/// <param name="productionId">The Artifact ID of production.</param>
		/// <param name="entity">The entity to update.</param>
		/// <returns>The updated data source entity.</returns>
		ProductionDataSource Update(int workspaceId, int productionId, ProductionDataSource entity);
	}
}
