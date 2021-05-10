namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IDocumentSingleProducedImageImportStrategy
	{
		/// <summary>
		/// Import single produced image document by specified path.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="productionId">The production ID.</param>
		/// <param name="pathToFile">The path to the existing file.</param>
		void Import(int workspaceId, int productionId, string pathToFile);
	}
}
