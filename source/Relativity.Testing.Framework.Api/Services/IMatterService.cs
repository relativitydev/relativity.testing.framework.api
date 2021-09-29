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
		/// Creates the specified [Matter](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Matter.html).
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
		/// <item>If the [Artifact.ArtifactID](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Artifact.html#Relativity_Testing_Framework_Models_Artifact_ArtifactID) property of <paramref name="entity"/> has a positive value, this gets the matter by ID and updates it.</item>
		/// <item>Else if the [NamedArtifact.Name](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.NamedArtifact.html#Relativity_Testing_Framework_Models_NamedArtifact_Name) property of <paramref name="entity"/> have a value, this gets the matter by name and client ID and updates it, if it exists.</item>
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
		/// Deletes the [Matter](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Matter.html) by ArtifactID.
		/// </summary>
		/// <param name="id">The ArtifactID of the matter.</param>
		/// <example>
		/// <code>
		/// _matterService.Delete(matter.ArtifactID);
		/// </code>
		/// </example>
		void Delete(int id);

		/// <summary>
		/// Gets the [Matter](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Matter.html) by the ArtifactID.
		/// </summary>
		/// <param name="id">The ArtifactID of the matter.</param>
		/// <param name="withExtendedMetadata">If set to true Meta field will be populated on get. Default is false.</param>
		/// <returns>The [Matter](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Matter.html) if it exists, otherwise <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// Matter matter = _matterService.Get(1234567);
		/// </code>
		/// </example>
		Matter Get(int id, bool withExtendedMetadata = false);

		/// <summary>
		/// Gets the [Matter](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Matter.html) by the matter name, and Client ArtifactID.
		/// </summary>
		/// <param name="name">The name of the matter.</param>
		/// <param name="clientId">The ArtifactID of the client.</param>
		/// <returns>The [Matter](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Matter.html) if it exists, otherwise <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// Matter matter = _matterService.Get("AnotherMatter", 2345678);
		/// </code>
		/// </example>
		Matter Get(string name, int clientId);

		/// <summary>
		/// Updates the specified [Matter](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Matter.html).
		/// </summary>
		/// <param name="entity">The matter to update.</param>
		/// <param name="restrictedUpdate">If set to true the LastModifiedOn date will be added to API request and its value must match the LastModifiedOn date for the matter stored in Relativity. Otherwise, you receive a 409 error, indicating that the object has been modified. Default set to false.</param>
		/// <example>
		/// <code>
		/// Matter matter = _matterService.Get("AnotherMatter", 2345678);
		///
		/// matter.Name = "ADifferentMatter";
		///
		/// matter = _matterService.Update(matter);
		/// </code>
		/// </example>
		void Update(Matter entity, bool restrictedUpdate = false);

		/// <summary>
		/// Gets all of available clients in a Relativity environment.
		/// </summary>
		/// <returns>The array with pairs of Names and Artifact IDs of available clients.</returns>
		/// <example>
		/// <code>
		/// ArtifactIdNamePair[] allClients = _matterService.GetEligibleClients();
		/// </code>
		/// </example>
		ArtifactIdNamePair[] GetEligibleClients();

		/// <summary>
		/// Gets all of available matter statuses in a Relativity environment.
		/// </summary>
		/// <returns>The array with pairs of Names and Artifact IDs of available statuses.</returns>
		/// <example>
		/// <code>
		/// ArtifactIdNamePair[] allStatuses = _matterService.GetEligibleStatuses();
		/// </code>
		/// </example>
		ArtifactIdNamePair[] GetEligibleStatuses();
	}
}
