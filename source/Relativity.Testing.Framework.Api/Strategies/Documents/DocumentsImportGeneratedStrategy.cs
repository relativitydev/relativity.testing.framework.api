using System.Data;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentsImportGeneratedStrategy : IDocumentsImportGeneratedStrategy
	{
		private readonly IDocumentsNativeImportStrategy _documentsNativeImportStrategy;

		public DocumentsImportGeneratedStrategy(IDocumentsNativeImportStrategy documentsNativeImportStrategy)
		{
			_documentsNativeImportStrategy = documentsNativeImportStrategy;
		}

		public void Import(int workspaceId, int numberOfDocuments = 10)
		{
			using (var sourceData = GenerateBasicDocuments(numberOfDocuments))
			{
				var options = new NativeImportOptions { NativeFileCopyMode = NativeFileCopyMode.DoNotImportNativeFiles, NativeFilePathColumnName = null };
				_documentsNativeImportStrategy.Import(workspaceId, sourceData, options);
			}
		}

		private DataTable GenerateBasicDocuments(int numberOfDocuments)
		{
			const string extractedTextFieldName = "Extracted Text";
			var dataTable = new DataTable("Documents");
			var options = new NativeImportOptions();
			dataTable.Columns.Add(options.DocumentIdentifierField);
			dataTable.Columns.Add(extractedTextFieldName);

			for (int i = 0; i < numberOfDocuments; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow[options.DocumentIdentifierField] = $"DOC{i + 1}";
				dataRow[extractedTextFieldName] = $"{extractedTextFieldName} for document {dataRow[options.DocumentIdentifierField]}";
				dataTable.Rows.Add(dataRow);
			}

			return dataTable;
		}
	}
}
