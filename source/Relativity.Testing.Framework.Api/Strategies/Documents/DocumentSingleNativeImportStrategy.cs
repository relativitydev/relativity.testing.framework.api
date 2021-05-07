using System;
using System.Data;
using System.IO;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentSingleNativeImportStrategy : IDocumentSingleNativeImportStrategy
	{
		private readonly IDocumentsNativeImportStrategy _documentsNativeImportStrategy;

		public DocumentSingleNativeImportStrategy(IDocumentsNativeImportStrategy documentsNativeImportStrategy)
		{
			_documentsNativeImportStrategy = documentsNativeImportStrategy;
		}

		public void Import(int workspaceId, string pathToFile)
		{
			if (!File.Exists(pathToFile))
			{
				throw new ArgumentException($"The specified file at '{pathToFile}' could not be found.");
			}

			using (var sourceData = GenerateNativeDocumentSourceData(pathToFile))
			{
				_documentsNativeImportStrategy.Import(workspaceId, sourceData);
			}
		}

		private static DataTable GenerateNativeDocumentSourceData(string path)
		{
			var dataTable = new DataTable("Documents");
			var options = new NativeImportOptions();

			dataTable.Columns.Add(options.DocumentIdentifierField);
			dataTable.Columns.Add(options.NativeFilePathColumnName);

			DataRow dataRow = dataTable.NewRow();
			dataRow[options.DocumentIdentifierField] = Path.GetFileName(path);
			dataRow[options.NativeFilePathColumnName] = path;
			dataTable.Rows.Add(dataRow);

			return dataTable;
		}
	}
}
