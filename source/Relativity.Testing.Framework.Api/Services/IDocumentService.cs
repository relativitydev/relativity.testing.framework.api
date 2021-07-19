using System.Data;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the document service.
	/// </summary>
	/// <example>
	/// <code>
	/// _documentService = relativityFacade.Resolve&lt;IDocumentService&gt;();
	/// </code>
	/// </example>
	public interface IDocumentService
	{
		/// <summary>
		/// Gets the documents by the specified workspace ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <returns>The array of documents.</returns>
		/// <example>
		/// <code>
		/// var workspaceArtifactId = 1234567;
		/// var documents = _documentService.GetAll(workspaceArtifactId);
		/// </code>
		/// </example>
		Document[] GetAll(int workspaceId);

		/// <summary>
		/// Gets the document by the entity name.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityName">The entity name.</param>
		/// <returns>Returns a document.</returns>
		/// <example>
		/// <code>
		/// var workspaceArtifactId = 1234567;
		/// Document document = _documentService.Get(workspaceArtifactId, "someDocumentName");
		/// </code>
		/// </example>
		Document Get(int workspaceId, string entityName);

		/// <summary>
		/// Deletes the workspace entity by the specified IDs of workspace and entity.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The entity ID.</param>
		/// <example>
		/// <code>
		/// var workspaceArtifactId = 1234567;
		/// var documentArtifactId = 12345;
		/// _documentService.Delete(workspaceArtifactId, documentArtifactId);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Creates and import basic document metadata to import to the given workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="numberOfDocuments">The number of documents to generate and import.</param>
		/// <example>
		/// <code>
		/// var workspaceService = Facade.Resolve&lt;IWorkspaceService&gt;();
		/// var defaultWorkspace = workspaceService.Create(new Workspace { Name = "DefaultWorkspaceName" });
		/// documentService.ImportGeneratedDocuments(defaultWorkspace.ArtifactID);
		/// </code>
		/// </example>
		void ImportGeneratedDocuments(int workspaceId, int numberOfDocuments = 10);

		/// <summary>
		/// Import image documents from data table object.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="dataTable">The data table object.</param>
		/// <param name="options">The options for document import.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactId = 1234567;
		/// DataTable table = new DataTable();
		/// table.Columns.Add("Bates", typeof(string));
		/// table.Columns.Add("Doc", typeof(string));
		/// table.Columns.Add("File", typeof(string));
		/// ImageImportOptions imageImportOptions = new ImageImportOptions()
		/// {
		/// 	DocumentIdentifierField = "Doc",
		/// 	BatesNumberField = "Bates",
		/// 	FileLocationField = "File",
		/// 	OverwriteMode = DocumentOverwriteMode.Append
		/// };
		/// documentService.ImportImages(workspaceArtifactId, table, imageImportOptions);
		/// </code>
		/// </example>
		void ImportImages(int workspaceId, DataTable dataTable, ImageImportOptions options = null);

		/// <summary>
		/// Import native documents from data table object.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="dataTable">The data table object.</param>
		/// <param name="options">The options for document import.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactId = 1234567;
		/// DataTable table = new DataTable();
		/// table.Columns.Add("Control Number", typeof(string));
		/// table.Columns.Add("Native File", typeof(string));
		/// table.Rows.Add("A_1", @"");
		/// table.Rows.Add("Image-5_00002", @"");
		///
		/// NativeImportOptions nativeImportOptions = new NativeImportOptions()
		/// {
		/// 	DocumentIdentifierField = "Control Number",
		/// 	NativeFilePathColumnName = "Native File",
		/// 	NativeFileCopyMode = NativeFileCopyMode.CopyFiles,
		/// 	OverwriteMode = DocumentOverwriteMode.Append
		/// };
		/// documentService.ImportNatives(workspaceArtifactId, table, nativeImportOptions);
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// int workspaceArtifactId = 1234567;
		/// var dataTable = new DataTable("Documents");
		/// var options = new NativeImportOptions()
		/// {
		/// 	NativeFileCopyMode = NativeFileCopyMode.DoNotImportNativeFiles,
		/// 	NativeFilePathColumnName = null,
		/// 	ExtractedTextFieldContainsFilePath = false,
		/// 	OverwriteMode = DocumentOverwriteMode.AppendOverlay
		/// };
		/// string controlNumberField = options.DocumentIdentifierField;
		///
		/// dataTable.Columns.Add(controlNumberField);
		/// dataTable.Columns.Add("Extracted Text");
		/// documentService.ImportNatives(workspaceArtifactId, dataTable, options);
		/// </code>
		/// </example>
		void ImportNatives(int workspaceId, DataTable dataTable, NativeImportOptions options = null);

		/// <summary>
		/// Import produced image documents from data table object.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="productionId">The production ID.</param>
		/// <param name="dataTable">The data table object.</param>
		/// <param name="options">The options for document import.</param>
		/// <example>
		/// <code>
		/// int productionId = 12354;
		/// int workspaceArtifactId = 1234567;
		///
		/// DataTable table = new DataTable();
		/// table.Columns.Add("Bates", typeof(string));
		/// table.Columns.Add("Doc", typeof(string));
		/// table.Columns.Add("File", typeof(string));
		/// ImageImportOptions imageImportOptions = new ImageImportOptions()
		/// {
		/// 	DocumentIdentifierField = "Doc",
		/// 	BatesNumberField = "Bates",
		/// 	FileLocationField = "File",
		/// 	OverwriteMode = DocumentOverwriteMode.Append
		/// };
		/// documentService.ImportProducedImages(workspaceArtifactId, productionId, table, imageImportOptions);
		/// </code>
		/// </example>
		void ImportProducedImages(int workspaceId, int productionId, DataTable dataTable, ImageImportOptions options = null);

		/// <summary>
		/// Import image documents from comma-separated values file.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="pathToFile">The path to the existing comma-separated values file.</param>
		/// <param name="options">The options for document import.</param>
		/// <example>
		/// <code>
		/// ImageImportOptions options = new ImageImportOptions();
		/// int workspaceId = 1234;
		/// string fullPath = "some\path\to\file.csv";
		/// documentService.ImportImagesFromCsv(workspaceId, fullPath, options);
		/// </code>
		/// </example>
		void ImportImagesFromCsv(int workspaceId, string pathToFile, ImageImportOptions options = null);

		/// <summary>
		/// Import native documents from comma-separated values file.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="pathToFile">The path to the existing comma-separated values file.</param>
		/// <param name="options">The options for document import.</param>
		/// <example>
		/// <code>
		/// NativeImportOptions options = new NativeImportOptions();
		/// int workspaceId = 1234;
		/// string fullPath = "some\path\to\file.csv";
		/// documentService.ImportNativesFromCsv(workspaceId, fullPath, options);
		/// </code>
		/// </example>
		void ImportNativesFromCsv(int workspaceId, string pathToFile, NativeImportOptions options = null);

		/// <summary>
		/// Import produced image documents from comma-separated values file.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="productionId">The production ID.</param>
		/// <param name="pathToFile">The path to the existing comma-separated values file.</param>
		/// <param name="options">The options for document import.</param>
		/// <example>
		/// <code>
		/// int workspaceId = 1234;
		/// int productionId = 12354;
		/// string fullPath = "some\path\to\file.csv";
		/// ImageImportOptions options = new ImageImportOptions();
		/// documentService.ImportProducedImagesFromCsv(workspaceId, productionId, fullPath, options);
		/// </code>
		/// </example>
		void ImportProducedImagesFromCsv(int workspaceId, int productionId, string pathToFile, ImageImportOptions options = null);

		/// <summary>
		/// Import single image document by specified path.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="pathToFile">The path to the existing native file.</param>
		/// <example>
		/// <code>
		/// int workspaceId = 1234;
		/// string fullPath = "some\path\to\file";
		/// documentService.ImportSingleImage(workspaceId, fullPath);
		/// </code>
		/// </example>
		void ImportSingleImage(int workspaceId, string pathToFile);

		/// <summary>
		/// Import single native document by specified path.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="pathToFile">The path to the existing native file.</param>
		/// <example>
		/// <code>
		/// int workspaceId = 1234;
		/// string fullPath = "some\path\to\native\file";
		/// documentService.ImportSingleNative(workspaceId, fullPath);
		/// </code>
		/// </example>
		void ImportSingleNative(int workspaceId, string pathToFile);

		/// <summary>
		/// Import single produced image document by specified path.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="productionId">The production ID.</param>
		/// <param name="pathToFile">The path to the existing file.</param>
		/// <example>
		/// <code>
		/// int workspaceId = 1234;
		/// int productionId = 12354;
		/// string fullPath = "some\path\to\file.csv";
		/// ImageImportOptions options = new ImageImportOptions();
		/// documentService.ImportProducedImagesFromCsv(workspaceId, productionId, fullPath, options);
		/// </code>
		/// </example>
		void ImportSingleProducedImage(int workspaceId, int productionId, string pathToFile);
	}
}
