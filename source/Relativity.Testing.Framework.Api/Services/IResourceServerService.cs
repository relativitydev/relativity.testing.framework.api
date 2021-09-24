using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the resource server API service.
	/// </summary>
	/// <example>
	/// <code>
	/// IResourceServerService _resourceServerService = RelativityFacade.Resolve&lt;IResourceServerService&gt;();
	/// </code>
	/// </example>
	public interface IResourceServerService
	{
		/// <summary>
		/// Gets all resource servers.
		/// </summary>
		/// <returns>The collection of <see cref="ResourceServer"/> entities.</returns>
		/// <example>
		/// <code>
		/// ResourceServer[] result = _resourceServerService.GetAll();
		/// </code>
		/// </example>
		ResourceServer[] GetAll();
	}
}
