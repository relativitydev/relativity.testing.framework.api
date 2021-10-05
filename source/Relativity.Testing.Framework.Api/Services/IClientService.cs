using System.Collections.Generic;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the client API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _clientService = relativityFacade.Resolve&lt;IClientService&gt;();
	/// </code>
	/// </example>
	public interface IClientService
	{
		/// <summary>
		/// Creates the specified client.
		/// </summary>
		/// <param name="entity">The [Client](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Client.html) to create.</param>
		/// <returns>The created client.</returns>
		/// <example>
		/// Create any old client.
		/// <code>
		/// Client client = _clientService.Create(Client());
		/// </code>
		/// </example>
		/// <example>
		/// Create a client with specified properties.
		/// <code>
		/// Client client = new Client
		/// {
		///     Name = "TheBigClient",
		///     Number = 12345,
		///     Status = new NamedArtifact { Name = ClientStatus.Inactive.ToString() },
		///     Keywords = "SomeKeyword"
		///     Notes = "Some note about the client."
		/// };
		/// client = _clientService.Create(client);
		/// </code>
		/// </example>
		Client Create(Client entity);

		/// <summary>
		/// Updates the specified [Client](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Client.html).
		/// </summary>
		/// <param name="entity">The [Client](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Client.html) model to use to update the existing client with.</param>
		/// <example>
		/// <code>
		/// Client client = _clientService.Get("Some Client Name");
		/// client.Keywords = "SampleKeywords";
		/// _clientService.Update(client);
		/// </code>
		/// </example>
		void Update(Client entity);

		/// <summary>
		/// Requires the specified client.
		/// <list type="number">
		/// <item>If the [ArtifactID](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Artifact.html#Relativity_Testing_Framework_Models_Artifact_ArtifactID) property of <paramref name="entity"/> has a positive value, this gets the client by ID and returns it.</item>
		/// <item>Else if the [Name](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.NamedArtifact.html#Relativity_Testing_Framework_Models_NamedArtifact_Name) property of <paramref name="entity"/> has a value, this gets the client by name and returns it if it exists.</item>
		/// <item>Otherwise this creates a new client using [ICreateStrategy&lt;T&gt;](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Strategies.ICreateStrategy-1.html).</item>
		/// </list>
		/// </summary>
		/// <param name="entity">The [Client](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Client.html) to require.</param>
		/// <returns>The already existing, or created [Client](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Client.html).</returns>
		/// <example>
		/// <para>This example will search for a client with the ArtifactID "1234567".</para>
		/// <para>If it is found, it will be returned.</para>
		/// <para>If not, it will be created.</para>
		/// <code>
		/// Client client = new Client
		/// {
		///     ArtifactID = 1234567
		/// };
		/// client = _clientService.Require(client);
		/// </code>
		/// </example>
		/// <example>
		/// <para>This example will search for a client named "MyClient".</para>
		/// <para>If one is found, it will be returned.</para>
		/// <para>If not, it will be created.</para>
		/// <code>
		/// Client client = new Client
		/// {
		///     Name = "MyClient"
		/// };
		/// client = _clientService.Require(client);
		/// </code>
		/// </example>
		Client Require(Client entity);

		/// <summary>
		/// Deletes the [Client](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Client.html) by ID.
		/// </summary>
		/// <param name="id">The ArtifactID of the client.</param>
		/// <example>
		/// <code>
		/// _clientService.Delete(client.ArtifactID);
		/// </code>
		/// </example>
		void Delete(int id);

		/// <summary>
		/// Gets the [Client](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Client.html) by ArtifactID.
		/// </summary>
		/// <param name="id">The ArtifactID of the client.</param>
		/// <returns>The [Client](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Client.html) if it exists, else <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// Client client = _clientService.Get(1234567);
		/// </code>
		/// </example>
		Client Get(int id);

		/// <summary>
		/// Gets the [Client](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Client.html) by name.
		/// </summary>
		/// <param name="name">The name of the client.</param>
		/// <returns>The [Client](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Client.html) if it exists, else <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// Client client = _clientService.Get("AnotherClient");
		/// </code>
		/// </example>
		Client Get(string name);

		/// <summary>
		/// Gets a list of available statuses for a [Client](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Client.html).
		/// </summary>
		/// <returns>List of of available statuses.</returns>
		/// <example>
		/// <code>
		/// IEnumerable&lt;NamedArtifact&gt; availableStatuses = _clientService.GetEligibleStatuses();
		/// </code>
		/// </example>
		IEnumerable<NamedArtifact> GetEligibleStatuses();
	}
}
