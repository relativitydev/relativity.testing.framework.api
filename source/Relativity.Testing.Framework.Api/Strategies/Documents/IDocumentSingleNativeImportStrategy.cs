namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IDocumentSingleNativeImportStrategy
	{
		/// <summary>
		/// Import single native document by specified path.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="pathToFile">The path to the existing native file.</param>
		void Import(int workspaceId, string pathToFile);
	}
}
