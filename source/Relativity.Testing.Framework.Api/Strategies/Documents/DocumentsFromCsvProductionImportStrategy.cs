using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentsFromCsvProductionImportStrategy : IDocumentsFromCsvProductionImportStrategy
	{
		private readonly IDocumentsProductionImportStrategy _documentsProductionImportStrategy;

		public DocumentsFromCsvProductionImportStrategy(IDocumentsProductionImportStrategy documentsProductionImportStrategy)
		{
			_documentsProductionImportStrategy = documentsProductionImportStrategy;
		}

		public void Import(int workspaceId, int productionId, string pathToFile, ImageImportOptions options = null)
		{
			if (!System.IO.File.Exists(pathToFile))
			{
				throw new ArgumentException($"The specified file at '{pathToFile}' could not be found.");
			}

			using (var reader = new System.IO.FileStream(pathToFile, System.IO.FileMode.Open))
			{
				using (var dataTable = DataTableExtensions.CsvToDataTable(reader))
				{
					_documentsProductionImportStrategy.Import(workspaceId, productionId, dataTable, options);
				}
			}
		}
	}
}
