using System;
using System.Data;
using System.IO;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentSingleImageImportStrategy : IDocumentSingleImageImportStrategy
	{
		private readonly IDocumentsImageImportStrategy _documentsImageImportStrategy;

		public DocumentSingleImageImportStrategy(IDocumentsImageImportStrategy documentsImageImportStrategy)
		{
			_documentsImageImportStrategy = documentsImageImportStrategy;
		}

		public void Import(int workspaceId, string pathToFile)
		{
			if (!File.Exists(pathToFile))
			{
				throw new ArgumentException($"The specified file at '{pathToFile}' could not be found.");
			}

			using (var sourceData = GenerateImageDocumentSourceData(pathToFile))
			{
				_documentsImageImportStrategy.Import(workspaceId, sourceData);
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
