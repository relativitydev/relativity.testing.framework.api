namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IDocumentSingleImageImportStrategy
	{
		/// <summary>
		/// Import single image document by specified path.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="pathToFile">The path to the existing native file.</param>
		void Import(int workspaceId, string pathToFile);
	}
}
