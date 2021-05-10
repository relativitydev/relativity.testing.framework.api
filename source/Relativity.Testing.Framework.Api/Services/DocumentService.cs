using System.Data;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class DocumentService : IDocumentService
	{
		private readonly IGetAllWorkspaceEntitiesStrategy<Document> _getAllWorkspaceEntitiesStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<Document> _getWorkspaceEntityByNameStrategy;
		private readonly IDeleteWorkspaceEntityByIdStrategy<Document> _deleteWorkspaceEntityByIdStrategy;
		private readonly IDocumentsImportGeneratedStrategy _documentsImportGeneratedStrategy;
		private readonly IDocumentsImageImportStrategy _documentsImageImportStrategy;
		private readonly IDocumentsNativeImportStrategy _documentsNativeImportStrategy;
		private readonly IDocumentsProductionImportStrategy _documentsProductionImportStrategy;
		private readonly IDocumentsFromCsvImageImportStrategy _documentsFromCsvImageImportStrategy;
		private readonly IDocumentsFromCsvNativeImportStrategy _documentsFromCsvNativeImportStrategy;
		private readonly IDocumentsFromCsvProductionImportStrategy _documentsFromCsvProductionImportStrategy;
		private readonly IDocumentSingleImageImportStrategy _documentSingleImageImportStrategy;
		private readonly IDocumentSingleNativeImportStrategy _documentSingleNativeImportStrategy;
		private readonly IDocumentSingleProducedImageImportStrategy _documentSingleProducedImageImportStrategy;

		public DocumentService(
			IGetAllWorkspaceEntitiesStrategy<Document> getAllWorkspaceEntitiesStrategy,
			IGetWorkspaceEntityByNameStrategy<Document> getWorkspaceEntityByNameStrategy,
			IDeleteWorkspaceEntityByIdStrategy<Document> deleteWorkspaceEntityByIdStrategy,
			IDocumentsImportGeneratedStrategy documentsImportGeneratedStrategy,
			IDocumentsImageImportStrategy documentsImageImportStrategy,
			IDocumentsNativeImportStrategy documentsNativeImportStrategy,
			IDocumentsProductionImportStrategy documentsProductionImportStrategy,
			IDocumentsFromCsvImageImportStrategy documentsFromCsvImageImportStrategy,
			IDocumentsFromCsvNativeImportStrategy documentsFromCsvNativeImportStrategy,
			IDocumentsFromCsvProductionImportStrategy documentsFromCsvProductionImportStrategy,
			IDocumentSingleImageImportStrategy documentSingleImageImportStrategy,
			IDocumentSingleNativeImportStrategy documentSingleNativeImportStrategy,
			IDocumentSingleProducedImageImportStrategy documentSingleProducedImageImportStrategy)
		{
			_getAllWorkspaceEntitiesStrategy = getAllWorkspaceEntitiesStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
			_documentsImportGeneratedStrategy = documentsImportGeneratedStrategy;
			_documentsImageImportStrategy = documentsImageImportStrategy;
			_documentsNativeImportStrategy = documentsNativeImportStrategy;
			_documentsProductionImportStrategy = documentsProductionImportStrategy;
			_documentsFromCsvImageImportStrategy = documentsFromCsvImageImportStrategy;
			_documentsFromCsvNativeImportStrategy = documentsFromCsvNativeImportStrategy;
			_documentsFromCsvProductionImportStrategy = documentsFromCsvProductionImportStrategy;
			_documentSingleImageImportStrategy = documentSingleImageImportStrategy;
			_documentSingleNativeImportStrategy = documentSingleNativeImportStrategy;
			_documentSingleProducedImageImportStrategy = documentSingleProducedImageImportStrategy;
		}

		public Document[] GetAll(int workspaceId)
			=> _getAllWorkspaceEntitiesStrategy.GetAll(workspaceId);

		public Document Get(int workspaceId, string entityName)
			=> _getWorkspaceEntityByNameStrategy.Get(workspaceId, entityName);

		public void Delete(int workspaceId, int entityId)
			=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceId, entityId);

		public void ImportGeneratedDocuments(int workspaceId, int numberOfDocuments = 10)
			=> _documentsImportGeneratedStrategy.Import(workspaceId, numberOfDocuments);

		public void ImportImages(int workspaceId, DataTable dataTable, ImageImportOptions options = null)
			=> _documentsImageImportStrategy.Import(workspaceId, dataTable, options);

		public void ImportNatives(int workspaceId, DataTable dataTable, NativeImportOptions options = null)
			=> _documentsNativeImportStrategy.Import(workspaceId, dataTable, options);

		public void ImportProducedImages(int workspaceId, int productionId, DataTable dataTable, ImageImportOptions options = null)
			=> _documentsProductionImportStrategy.Import(workspaceId, productionId, dataTable, options);

		public void ImportImagesFromCsv(int workspaceId, string pathToFile, ImageImportOptions options = null)
			=> _documentsFromCsvImageImportStrategy.Import(workspaceId, pathToFile, options);

		public void ImportNativesFromCsv(int workspaceId, string pathToFile, NativeImportOptions options = null)
			=> _documentsFromCsvNativeImportStrategy.Import(workspaceId, pathToFile, options);

		public void ImportProducedImagesFromCsv(int workspaceId, int productionId, string pathToFile, ImageImportOptions options = null)
			=> _documentsFromCsvProductionImportStrategy.Import(workspaceId, productionId, pathToFile, options);

		public void ImportSingleImage(int workspaceId, string pathToFile)
			=> _documentSingleImageImportStrategy.Import(workspaceId, pathToFile);

		public void ImportSingleNative(int workspaceId, string pathToFile)
			=> _documentSingleNativeImportStrategy.Import(workspaceId, pathToFile);

		public void ImportSingleProducedImage(int workspaceId, int productionId, string pathToFile)
			=> _documentSingleProducedImageImportStrategy.Import(workspaceId, productionId, pathToFile);
	}
}
