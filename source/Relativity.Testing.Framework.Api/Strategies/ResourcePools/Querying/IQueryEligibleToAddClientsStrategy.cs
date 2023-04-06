using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IQueryEligibleToAddClientsStrategy
	{
		/// <summary>
		/// Gets a query for all clients that may be set as the client for a resource pool.
		/// </summary>
		/// <returns>The <see cref="ResourcePoolQuery{Client}"/> object.</returns>
		ResourcePoolQuery<Client> Query();
	}
}
