using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the workspace API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _workspaceService = relativityFacade.Resolve&lt;IWorkspaceService&gt;();
	/// </code>
	/// </example>
	public interface IWorkspaceService
	{
		/// <summary>
		/// Creates the specified [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html).
		/// If properties are not specified in the entity, the first value found for each property will be used.
		/// It is the responsibility of the user writing/running the tests to ensure that they are adhering to all environment related best practices and rules.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		/// <example>
		/// Create any old workspace.
		/// <code>
		/// Workspace workspace = _workspaceService.Create(Workspace());
		/// </code>
		/// </example>
		/// <example>
		/// Create a workspace with specific properties.
		/// <code>
		/// Client client = new Client
		/// {
		///     Name = "MI6"
		/// };
		/// client = _clientService.Create(client);
		///
		/// Matter matter = new Matter
		/// {
		///     Name = "Moonraker",
		///     Client = client
		/// };
		/// matter = _matterService.Create(matter);
		///
		/// Workspace topSecretWorkspace = new Workspace
		/// {
		///     Name = "Top Secret [Don't Delete] [Don't Look Inside] [Don't Even Read This]",
		///     Client = client,
		///     Matter = matter
		/// };
		/// topSecretWorkspace = _workspaceService.Create(workspace);
		/// </code>
		/// </example>
		/// <example>
		/// Create a workspace that uses a specific base template.
		/// <code>
		/// Workspace workspace = new Workspace
		/// {
		///     Name = "MyNewWorkspace",
		///     TemplateWorkspace = new Workspace { Name = "MyOldWorkspace" }
		/// };
		///
		/// Workspace workspace = _workspaceService.Create(workspace);
		/// </code>
		/// </example>
		/// <example>
		/// <para>Create a workspace that is compliant with the configuration of the Regression environments (at least as of 2021/08/05).</para>
		/// <para>Note that the dependent artifacts are assumed to exist on the Regression enviornments.
		/// If they do not exist, the environment is probably not intended/ready to be tested against.</para>
		/// <code>
		/// string applicationName = "YourApplicationNameHere";
		///
		/// Client aeroClient = _clientService.Get("Aero CI/CD");
		/// Matter aeroMatter = _matterService.Get($"{applicationName} CI/CD", aeroClient.ArtifactID);
		/// Group aeroGroup = _groupService.Get($"{applicationName} CI/CD");
		/// Workspace templateWorkspace = new Workspace { Name = "Base Template" };
		/// ResourcePool aeroResourcePool = _objectService.GetAll&lt;ResourcePool&gt;().Where(x => x.Name == "RelativityOne Pool").First();
		///
		/// Workspace workspace = new Workspace
		/// {
		///     Name = $"{applicationName} - Aero CD",
		///     Client = client,
		///     Matter = matter,
		///     Status = "Active",
		///     TemplateWorkspace = templateWorkspace,
		///     WorkspaceAdminGroup = adminGroup,
		///     ResourcePool = resourcePool,
		///     SqlServer = resourcePool.SqlServers.First(),
		///     DefaultCacheLocation = resourcePool.CacheLocationServers.First(),
		///     DefaultFileRepository = resourcePool.FileRepositories.First(),
		///     DownloadHandlerUrl = "Relativity.Distributed"
		/// };
		///
		/// Workspace workspace = _workspaceService.Create(workspace);
		/// </code>
		/// </example>
		Workspace Create(Workspace entity);

		/// <summary>
		/// Creates the specified [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html) with generated documents.
		/// If properties are not specified in the entity, the first value found for each property will be used.
		/// It is the responsibility of the user writing/running the tests to ensure that they are adhering to all environment related best practices and rules.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <param name="numberOfDocuments">Number of documents to create.</param>
		/// <returns>The created entity.</returns>
		/// <example>
		/// <code>
		/// Workspace workspace = _workspaceService.CreateWithDocs(Workspace(), 100);
		/// </code>
		/// </example>
		Workspace CreateWithDocs(Workspace entity, int numberOfDocuments = 10);

		/// <summary>
		/// Updates the specified [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html).
		/// </summary>
		/// <param name="entity">The workspace to update.</param>
		/// <example>
		/// <code>
		/// int workspaceId = 1234;
		/// Workspace workspace = _workspaceService.Get(workspaceId);
		/// workspace.Name = "updatedName";
		/// _workspaceService.Update(workspace);
		/// </code>
		/// </example>
		void Update(Workspace entity);

		/// <summary>
		/// Delete the [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html) by ArtifactID.
		/// </summary>
		/// <param name="id">The ArtifactID of the workspace to delete.</param>
		/// <example>
		/// <code>
		/// _workspaceService.Delete(1234567);
		/// </code>
		/// </example>
		void Delete(int id);

		/// <summary>
		/// Gets the [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html) by ArtifactID.
		/// </summary>
		/// <param name="id">The ArtifactID of the workspace to get.</param>
		/// <returns>The [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html) if it exists, otherwise <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// Workspace workspace = _workspaceService.Get(1234567);
		/// </code>
		/// </example>
		Workspace Get(int id);

		/// <summary>
		/// Gets the [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html) by the specified name.
		/// </summary>
		/// <param name="name">The name of the workspace to get.</param>
		/// <returns>The [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html) if it exists, otherwise <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// Workspace lowSecretWorkspace = _workspaceService.Get("Low Secret[Feel Free To Look Inside] [But Still Don't Delete]");
		/// </code>
		/// </example>
		Workspace Get(string name);

		/// <summary>
		/// Get all [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html)s in the environment.
		/// </summary>
		/// <returns>An array of [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html)s.</returns>
		/// <example>
		/// <code>
		/// Workspace[] workspace = _workspaceService.GetAll();
		/// </code>
		/// </example>
		Workspace[] GetAll();

		/// <summary>
		/// Gets all [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html)s that are associated with the specified client.
		/// </summary>
		/// <param name="clientName">The client name.</param>
		/// <returns>The collection of [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html) entities.</returns>
		/// <example>
		/// <code>
		/// List&lt;Workspace&gt; workspace = _workspaceService.GetAllByClientName(existingClientName).ToList();
		/// </code>
		/// </example>
		IEnumerable<Workspace> GetAllByClientName(string clientName);

		/// <summary>
		/// Determines whether the [Workspace](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Workspace.html) with the specified ArtifactID exists.
		/// </summary>
		/// <param name="id">The ArtifactID of the workspace to check the existence of.</param>
		/// <returns><see langword="true"/> if the workspace exists, otherwise <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// bool workspaceExists = _workspaceService.Exists(1234567);
		/// </code>
		/// </example>
		bool Exists(int id);
	}
}
