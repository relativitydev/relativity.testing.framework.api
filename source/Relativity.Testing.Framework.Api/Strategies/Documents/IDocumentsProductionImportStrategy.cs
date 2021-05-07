using System.Data;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IDocumentsProductionImportStrategy
	{
		/// <summary>
		/// Import produced image documents from data table object.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="productionId">The production ID.</param>
		/// <param name="dataTable">The data table object.</param>
		/// <param name="options">The options for document import.</param>
		void Import(int workspaceId, int productionId, DataTable dataTable, ImageImportOptions options = null);
	}
}
