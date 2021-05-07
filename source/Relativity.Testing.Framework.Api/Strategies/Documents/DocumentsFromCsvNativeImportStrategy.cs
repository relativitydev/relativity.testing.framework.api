using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentsFromCsvNativeImportStrategy : IDocumentsFromCsvNativeImportStrategy
	{
		private readonly IDocumentsNativeImportStrategy _documentsNativeImportStrategy;

		public DocumentsFromCsvNativeImportStrategy(IDocumentsNativeImportStrategy documentsNativeImportStrategy)
		{
			_documentsNativeImportStrategy = documentsNativeImportStrategy;
		}

		public void Import(int workspaceId, string pathToFile, NativeImportOptions options = null)
		{
			if (!System.IO.File.Exists(pathToFile))
			{
				throw new ArgumentException($"The specified file at '{pathToFile}' could not be found.");
			}

			using (var reader = new System.IO.FileStream(pathToFile, System.IO.FileMode.Open))
			{
				using (var dataTable = DataTableExtensions.CsvToDataTable(reader))
				{
					_documentsNativeImportStrategy.Import(workspaceId, dataTable, options);
				}
			}
		}
	}
}
