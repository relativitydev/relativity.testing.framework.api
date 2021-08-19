using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the tab API service for interacting with Tabs.
	/// </summary>
	/// <example>
	/// <code>
	/// ITabService _tabService = relativityFacade.Resolve&lt;ITabService&gt;();
	/// </code>
	/// </example>
	public interface ITabService
	{
		/// <summary>
		/// Creates the specified tab.
		/// </summary>
		/// <param name="workspaceArtifactID">The Artifact ID of the workspace where you want to add the new tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="tab">The <see cref="Tab"/> to create.</param>
		/// <returns>The created <see cref="Tab"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = -1;
		/// Tab tab = _tabService.Create(workspaceArtifactID, new Tab());
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// Tab tab = new Tab
		/// {
		///     Name = "MySpecialTab",
		///     LinkType = TabLinkType.Parent
		/// };
		/// Tab tab = _tabService.Create(workspaceArtifactID, tab);
		/// </code>
		/// </example>
		Tab Create(int workspaceArtifactID, Tab tab);

		/// <summary>
		/// Requires the specified tab.
		/// <list type="number">
		/// <item><description>If <see cref="Artifact.ArtifactID"/> property of <paramref name="tab"/> has positive value, gets entity by ID and updates it.</description></item>
		/// <item><description>If <see cref="NamedArtifact.Name"/> property of <paramref name="tab"/> have a value, gets entity by name and updates it if it exists.</description></item>
		/// <item><description>Otherwise creates a new <see cref="Tab"/>.</description></item>
		/// </list>
		/// </summary>
		/// <param name="workspaceArtifactID">The Artifact ID of the workspace where you want to require tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="tab">The <see cref="Tab"/> to require.</param>
		/// <returns>The <see cref="Tab"/> required.</returns>
		/// <example>
		/// In this example, the tab does not exist, so it will be created.
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// Tab tab = new Tab
		/// {
		///     Name = "MySpecialTab",
		///     LinkType = TabLinkType.Parent
		/// };
		/// Tab tab = _tabService.Require(workspaceArtifactID, tab);
		/// </code>
		/// However, if the tab did exist, it would be updated to match the definition.
		/// </example>
		Tab Require(int workspaceArtifactID, Tab tab);

		/// <summary>
		/// Deletes the <see cref="Tab"/> by ID.
		/// </summary>
		/// <param name="workspaceArtifactID">The Artifact ID of the workspace where you want to delete the tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="tabArtifactId">The artifact ID of the <see cref="Tab"/>.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// _tabService.Delete(workspaceArtifactID, tab.ArtifactId);
		/// </code>
		/// </example>
		void Delete(int workspaceArtifactID, int tabArtifactId);

		/// <summary>
		/// Gets the <see cref="Tab"/> by the specified ID.
		/// </summary>
		/// <param name="workspaceArtifactID">The Artifact ID of the workspace where you want to get the tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="tabArtifactId">The artifact ID of the <see cref="Tab"/>.</param>
		/// <returns>>The <see cref="Tab"/> entity.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// Tab tab = _tabService.Get(workspaceArtifactID, tab.ArtifactId);
		/// </code>
		/// </example>
		Tab Get(int workspaceArtifactID, int tabArtifactId);

		/// <summary>
		/// Gets the <see cref="Tab"/> by the specified name.
		/// </summary>
		/// <param name="workspaceArtifactID">The Artifact ID of the workspace where you want to get the tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="tabName">The name of the tab.</param>
		/// <returns>>The <see cref="Tab"/> entity.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// Tab tab = _tabService.Get(workspaceArtifactID, "MyTabName");
		/// </code>
		/// </example>
		Tab Get(int workspaceArtifactID, string tabName);

		/// <summary>
		/// Updates the specified <see cref="Tab"/>.
		/// </summary>
		/// <param name="workspaceArtifactID">The Artifact ID of the workspace where you want to update the tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="tab">The <see cref="Tab"/> to update.
		/// Must include the ArtifactID of the Tab in the model.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// Tab tab = _tabService.Get(workspaceArtifactID, "MyTabName");
		///
		/// tab.Name = "AnotherName"
		///
		/// _tabService.Update(workspaceArtifactID, tab);
		/// </code>
		/// </example>
		void Update(int workspaceArtifactID, Tab tab);

		/// <summary>
		/// Retrieves a list of all object types in a workspace available for creating or updating a tab.
		/// </summary>
		/// <param name="workspaceArtifactID">The Artifact ID of the workspace that you want to retrieve available object types for.</param>
		/// <returns>>List of <see cref="ObjectType"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// List&lt;ObjectType&gt; result = _tabService.GetAvailableObjectTypes(workspaceArtifactID);
		/// </code>
		/// </example>
		List<ObjectType> GetAvailableObjectTypes(int workspaceArtifactID);

		/// <summary>
		/// Gets the <see cref="Meta"/> with admin-level metadata about admin and system tabs.
		/// </summary>
		/// <example>
		/// <code>
		/// Meta meta = _tabService.GetAdminLevelMetadata();
		/// </code>
		/// </example>
		/// <returns>The <see cref="Meta"/> entity.</returns>
		Meta GetAdminLevelMetadata();

		/// <summary>
		/// Retrieves a list of parent tabs, which can be associated with a tab for adding or updating it.
		/// </summary>
		/// /// <param name="workspaceArtifactID">The Artifact ID of the workspace that you want to retrieve parent tabs for.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// List&lt;TabEligibleParentV1&gt; result = _tabService.GetEligibleParents(workspaceArtifactID);
		/// </code>
		/// </example>
		/// <returns>List of <see cref="TabEligibleParent"/> entities.</returns>
		List<TabEligibleParent> GetEligibleParents(int workspaceArtifactID);

		/// <summary>
		/// Gets current order of the tabs in a workspace.
		/// </summary>
		/// <param name="workspaceArtifactID">The Artifact ID of the workspace that you want to retrieve tabs order for.</param>
		/// <returns>Ordered list of <see cref="Tab"/>s. Only basic information and Order fields are filled.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// List&lt;Tab&gt; = _tabService.GetTabsOrder(workspaceArtifactID);
		/// </code>
		/// </example>
		List<Tab> GetTabsOrder(int workspaceArtifactID);

		/// <summary>
		/// Retrieves a list of tabs with information about each tab that the calling user can navigate to in a specific workspace.
		/// </summary>
		/// <param name="workspaceArtifactID">The Artifact ID of the workspace that you want to retrieve tabs navigation information for.</param>
		/// <returns>List of <see cref="Tab"/> entities.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// List&lt;Tab&gt; result = _tabService.GetAllForNavigation(workspaceArtifactID);
		/// </code>
		/// </example>
		List<Tab> GetAllForNavigation(int workspaceArtifactID);
	}
}
