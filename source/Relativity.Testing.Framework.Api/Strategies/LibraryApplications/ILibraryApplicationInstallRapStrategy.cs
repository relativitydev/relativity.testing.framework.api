using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface ILibraryApplicationInstallRapStrategy
	{
		int InstallToLibrary(string pathToRap, LibraryApplicationInstallOptions options = null);
	}
}
