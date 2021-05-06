using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IDocumentsFromCsvProductionImportStrategy
	{
		/// <summary>
		/// Import produced image documents from comma-separated values file.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="productionId">The production ID.</param>
		/// <param name="pathToFile">The path to the existing comma-separated values file.</param>
		/// <param name="options">The options for document import.</param>
		void Import(int workspaceId, int productionId, string pathToFile, ImageImportOptions options = null);
	}
}
