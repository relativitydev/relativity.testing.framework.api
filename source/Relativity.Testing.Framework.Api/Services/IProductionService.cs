using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the production API service.
	/// </summary>
	/// <example>
	/// <code>
	/// IProductionService _productionService = relativityFacade.Resolve&lt;IProductionService&gt;();
	/// </code>
	/// </example>
	public interface IProductionService
	{
		/// <summary>
		/// Creates the specified [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to add the new [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).</param>
		/// <param name="entity">The [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html) entity to create.</param>
		/// <returns>The created [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html) entity.</returns>
		/// <example> This example shows how to create Production witout setting properties - they will be set to default values.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// Production createdProduction = _productionService.Create(workspaceArtifactID, new Production());
		/// </code>
		/// </example>
		/// <example> This example shows how to create Production with some properties set.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// string fieldName = "Control Number";
		/// var productionToCreate = new Production
		/// {
		///     Name = "Test Production Name",
		///     Numbering = new ProductionNumbering
		///     {
		///         NumberingType = NumberingType.PageLevel,
		///         BatesPrefix = "Prefix",
		///         BatesSuffix = "Suffix",
		///         BatesStartNumber = 1,
		///         NumberOfDigitsForDocumentNumbering = 7,
		///         AttachmentRelationalField = new NamedArtifact { Name = string.Empty}
		///     }
		///     Headers = new ProductionHeaders
		///     {
		///         CenterHeader = new HeaderFooter { Type = HeaderFooterType.OriginalImageNumber }
		///     },
		///     Footers = new ProductionFooters
		///     {
		///         LeftFooter = new HeaderFooter { Type = HeaderFooterType.Field, Field = new NamedArtifact { Name = fieldName } },
		///         CenterFooter = new HeaderFooter { Type = HeaderFooterType.FreeText, FreeText = Randomizer.GetString() },
		///         RightFooter = new HeaderFooter { Type = HeaderFooterType.AdvancedFormatting, AdvancedFormatting = fieldName }
		///     }
		/// };
		/// Production createdProduction = _productionService.Create(workspaceArtifactID, productionToCreate);
		/// </code>
		/// </example>
		Production Create(int workspaceId, Production entity);

		/// <summary>
		/// Gets the [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html) by the specified ArtifactID.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to get the [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).</param>
		/// <param name="entityId">The ArtifactID of the [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).</param>
		/// <returns>>The [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingProductionArtifactID = 101353;
		/// Production production = _productionService.Get(workspaceArtifactID, existingProductionArtifactID);
		/// </code>
		/// </example>
		Production Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the production by the specified name.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to get the [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).</param>
		/// <param name="entityName">The Namwe of the production.</param>
		/// <returns>>The [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// string existingProductionName = "Test Production Name";
		/// Production production = _productionService.Get(workspaceArtifactID, existingProductionName);
		/// </code>
		/// </example>
		Production Get(int workspaceId, string entityName);

		/// <summary>
		/// Gets all [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html)s.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to get the all [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html)s.</param>
		/// <returns>The collection of [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html) entities.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// Production[] productions = _productionService.GetAll(workspaceArtifactID);
		/// </code>
		/// </example>
		Production[] GetAll(int workspaceId);

		/// <summary>
		/// Determines whether the production with the specified ArtifactID exists.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace.</param>
		/// <param name="entityId">The ArtifactID of the [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).</param>
		/// <returns><see langword="true"/> if a production exists; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int productionArtifactID = 101353;
		/// bool productionExists = _productionService.Exists(workspaceArtifactID, productionArtifactID);
		/// </code>
		/// </example>
		bool Exists(int workspaceId, int entityId);

		/// <summary>
		/// Delete the [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html) by the specified ArtifactID.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace.</param>
		/// <param name="entityId">The ArtifactID of the [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingProductionArtifactID = 101353;
		/// _productionService.Delete(workspaceArtifactID, existingProductionArtifactID);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets status of a specific [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of a workspace.</param>
		/// <param name="entityId">The ArtifactID of a [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).</param>
		/// <returns>Returns status of a specific [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingProductionArtifactID = 101353;
		/// ProductionStatus productionStatus = _productionService.GetStatus(workspaceArtifactID, existingProductionArtifactID);
		/// </code>
		/// </example>
		ProductionStatus GetStatus(int workspaceId, int entityId);

		/// <summary>
		/// Stages a specific [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of a workspace.</param>
		/// <param name="entityId">The ArtifactID of a [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).</param>
		/// <param name="seconds">Time in seconds to wait for "Staged" status. Default is 60.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingProductionArtifactID = 101353;
		/// _productionService.Stage(workspaceArtifactID, existingProductionArtifactID, 100);
		/// </code>
		/// </example>
		void Stage(int workspaceId, int entityId, int seconds = 60);

		/// <summary>
		/// Runs a specific [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of a workspace.</param>
		/// <param name="entityId">The ArtifactID of a [Production](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Production.html).</param>
		/// <param name="timeout">Time in seconds to wait for "Produced" status. Default is 120.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingProductionArtifactID = 101353;
		/// _productionService.Run(workspaceArtifactID, existingProductionArtifactID, 80);
		/// </code>
		/// </example>
		void Run(int workspaceId, int entityId, int timeout = 120);
	}
}
