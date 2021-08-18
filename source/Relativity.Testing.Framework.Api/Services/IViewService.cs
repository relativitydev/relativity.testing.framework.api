using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represent the API view service.
	/// </summary>
	/// <example>
	/// <code>
	/// _viewService = relativityFacade.Resolve&lt;IViewService&gt;();
	/// </code>
	/// </example>
	public interface IViewService
	{
		/// <summary>
		///  Creates the specified <see cref="View"/>.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace where you want to create the view,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="view">The view to create.</param>
		/// <returns>The <see cref="View"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1015427;
		/// FixedLengthTextField field = relativityFacade.Resolve&lt;IGetWorkspaceEntityByNameStrategy&lt;FixedLengthTextField&gt;&gt;().Get(workspaceID, "Control Number");
		/// View viewtoCreate = new View
		/// {
		/// 	Name = "Test View Name",
		/// 	Fields = new[] { new NamedArtifact { Name = field.Name, ArtifactID = field.ArtifactID } },
		/// }
		/// View createdView = _viewService.Create(workspaceID, viewtoCreate);
		/// </code>
		/// </example>
		View Create(int workspaceArtifactID, View view);

		/// <summary>
		/// Gets all <see cref="View"/>s in workspace.
		/// Returns not all fields for faster response.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace where you want to get the view,
		/// or use -1 to indicate the admin-level context.</param>
		/// <returns>The collection of <see cref="View"/> entities.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1015427;
		/// View[] views = _viewService.GetAll(workspaceID);
		/// </code>
		/// </example>
		View[] GetAll(int workspaceArtifactID);

		/// <summary>
		/// Gets the <see cref="View"/> by the specified ArtifactID.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace where you want to get the view,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="viewArtifactID">The ArtifactID of the view.</param>
		/// <returns>The <see cref="View"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1015427;
		/// int viewArtifactID = 1024345;
		/// View view = _viewService.Get(workspaceID, viewArtifactID);
		/// </code>
		/// </example>
		View Get(int workspaceArtifactID, int viewArtifactID);

		/// <summary>
		/// Gets the <see cref="View"/> by the specified name.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace where you want to get the view,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="viewName">The name of the view.</param>
		/// <returns>The <see cref="View"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1015427;
		/// int viewName = "Test View Name";
		/// View view = _viewService.Get(workspaceID, viewName);
		/// </code>
		/// </example>
		View Get(int workspaceArtifactID, string viewName);

		/// <summary>
		/// Requires the specified <see cref="View"/>.
		/// <list type="number">
		/// <item><description>If <see cref="Artifact.ArtifactID"/> property of <paramref name="view"/> has positive value, gets view by ArtifactID and updates it.</description></item>
		/// <item><description>If <see cref="NamedArtifact.Name"/> property of <paramref name="view"/> has a value, gets view by name and updates it if it exists.</description></item>
		/// <item><description>Otherwise creates a new view using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</description></item>
		/// </list>
		/// </summary>
		/// <returns>The <see cref="View"/> entity or <see langword="null"/>.</returns>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace where you want to require view.</param>
		/// <param name="view">The view to require.</param>
		/// <example> This shows how to update view by using Require method with View entity that hase ArtifactID field filled.
		/// <code>
		/// int workspaceID = 1015427;
		/// int existingViewID = 1024345;
		/// int updatedOrder = 5;
		/// FixedLengthTextField field = relativityFacade.Resolve&lt;IGetWorkspaceEntityByNameStrategy&lt;FixedLengthTextField&gt;&gt;().Get(workspaceID, "Control Number");
		/// View viewToUpdate = new View
		/// {
		/// 	ArtifactID = existingViewID,
		/// 	Name = "Test Existing View Name",
		/// 	Fields = new[] { new NamedArtifact { Name = field.Name, ArtifactID = field.ArtifactID } },
		/// 	Order = updatedOrder
		/// }
		/// View updatedView = _viewService.Require(workspaceID, viewToUpdate);
		/// </code>
		/// </example>
		/// <example> This shows how to update view by using Require method with View entity that hase Name field filled.
		/// <code>
		/// int workspaceID = 1015427;
		/// int existingViewName = "Test Existing View Name",
		/// int updatedOrder = 5;
		/// FixedLengthTextField field = relativityFacade.Resolve&lt;IGetWorkspaceEntityByNameStrategy&lt;FixedLengthTextField&gt;&gt;().Get(workspaceID, "Control Number");
		/// View viewToUpdate = new View
		/// {
		/// 	Name = existingViewName
		/// 	Fields = new[] { new NamedArtifact { Name = field.Name, ArtifactID = field.ArtifactID } },
		/// 	Order = updatedOrder
		/// }
		/// View updatedView = _viewService.Require(workspaceID, viewToUpdate);
		/// </code>
		/// </example>
		/// <example> This shows how to create new view by using Require method with View entity that doesn't have ArtifactID field filled and Name that doesn't match any existing View.
		/// <code>
		/// int workspaceID = 1015427;
		/// FixedLengthTextField field = relativityFacade.Resolve&lt;IGetWorkspaceEntityByNameStrategy&lt;FixedLengthTextField&gt;&gt;().Get(workspaceID, "Control Number");
		/// View viewtoCreate = new View
		/// {
		/// 	Name = "Test Not Existing View Name",
		/// 	Fields = new[] { new NamedArtifact { Name = field.Name, ArtifactID = field.ArtifactID } },
		/// }
		/// View createdView = _viewService.Require(workspaceID, viewtoCreate);
		/// </code>
		/// </example>
		View Require(int workspaceArtifactID, View view);

		/// <summary>
		/// Determines whether the <see cref="View"/> with the specified ArtifactID exists.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace where you want to check existing of view,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="viewArtifactID">The ArtifactID of the view.</param>
		/// <returns><see langword="true"/> if a view exists; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1015427;
		/// int viewArtifactID = 1024345;
		/// bool viewExists = _viewService.Exists(workspaceID, viewArtifactID);
		/// </code>
		/// </example>
		bool Exists(int workspaceArtifactID, int viewArtifactID);

		/// <summary>
		/// Updates the specified <see cref="View"/>.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace where you want to update the view.</param>
		/// <param name="view">The <see cref="View"/> to update.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 1015427;
		/// int existingViewArtifactID = 1024345;
		/// View viewToUpdate = _viewService.Get(workspaceID, existingViewArtifactID);
		/// viewToUpdate.Name = "Updated View Name";
		/// viewToUpdate.Order = 345;
		/// _viewService.Update(workspaceID, existingViewArtifactID);
		/// </code>
		/// </example>
		void Update(int workspaceArtifactID, View view);
	}
}
