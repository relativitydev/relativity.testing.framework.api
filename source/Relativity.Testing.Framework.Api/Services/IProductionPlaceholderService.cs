using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the production placeholder API service.
	/// </summary>
	/// <example>
	/// <code>
	/// IProductionPlaceholderService _productionPlaceholderService = RelativityFacade.Resolve&lt;IProductionPlaceholderService&gt;();
	/// </code>
	/// </example>
	public interface IProductionPlaceholderService
	{
		/// <summary>
		/// Creates the specified production placeholder.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entity">The placeholder entity to create.</param>
		/// <returns>The created production placeholder source entity.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// string fileName = "file_name.jpg";
		/// string filePath = $"path/to/file/catalog/{fileName}";
		///
		/// string fileContent = Convert.ToBase64String(File.ReadAllBytes(filePath));
		///
		/// var productionPlaceholder = new ProductionPlaceholder
		/// {
		/// 	Name = "Placeholder name",
		/// 	PlaceholderType = PlaceholderType.Image,
		/// 	FileName = fileName,
		/// 	FileData = fileContent
		/// };
		///
		/// ProductionPlaceholder result = _productionPlaceholderService.Create(workspaceID, productionPlaceholder);
		/// </code>
		/// </example>
		ProductionPlaceholder Create(int workspaceId, ProductionPlaceholder entity);

		/// <summary>
		/// Gets the production placeholder by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the production placeholder.</param>
		/// <param name="entityId">The Artifact ID of the production placeholder.</param>
		/// <returns>The [ProductionPlaceholder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ProductionPlaceholder.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int productionPlaceholderID = 654321;
		///
		/// ProductionPlaceholder result = _productionPlaceholderService.Get(workspaceID, productionPlaceholderID);
		/// </code>
		/// </example>
		ProductionPlaceholder Get(int workspaceId, int entityId);

		/// <summary>
		/// Determines whether the production placeholder with the specified case artifact ID exists.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the production placeholder.</param>
		/// <returns><see langword="true"/> if a production placeholder exists; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int productionPlaceholderID = 654321;
		///
		/// bool result = _productionPlaceholderService.Exists(workspaceID, productionPlaceholderID);
		/// </code>
		/// </example>
		bool Exists(int workspaceId, int entityId);

		/// <summary>
		/// Updates the specified production placeholder.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the production placeholder.</param>
		/// <param name="entity">The entity to update.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int productionPlaceholderID = 654321;
		///
		/// ProductionPlaceholder toUpdate = _productionPlaceholderService.Get(workspaceID, productionPlaceholderID);
		/// toUpdate.CustomText = "Updated Text";
		///
		/// _productionPlaceholderService.Update(workspaceID, toUpdate);
		/// </code>
		/// </example>
		void Update(int workspaceId, ProductionPlaceholder entity);

		/// <summary>
		/// Delete the production placeholder by the specified artifact ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the production placeholder.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int productionPlaceholderID = 654321;
		///
		/// _productionPlaceholderService.Delete(workspaceID, productionPlaceholderID);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Retrieve default field values for a placeholder.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <returns>The [DefaultFieldValue{NamedArtifact](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.DefaultFieldValue-1.html).</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		///
		/// DefaultFieldValue&lt;NamedArtifact&gt; result = _productionPlaceholderService.GetDefaultFieldValues(workspaceID);
		/// </code>
		/// </example>
		DefaultFieldValue<NamedArtifact> GetDefaultFieldValues(int workspaceId);
	}
}
