using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the resource server API service.
	/// </summary>
	public interface IResourceServerService
	{
		/// <summary>
		/// Gets all resource servers.
		/// </summary>
		/// <returns>The collection of <see cref="ResourceServer"/> entities.</returns>
		ResourceServer[] GetAll();
	}
}
