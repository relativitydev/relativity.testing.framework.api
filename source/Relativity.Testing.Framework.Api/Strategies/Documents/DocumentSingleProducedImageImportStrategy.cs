using System;
using System.Data;
using System.IO;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentSingleProducedImageImportStrategy : IDocumentSingleProducedImageImportStrategy
	{
		private readonly IDocumentsProductionImportStrategy _documentsImageImportStrategy;

		public DocumentSingleProducedImageImportStrategy(IDocumentsProductionImportStrategy documentsImageImportStrategy)
		{
			_documentsImageImportStrategy = documentsImageImportStrategy;
		}

		public void Import(int workspaceId, int productionId, string pathToFile)
		{
			if (!File.Exists(pathToFile))
			{
				throw new ArgumentException($"The specified file at '{pathToFile}' could not be found.");
			}

			using (var sourceData = GenerateImageDocumentSourceData(pathToFile))
			{
				_documentsImageImportStrategy.Import(workspaceId, productionId, sourceData);
			}
		}

		protected static DataTable GenerateImageDocumentSourceData(string path)
		{
			var options = new ImageImportOptions();

			var dataTable = new DataTable("Documents");
			dataTable.Columns.Add(options.BatesNumberField);
			dataTable.Columns.Add(options.DocumentIdentifierField);
			dataTable.Columns.Add(options.FileLocationField);

			DataRow dataRow = dataTable.NewRow();
			dataRow[options.BatesNumberField] = Path.GetFileName(path);
			dataRow[options.DocumentIdentifierField] = Path.GetFileName(path);
			dataRow[options.FileLocationField] = path;
			dataTable.Rows.Add(dataRow);

			return dataTable;
		}
	}
}
