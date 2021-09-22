using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the search provider API service.
	/// </summary>
	/// <example>
	/// <code>
	/// ISearchProviderService _searchProviderService = relativityFacade.Resolve&lt;ISearchProviderService&gt;();
	/// </code>
	/// </example>
	public interface ISearchProviderService
	{
		/// <summary>
		/// Creates the specified search provider.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new search provider.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// var searchProvider = new SearchProvider
		/// {
		///     Name = "SomeName",
		///     Order = 200
		/// };
		/// SearchProvider createdSearchProvider = _searchProviderService.Create(workspaceID, searchProvider);
		/// </code>
		/// </example>
		SearchProvider Create(int workspaceId, SearchProvider entity);

		/// <summary>
		/// Deletes the search provider by ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the search provider.</param>
		/// <param name="entityId">The artifact ID of the search provider.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// int searchProviderID = 654321;
		///
		/// _searchProviderService.Delete(workspaceID, searchProviderID);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the search provider by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get search provider.</param>
		/// <param name="entityId">The artifact ID of the search provider.</param>
		/// <returns>The <see cref="KeywordSearch"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// int searchProviderID = 654321;
		///
		/// SearchProvider searchProvider = _searchProviderService.Get(workspaceID, searchProviderID);
		/// </code>
		/// </example>
		SearchProvider Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the search provider by the specified name.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get search provider.</param>
		/// <param name="entityName">The name of the search provider.</param>
		/// <returns>The <see cref="SearchProvider"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// int searchProviderName = "searchProviderName";
		///
		/// SearchProvider searchProvider = _searchProviderService.Get(workspaceID, searchProviderName);
		/// </code>
		/// </example>
		SearchProvider Get(int workspaceId, string entityName);

		/// <summary>
		/// Updates the specified search provider.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update search provider.</param>
		/// <param name="entity">The entity to update.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// int searchProviderID = 654321;
		///
		/// SearchProvider searchProvider = _searchProviderService.Get(workspaceID, searchProviderID);
		/// searchProvider.Name = "New name";
		/// searchProvider.Order = 10;
		///
		/// _searchProviderService.Update(workspaceID, searchProvider);
		/// </code>
		/// </example>
		void Update(int workspaceId, SearchProvider entity);

		/// <summary>
		/// Requires the specified search provider.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</item>
		/// <item>If <see cref="NamedArtifact.Name"/> property of <paramref name="entity"/> has a value, gets entity by name and updates it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <returns>The <see cref="SearchProvider"/> entity or <see langword="null"/>.</returns>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to require search provider.</param>
		/// <param name="entity">The entity to require.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// var searchProvider = new SearchProvider
		/// {
		///     Name = "SomeName",
		///     Order = 200
		/// };
		///
		/// SearchProvider result = _searchProviderService.Require(workspaceID, searchProvider);
		/// </code>
		/// </example>
		SearchProvider Require(int workspaceId, SearchProvider entity);

		/// <summary>
		/// Gets the list of dependencies.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The entity ID.</param>
		/// <returns>The list of dependencies.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// int searchProviderID = 654321;
		///
		/// List&lt;Dependency&gt; dependencies = _searchProviderService.GetDependencies(workspaceID, searchProviderID);
		/// </code>
		/// </example>
		List<Dependency> GetDependencies(int workspaceId, int entityId);
	}
}
