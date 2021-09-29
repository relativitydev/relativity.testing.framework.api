using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represent the API view service.
	/// </summary>
	/// <example>
	/// <code>
	/// IViewService _viewService = relativityFacade.Resolve&lt;IViewService&gt;();
	/// </code>
	/// </example>
	public interface IViewService
	{
		/// <summary>
		///  Creates the specified [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html).
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace where you want to create the view,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="view">The [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html) to create.</param>
		/// <returns>The [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// FixedLengthTextField field = relativityFacade.Resolve&lt;IFieldService&gt;().Get&lt;FixedLengthTextField&gt;(workspaceArtifactID, "Control Number");
		/// View viewtoCreate = new View
		/// {
		/// 	Name = "Test View Name",
		/// 	Fields = new[] { new NamedArtifact { Name = field.Name, ArtifactID = field.ArtifactID } },
		/// }
		/// View createdView = _viewService.Create(workspaceArtifactID, viewtoCreate);
		/// </code>
		/// </example>
		View Create(int workspaceArtifactID, View view);

		/// <summary>
		/// Gets all [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html)s in workspace.
		/// Returns not all fields for faster response.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace where you want to get the view,
		/// or use -1 to indicate the admin-level context.</param>
		/// <returns>The collection of [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html) entities.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// View[] views = _viewService.GetAll(workspaceArtifactID);
		/// </code>
		/// </example>
		View[] GetAll(int workspaceArtifactID);

		/// <summary>
		/// Gets the [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html) by the specified ArtifactID.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace where you want to get the view,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="viewArtifactID">The ArtifactID of the view.</param>
		/// <returns>The [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int viewArtifactID = 1024345;
		/// View view = _viewService.Get(workspaceArtifactID, viewArtifactID);
		/// </code>
		/// </example>
		View Get(int workspaceArtifactID, int viewArtifactID);

		/// <summary>
		/// Gets the [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html) by the specified name.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace where you want to get the view,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="viewName">The name of the view.</param>
		/// <returns>The [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int viewName = "Test View Name";
		/// View view = _viewService.Get(workspaceArtifactID, viewName);
		/// </code>
		/// </example>
		View Get(int workspaceArtifactID, string viewName);

		/// <summary>
		/// Requires the specified [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html).
		/// <list type="number">
		/// <item><description>If [Artifact.ArtifactID](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Artifact.html#Relativity_Testing_Framework_Models_Artifact_ArtifactID) property of <paramref name="view"/> has positive value, gets view by ArtifactID and updates it.</description></item>
		/// <item><description>If [NamedArtifact.Name](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.NamedArtifact.html#Relativity_Testing_Framework_Models_NamedArtifact_Name) property of <paramref name="view"/> has a value, gets view by name and updates it if it exists.</description></item>
		/// <item><description>Otherwise creates a new [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html).</description></item>
		/// </list>
		/// </summary>
		/// <returns>The [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html) entity.</returns>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace where you want to require view.</param>
		/// <param name="view">The view to require.</param>
		/// <example> This shows how to update view by using Require method with View entity that hase ArtifactID field filled.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingViewArtifactID = 1024345;
		/// int updatedOrder = 5;
		/// FixedLengthTextField field = relativityFacade.Resolve&lt;IFieldService&gt;().Get&lt;FixedLengthTextField&gt;(workspaceArtifactID, "Control Number");
		/// View viewToUpdate = new View
		/// {
		/// 	ArtifactID = existingViewArtifactID,
		/// 	Name = "Test Existing View Name",
		/// 	Fields = new[] { new NamedArtifact { Name = field.Name, ArtifactID = field.ArtifactID } },
		/// 	Order = updatedOrder
		/// }
		/// View updatedView = _viewService.Require(workspaceArtifactID, viewToUpdate);
		/// </code>
		/// </example>
		/// <example> This shows how to update view by using Require method with View entity that hase Name field filled.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingViewName = "Test Existing View Name",
		/// int updatedOrder = 5;
		/// FixedLengthTextField field = relativityFacade.Resolve&lt;IFieldService&gt;().Get&lt;FixedLengthTextField&gt;(workspaceArtifactID, "Control Number");
		/// View viewToUpdate = new View
		/// {
		/// 	Name = existingViewName
		/// 	Fields = new[] { new NamedArtifact { Name = field.Name, ArtifactID = field.ArtifactID } },
		/// 	Order = updatedOrder
		/// }
		/// View updatedView = _viewService.Require(workspaceArtifactID, viewToUpdate);
		/// </code>
		/// </example>
		/// <example> This shows how to create new view by using Require method with View entity that doesn't have ArtifactID field filled and Name that doesn't match any existing View.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// FixedLengthTextField field = relativityFacade.Resolve&lt;IFieldService&gt;().Get&lt;FixedLengthTextField&gt;(workspaceArtifactID, "Control Number");
		/// View viewtoCreate = new View
		/// {
		/// 	Name = "Test Not Existing View Name",
		/// 	Fields = new[] { new NamedArtifact { Name = field.Name, ArtifactID = field.ArtifactID } },
		/// }
		/// View createdView = _viewService.Require(workspaceArtifactID, viewtoCreate);
		/// </code>
		/// </example>
		View Require(int workspaceArtifactID, View view);

		/// <summary>
		/// Determines whether the [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html) with the specified ArtifactID exists.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace where you want to check if a view exists,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="viewArtifactID">The ArtifactID of the view.</param>
		/// <returns><see langword="true"/> if a view exists; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int viewArtifactID = 1024345;
		/// bool viewExists = _viewService.Exists(workspaceArtifactID, viewArtifactID);
		/// </code>
		/// </example>
		bool Exists(int workspaceArtifactID, int viewArtifactID);

		/// <summary>
		/// Updates the specified [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html).
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace where you want to update the view.</param>
		/// <param name="view">The [View](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.View.html) to update.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingViewArtifactID = 1024345;
		/// View viewToUpdate = _viewService.Get(workspaceArtifactID, existingViewArtifactID);
		/// viewToUpdate.Name = "Updated View Name";
		/// viewToUpdate.Order = 345;
		/// _viewService.Update(workspaceArtifactID, view);
		/// </code>
		/// </example>
		void Update(int workspaceArtifactID, View view);

		/// <summary>
		/// Gets the list of of object types in a specific workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <returns>The list of [NamedArtifact](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.NamedArtifact.html) objects.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingViewArtifactID = 1024345;
		/// var eligibleObjectTypes = _viewService.GetEligibleObjectTypes(workspaceArtifactID);
		/// View view = _viewService.Get(workspaceArtifactID, existingViewArtifactID);
		/// view.ObjectType = eligibleObjectTypes[0];
		/// _viewService.Update(workspaceArtifactID, view);
		/// </code>
		/// </example>
		List<NamedArtifact> GetEligibleObjectTypes(int workspaceId);

		/// <summary>
		/// Gets a list of users eligible to be view owners in a specific workspace.
		/// You can then use this list to assign owners to a view.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <returns>An array of [NamedArtifact](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.NamedArtifact.html) objects.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingViewArtifactID = 1024345;
		/// var owners = _viewService.GetViewOwners(workspaceArtifactID);
		/// View view = _viewService.Get(workspaceArtifactID, existingViewArtifactID);
		/// view.Owner = owners[0];
		/// _viewService.Update(workspaceArtifactID, view);
		/// </code>
		/// </example>
		NamedArtifact[] GetViewOwners(int workspaceId);
	}
}
