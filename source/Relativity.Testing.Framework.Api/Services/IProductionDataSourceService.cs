using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the production data source API service.
	/// </summary>
	/// <example>
	/// <code>
	/// IProductionDataSourceService _productionDataSourceService = RelativityFacade.Resolve&lt;IProductionDataSourceService&gt;();
	/// </code>
	/// </example>
	public interface IProductionDataSourceService
	{
		/// <summary>
		/// Creates the specified production data source.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html).</param>
		/// <param name="productionId">The Artifact ID of the [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).</param>
		/// <param name="entity">The [ProductionDataSource](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ProductionDataSource.html) to create.</param>
		/// <returns>The created data source entity.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		///
		/// Production _production = Facade.Resolve&lt;ICreateWorkspaceEntityStrategy&lt;Production&gt;&gt;()
		///     .Create(workspaceID, new Production());
		/// KeywordSearch _keywordSearch = Facade.Resolve&lt;ICreateWorkspaceEntityStrategy&lt;KeywordSearch&gt;&gt;()
		///     .Create(workspaceID, new KeywordSearch());
		///
		/// ProductionDataSource toCreate = new ProductionDataSource
		/// {
		///     Name = "Some name",
		///     ProductionId = _production.ArtifactID,
		///     ProductionType = ProductionType.NativesOnly,
		///     SavedSearch = new NamedArtifact { ArtifactID = _keywordSearch.ArtifactID },
		///     UseImagePlaceholder = UseImagePlaceholderOption.NeverUseImagePlaceholder
		/// };
		///
		/// ProductionDataSource result = _productionDataSourceService.Create(workspaceID, _production.ArtifactID, toCreate);
		/// </code>
		/// </example>
		ProductionDataSource Create(int workspaceId, int productionId, ProductionDataSource entity);

		/// <summary>
		/// Determines whether the production data source with the specified case artifact ID exists.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html).</param>
		/// <param name="entityId">The Artifact ID of the [ProductionDataSource](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ProductionDataSource.html).</param>
		/// <returns><see langword="true"/> if a production data source exists; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int productionDataSourceID  = 198715;
		///
		/// bool result = _productionDataSourceService.Exists(workspaceID, productionDataSourceID);
		/// </code>
		/// </example>
		bool Exists(int workspaceId, int entityId);

		/// <summary>
		/// Delete the production data source by the specified artifact ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html).</param>
		/// <param name="entityId">The Artifact ID of the [ProductionDataSource](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ProductionDataSource.html).</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int productionDataSourceID  = 198715;
		///
		/// _productionDataSourceService.Delete(workspaceID, productionDataSourceID);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the production data source by the specified workspace ID and production set ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html).</param>
		/// <param name="entityId">The Artifact ID of the [ProductionDataSource](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ProductionDataSource.html).</param>
		/// <returns>The entity.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int productionDataSourceID = 198715;
		///
		/// ProductionDataSource productionDataSource = _productionDataSourceService.Get(workspaceID, productionDataSourceID);
		/// </code>
		/// </example>
		ProductionDataSource Get(int workspaceId, int entityId);

		/// <summary>
		/// Updates the specified production data source.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html).</param>
		/// <param name="productionId">The Artifact ID of the [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).</param>
		/// <param name="entity">The [ProductionDataSource](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ProductionDataSource.html) to update.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int productionDataSourceID = 198715;
		///
		/// ProductionDataSource toUpdate = _productionDataSourceService.Get(workspaceID, productionDataSourceID);
		/// toUpdate.Name = "New name";
		///
		/// _productionDataSourceService.Update(workspaceID, toUpdate.ProductionId, toUpdate);
		/// </code>
		/// </example>
		void Update(int workspaceId, int productionId, ProductionDataSource entity);

		/// <summary>
		/// Retrieves default field values for a data source.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html).</param>
		/// <returns>Default values for a data source as [ProductionDataSourceDefaultValues](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ProductionDataSourceDefaultValues.html) object.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		///
		/// ProductionDataSourceDefaultValues defaultValues = _productionDataSourceService.GetDefaultFieldValues(workspaceID);
		/// </code>
		/// </example>
		ProductionDataSourceDefaultValues GetDefaultFieldValues(int workspaceId);
	}
}
