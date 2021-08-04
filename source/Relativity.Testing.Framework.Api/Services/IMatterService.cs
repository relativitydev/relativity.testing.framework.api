using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the matter API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _matterService = relativityFacade.Resolve&lt;IMatterService&gt;();
	/// </code>
	/// </example>
	public interface IMatterService
	{
		/// <summary>
		/// Creates the specified <see cref="Matter"/>.
		/// </summary>
		/// <param name="entity">The matter to create.</param>
		/// <returns>The created matter.</returns>
		/// <example>
		/// Create any old matter.
		/// <code>
		/// Matter matter = _matterService.Create(Matter());
		/// </code>
		/// </example>
		/// <example>
		/// Create a matter with specified properties.
		/// <code>
		/// Client client = _clientService.Create(Client());
		///
		/// Matter matter = new Matter
		/// {
		///     Name = "Dark",
		///     Number = 12345,
		///     Status = "Active",
		///     Client = client
		///     Keywords = "SomeKeyword"
		///     Notes = "Some note about the matter."
		/// };
		/// matter = _matterService.Create(matter);
		/// </code>
		/// </example>
		Matter Create(Matter entity);

		/// <summary>
		/// Requires the specified matter.
		/// <list type="number">
		/// <item>If the <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has a positive value, this gets the matter by ID and updates it.</item>
		/// <item>Else if the <see cref="NamedArtifact.Name"/> property of <paramref name="entity"/> have a value, this gets the matter by name and client ID and updates it, if it exists.</item>
		/// <item>Otherwise this creates a new matter using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <param name="entity">The matter to require.</param>
		/// <returns>The created or updated matter.</returns>
		/// <example>
		/// <para>This example will search for a matter with the ArtifactID "1234567".</para>
		/// <para>If it is found, the model will be used to update the rest of the properties on the matter.</para>
		/// <para>If not, it will be created.</para>
		/// <code>
		/// Matter matter = new Matter
		/// {
		///     ArtifactID = 1234567,
		///     Status = "Inactive"
		/// };
		/// matter = _matterService.Require(matter);
		/// </code>
		/// </example>
		/// <example>
		/// <para>This example will search for a matter named "MyMatter".</para>
		/// <para>If one is found, it will be updated and returned.</para>
		/// <para>If not, it will be created.</para>
		/// <code>
		/// Client client = new Client
		/// {
		///     Name = "MyClient"
		/// };
		///
		/// Client client = _clientService.Require(client);
		///
		/// Matter matter = new Matter
		/// {
		///     Name = "MyMatter",
		///     Client = client,
		///     Status = "Inactive"
		/// };
		///
		/// matter = _matterService.Require(matter);
		/// </code>
		/// </example>
		Matter Require(Matter entity);

		/// <summary>
		/// Deletes the <see cref="Matter"/> by ArtifactID.
		/// </summary>
		/// <param name="id">The ArtifactID of the matter.</param>
		/// <example>
		/// <code>
		/// _matterService.Delete(matter.ArtifactID);
		/// </code>
		/// </example>
		void Delete(int id);

		/// <summary>
		/// Gets the <see cref="Matter"/> by the ArtifactID.
		/// </summary>
		/// <param name="id">The ArtifactID of the matter.</param>
		/// <returns>The <see cref="Matter"/> if it exists, otherwise <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// Matter matter = _matterService.Get(1234567);
		/// </code>
		/// </example>
		Matter Get(int id);

		/// <summary>
		/// Gets the <see cref="Matter"/> by the matter name, and Client ArtifactID.
		/// </summary>
		/// <param name="name">The name of the matter.</param>
		/// <param name="clientId">The ArtifactID of the client.</param>
		/// <returns>The <see cref="Matter"/> if it exists, otherwise <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// Matter matter = _matterService.Get("AnotherMatter", 2345678);
		/// </code>
		/// </example>
		Matter Get(string name, int clientId);

		/// <summary>
		/// Updates the specified <see cref="Matter"/>.
		/// </summary>
		/// <param name="entity">The matter to update.</param>
		/// <example>
		/// <code>
		/// Matter matter = _matterService.Get("AnotherMatter", 2345678);
		///
		/// matter.Name = "ADifferentMatter";
		///
		/// matter = _matterService.Update(matter);
		/// </code>
		/// </example>
		void Update(Matter entity);
	}
}
