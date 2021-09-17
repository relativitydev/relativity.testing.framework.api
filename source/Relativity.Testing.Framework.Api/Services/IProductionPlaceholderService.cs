using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the production placeholder API service.
	/// </summary>
	public interface IProductionPlaceholderService
	{
		/// <summary>
		/// Creates the specified production placeholder.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entity">The placeholder entity to create.</param>
		/// <returns>The created production placeholder source entity.</returns>
		ProductionPlaceholder Create(int workspaceId, ProductionPlaceholder entity);

		/// <summary>
		/// Gets the production placeholder by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the production placeholder.</param>
		/// <param name="entityId">The Artifact ID of the production placeholder.</param>
		/// <returns>The <see cref="ProductionPlaceholder"/> entity or <see langword="null"/>.</returns>
		ProductionPlaceholder Get(int workspaceId, int entityId);

		/// <summary>
		/// Determines whether the production placeholder with the specified case artifact ID exists.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the production placeholder.</param>
		/// <returns><see langword="true"/> if a production placeholder exists; otherwise, <see langword="false"/>.</returns>
		bool Exists(int workspaceId, int entityId);

		/// <summary>
		/// Updates the specified production placeholder.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the production placeholder.</param>
		/// <param name="entity">The entity to update.</param>
		/// <returns>The updated production placeholder source entity.</returns>
		ProductionPlaceholder Update(int workspaceId, ProductionPlaceholder entity);

		/// <summary>
		/// Delete the production placeholder by the specified artifact ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the production placeholder.</param>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Retrieve default field values for a placeholder.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <returns>The <see cref="DefaultFieldValue{NamedArtifact}"/>.</returns>
		DefaultFieldValue<NamedArtifact> GetDefaultFieldValues(int workspaceId);
	}
}
