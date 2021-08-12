﻿using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Strategies.Tabs.DTO;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the tab API service for interacting with Tabs.
	/// </summary>
	/// <example>
	/// <code>
	/// _tabService = relativityFacade.Resolve&lt;ITabService&gt;();
	/// </code>
	/// </example>
	public interface ITabService
	{
		/// <summary>
		/// Creates the specified tab.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The <see cref="Tab"/> to create.</param>
		/// <returns>The created <see cref="Tab"/>.</returns>
		/// <example>
		/// <code>
		/// Tab tab = _tabService.Create(-1, new Tab());
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// Tab tab = new Tab
		/// {
		///     Name = "MySpecialTab",
		///     LinkType = TabLinkType.Parent
		/// };
		/// Tab tab = _tabService.Create(1015247, tab);
		/// </code>
		/// </example>
		Tab Create(int workspaceId, Tab entity);

		/// <summary>
		/// Requires the specified tab.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</item>
		/// <item>If <see cref="NamedArtifact.Name"/> property of <paramref name="entity"/> have a value, gets entity by name and updates it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to require tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The <see cref="Tab"/> to require.</param>
		/// <returns>The <see cref="Tab"/> required.</returns>
		/// <example>
		/// In this example, the tab does not exist, so it will be created.
		/// <code>
		/// Tab tab = new Tab
		/// {
		///     Name = "MySpecialTab",
		///     LinkType = TabLinkType.Parent
		/// };
		/// Tab tab = _tabService.Require(1015247, tab);
		/// </code>
		/// However, if the tab did exist, it would be updated to match the definition.
		/// </example>
		Tab Require(int workspaceId, Tab entity);

		/// <summary>
		/// Deletes the tab by ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the <see cref="Tab"/>.</param>
		/// <example>
		/// <code>
		/// _tabService.Require(1015247, tab.ArtifactId);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the tab by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the <see cref="Tab"/>.</param>
		/// <returns>>The <see cref="Tab"/> entity.</returns>
		/// <example>
		/// <code>
		/// Tab tab = _tabService.Get(1015247, tab.ArtifactId);
		/// </code>
		/// </example>
		Tab Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the tab by the specified name.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityName">The name of the tab.</param>
		/// <returns>>The <see cref="Tab"/> entity.</returns>
		/// <example>
		/// <code>
		/// Tab tab = _tabService.Get(1015247, "MyTabName");
		/// </code>
		/// </example>
		Tab Get(int workspaceId, string entityName);

		/// <summary>
		/// Updates the specified tab.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The <see cref="Tab"/> to update.
		/// Must include the ArtifactID of the Tab in the model.</param>
		/// <example>
		/// <code>
		/// Tab tab = _tabService.Get(1015247, "MyTabName");
		///
		/// tab.Name = "AnotherName"
		///
		/// tab = _tabService.Update(1015247, tab);
		/// </code>
		/// </example>
		void Update(int workspaceId, Tab entity);

		/// <summary>
		/// Retrieves a list of all object types in a workspace available for creating or updating a tab.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that you want to retrieve available object types for.</param>
		/// <returns>>List of <see cref="ObjectType"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1234567;
		/// List&lt;ObjectType&gt; result = _tabService.GetAvailableObjectTypes(workspaceId);
		/// </code>
		/// </example>
		List<ObjectType> GetAvailableObjectTypes(int workspaceId);

		/// <summary>
		/// Gets the <see cref="Meta"/> with admin-level metadata about admin and system tabs.
		/// </summary>
		/// <example>
		/// <code>
		/// Meta meta = _tabService.GetAdminLevelMetadata();
		/// </code>
		/// </example>
		/// <returns>The <see cref="Meta"/> entitiy.</returns>
		Meta GetAdminLevelMetadata();

		/// <summary>
		/// Retrieves a list of parent tabs, which can be associated with a tab for adding or updating it.
		/// </summary>
		/// /// <param name="workspaceId">The Artifact ID of the workspace that you want to retrieve parent tabs for.</param>
		/// <example>
		/// <code>
		/// int workspaceId = 1234567;
		/// List&lt;TabEligibleParentV1&gt; result = _tabService.GetEligibleParents(workspaceId);
		/// </code>
		/// </example>
		/// <returns>List of <see cref="TabEligibleParentV1"/> entities.</returns>
		List<TabEligibleParentV1> GetEligibleParents(int workspaceId);
	}
}
