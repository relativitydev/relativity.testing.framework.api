using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of installation library application from RAP.
	/// </summary>
	internal interface ILibraryApplicationInstallRapStrategy
	{
		/// <summary>
		/// Installs library application from RAP file.
		/// </summary>
		/// <param name="pathToRap">Path to the RAP file.</param>
		/// <param name="options">Represents a request for creating or updating a Library Application.</param>
		/// <returns>Artifact ID of installed application.</returns>
		int InstallToLibrary(string pathToRap, LibraryApplicationInstallOptions options = null);
	}
}
