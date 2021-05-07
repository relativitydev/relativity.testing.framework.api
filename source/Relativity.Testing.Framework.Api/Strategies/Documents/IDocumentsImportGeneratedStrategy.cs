namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IDocumentsImportGeneratedStrategy
	{
		/// <summary>
		/// Creates and import basic document metadata to import to the given workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="numberOfDocuments">The number of documents to generate and import.</param>
		void Import(int workspaceId, int numberOfDocuments = 10);
	}
}
