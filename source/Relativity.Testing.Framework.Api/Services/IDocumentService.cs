using System.Data;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the document service.
	/// </summary>
	public interface IDocumentService
	{
		/// <summary>
		/// Gets the documents by the specified workspace ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <returns>The array of documents.</returns>
		Document[] GetAll(int workspaceId);

		/// <summary>
		/// Gets the document by the entity name.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityName">The entity name.</param>
		/// <returns>Returns a document.</returns>
		Document Get(int workspaceId, string entityName);

		/// <summary>
		/// Deletes the workspace entity by the specified IDs of workspace and entity.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The entity ID.</param>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Creates and import basic document metadata to import to the given workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="numberOfDocuments">The number of documents to generate and import.</param>
		void ImportGeneratedDocuments(int workspaceId, int numberOfDocuments = 10);

		/// <summary>
		/// Import image documents from data table object.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="dataTable">The data table object.</param>
		/// <param name="options">The options for document import.</param>
		void ImportImages(int workspaceId, DataTable dataTable, ImageImportOptions options = null);

		/// <summary>
		/// Import native documents from data table object.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="dataTable">The data table object.</param>
		/// <param name="options">The options for document import.</param>
		void ImportNatives(int workspaceId, DataTable dataTable, NativeImportOptions options = null);

		/// <summary>
		/// Import produced image documents from data table object.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="productionId">The production ID.</param>
		/// <param name="dataTable">The data table object.</param>
		/// <param name="options">The options for document import.</param>
		void ImportProducedImages(int workspaceId, int productionId, DataTable dataTable, ImageImportOptions options = null);

		/// <summary>
		/// Import image documents from comma-separated values file.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="pathToFile">The path to the existing comma-separated values file.</param>
		/// <param name="options">The options for document import.</param>
		void ImportImagesFromCsv(int workspaceId, string pathToFile, ImageImportOptions options = null);

		/// <summary>
		/// Import native documents from comma-separated values file.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="pathToFile">The path to the existing comma-separated values file.</param>
		/// <param name="options">The options for document import.</param>
		void ImportNativesFromCsv(int workspaceId, string pathToFile, NativeImportOptions options = null);

		/// <summary>
		/// Import produced image documents from comma-separated values file.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="productionId">The production ID.</param>
		/// <param name="pathToFile">The path to the existing comma-separated values file.</param>
		/// <param name="options">The options for document import.</param>
		void ImportProducedImagesFromCsv(int workspaceId, int productionId, string pathToFile, ImageImportOptions options = null);

		/// <summary>
		/// Import single image document by specified path.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="pathToFile">The path to the existing native file.</param>
		void ImportSingleImage(int workspaceId, string pathToFile);

		/// <summary>
		/// Import single native document by specified path.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="pathToFile">The path to the existing native file.</param>
		void ImportSingleNative(int workspaceId, string pathToFile);

		/// <summary>
		/// Import single produced image document by specified path.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="productionId">The production ID.</param>
		/// <param name="pathToFile">The path to the existing file.</param>
		void ImportSingleProducedImage(int workspaceId, int productionId, string pathToFile);
	}
}
