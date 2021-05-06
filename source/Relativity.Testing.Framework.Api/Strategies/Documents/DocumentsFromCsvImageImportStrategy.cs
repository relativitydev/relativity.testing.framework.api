using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentsFromCsvImageImportStrategy : IDocumentsFromCsvImageImportStrategy
	{
		private readonly IDocumentsImageImportStrategy _documentsImageImportStrategy;

		public DocumentsFromCsvImageImportStrategy(IDocumentsImageImportStrategy documentsImageImportStrategy)
		{
			_documentsImageImportStrategy = documentsImageImportStrategy;
		}

		public void Import(int workspaceId, string pathToFile, ImageImportOptions options = null)
		{
			if (!System.IO.File.Exists(pathToFile))
			{
				throw new ArgumentException($"The specified file at '{pathToFile}' could not be found.");
			}

			using (var reader = new System.IO.FileStream(pathToFile, System.IO.FileMode.Open))
			{
				using (var dataTable = DataTableExtensions.CsvToDataTable(reader))
				{
					_documentsImageImportStrategy.Import(workspaceId, dataTable, options);
				}
			}
		}
	}
}
