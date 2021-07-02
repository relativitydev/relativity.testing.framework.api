using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the batch set API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _batchSetService = relativityFacade.Resolve&lt;IBatchSetService&gt;()
	/// </code>
	/// </example>
	public interface IBatchSetService
	{
		/// <summary>
		/// Creates the specified batch set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new batch set.</param>
		/// <param name="entity">The <see cref="BatchSet"/> entity to create.</param>
		/// <returns>The created <see cref="BatchSet"/> entity.</returns>
		/// <example>
		/// <code>
		/// var fieldService =  relativityFacade.Resolve&lt;IFieldService&gt;();
		/// var workspaceId = 1015427;
		/// keywordSearch = relativityFacade.Resolve&lt;IKeywordSearchService&gt;().Create(workspaceId, new KeywordSearch());
		/// batchUnitField = fieldService.Get(workspaceId, "Custodian");
		/// familyField = fieldService.Get(workspaceId, "Group Identifier");
		/// reviewedField = fieldService.Get(workspaceId, "Classification Index");
		/// var batchSet = new BatchSet
		/// {
		/// 	Name = Randomizer.GetString(),
		/// 	BatchSize = 1500,
		/// 	BatchPrefix = Randomizer.GetString("BS", 3),
		/// 	DataSource = new NamedArtifact { ArtifactID = keywordSearch.ArtifactID},
		/// 	BatchUnitField = new NamedArtifact { ArtifactID = batchUnitField.ArtifactID},
		/// 	FamilyField = new NamedArtifact { ArtifactID = familyField.ArtifactID },
		/// 	ReviewedField = new NamedArtifact { ArtifactID = reviewedField.ArtifactID },
		/// 	AutoBatchSettings = new AutoBatchSettings
		/// 	{
		/// 		AutoCreateRateInMinutes = 10,
		/// 		MinimumBatchSize = 10
		/// 	}
		/// };
		/// BatchSet batchSet = _batchSetService.Create(workspaceId, batchSet);
		/// </code>
		/// </example>
		BatchSet Create(int workspaceId, BatchSet entity);

		/// <summary>
		/// Creates the specified batch set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new batch set.</param>
		/// <param name="entity">The <see cref="BatchSet"/> entity to create.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>The created <see cref="BatchSet"/> entity.</returns>
		/// <example>
		/// <code>
		/// var fieldService =  relativityFacade.Resolve&lt;IFieldService&gt;();
		/// var workspaceId = 1015427;
		/// keywordSearch = relativityFacade.Resolve&lt;IKeywordSearchService&gt;().Create(workspaceId, new KeywordSearch());
		/// batchUnitField = fieldService.Get(workspaceId, "Custodian");
		/// familyField = fieldService.Get(workspaceId, "Group Identifier");
		/// reviewedField = fieldService.Get(workspaceId, "Classification Index");
		/// var batchSet = new BatchSet
		/// {
		/// 	Name = Randomizer.GetString(),
		/// 	BatchSize = 1500,
		/// 	BatchPrefix = Randomizer.GetString("BS", 3),
		/// 	DataSource = new NamedArtifact { ArtifactID = keywordSearch.ArtifactID},
		/// 	BatchUnitField = new NamedArtifact { ArtifactID = batchUnitField.ArtifactID},
		/// 	FamilyField = new NamedArtifact { ArtifactID = familyField.ArtifactID },
		/// 	ReviewedField = new NamedArtifact { ArtifactID = reviewedField.ArtifactID },
		/// 	AutoBatchSettings = new AutoBatchSettings
		/// 	{
		/// 		AutoCreateRateInMinutes = 10,
		/// 		MinimumBatchSize = 10
		/// 	}
		/// };
		/// var userCredentials = new UserCredentials
		/// {
		/// 	Username = "SomeUsername",
		/// 	Password = "SomePassword"
		/// };
		/// BatchSet batchSet = _batchSetService.Create(workspaceId, batchSet, userCredentials);
		/// </code>
		/// </example>
		BatchSet Create(int workspaceId, BatchSet entity, UserCredentials userCredentials);

		/// <summary>
		/// Gets the <see cref="BatchSet"/> by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the batch set.</param>
		/// <param name="entityId">The Artifact ID of the batch set.</param>
		/// <returns>>The <see cref="Production"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// BatchSet batchSet = _batchSetService.Get(workspaceId, batchSetArtifactId);
		/// </code>
		/// </example>
		BatchSet Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the <see cref="BatchSet"/> by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the batch set.</param>
		/// <param name="entityId">The Artifact ID of the batch set.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>>The <see cref="Production"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var userCredentials = new UserCredentials
		/// {
		/// 	Username = "SomeUsername",
		/// 	Password = "SomePassword"
		/// };
		/// BatchSet batchSet = _batchSetService.Get(workspaceId, batchSetArtifactId, userCredentials);
		/// </code>
		/// </example>
		BatchSet Get(int workspaceId, int entityId, UserCredentials userCredentials);

		/// <summary>
		/// Determines whether the <see cref="BatchSet"/> with the specified case artifact ID exists.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the batch set.</param>
		/// <returns><see langword="true"/> if a batch set exists; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var batchSetExists = _batchSetService.Exists(workspaceId, batchSetArtifactId);
		/// </code>
		/// </example>
		bool Exists(int workspaceId, int entityId);

		/// <summary>
		/// Determines whether the <see cref="BatchSet"/> with the specified case artifact ID exists.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the batch set.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns><see langword="true"/> if a batch set exists; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var userCredentials = new UserCredentials
		/// {
		/// 	Username = "SomeUsername",
		/// 	Password = "SomePassword"
		/// };
		/// BatchSet batchSet = _batchSetService.Exists(workspaceId, batchSetArtifactId, userCredentials);
		/// </code>
		/// </example>
		bool Exists(int workspaceId, int entityId, UserCredentials userCredentials);

		/// <summary>
		/// Updates the specified  <see cref="BatchSet"/>.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entity">The <see cref="BatchSet"/> entity to update.</param>
		/// <returns>Updated <see cref="BatchSet"/> entity.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// BatchSet batchSetToUpdate = _batchSetService.Get(workspaceId, batchSetArtifactId);
		/// batchSetToUpdate.BatchSize = 30;
		/// BatchSet updatedBatchSet = _batchSetService.Update(workspaceId, batchSetToUpdate);
		/// </code>
		/// </example>
		BatchSet Update(int workspaceId, BatchSet entity);

		/// <summary>
		/// Updates the specified <see cref="BatchSet"/>.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entity">The <see cref="BatchSet"/> entity to update.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>Updated <see cref="BatchSet"/> entity.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// BatchSet batchSetToUpdate = _batchSetService.Get(workspaceId, batchSetArtifactId);
		/// batchSetToUpdate.BatchSize = 30;
		/// var userCredentials = new UserCredentials
		/// {
		/// 	Username = "SomeUsername",
		/// 	Password = "SomePassword"
		/// };
		/// BatchSet updatedBatchSet = _batchSetService.Update(workspaceId, batchSetToUpdate, userCredentials);
		/// </code>
		/// </example>
		BatchSet Update(int workspaceId, BatchSet entity, UserCredentials userCredentials);

		/// <summary>
		/// Deletes the <see cref="BatchSet"/> with the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the batch set.</param>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// BatchSet updatedBatchSet = _batchSetService.Delete(workspaceId, batchSetId);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Deletes the <see cref="BatchSet"/> with the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the batch set.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var userCredentials = new UserCredentials
		/// {
		/// 	Username = "SomeUsername",
		/// 	Password = "SomePassword"
		/// };
		/// BatchSet updatedBatchSet = _batchSetService.Delete(workspaceId, batchSetId, userCredentials);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId, UserCredentials userCredentials);

		/// <summary>
		/// Runs create batches operation for a specific <see cref="BatchSet"/>.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a batch set.</param>
		/// <returns>>The <see cref="BatchProcessResult"/> entity.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// BatchProcessResult result = _batchSetService.CreateBatches(workspaceId, batchSetArtifactId);
		/// </code>
		/// </example>
		BatchProcessResult CreateBatches(int workspaceId, int entityId);

		/// <summary>
		/// Runs create batches operation for a specific <see cref="BatchSet"/>.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a batch set.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>>The <see cref="BatchProcessResult"/> entity.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var userCredentials = new UserCredentials
		/// {
		/// 	Username = "SomeUsername",
		/// 	Password = "SomePassword"
		/// };
		/// BatchProcessResult result = _batchSetService.CreateBatches(workspaceId, batchSetArtifactId, userCredentials);
		/// </code>
		/// </example>
		BatchProcessResult CreateBatches(int workspaceId, int entityId, UserCredentials userCredentials);

		/// <summary>
		/// Runs purge batches operation for a specific <see cref="BatchSet"/>.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a batch set.</param>
		/// <returns>>The <see cref="BatchProcessResult"/> entity.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// BatchProcessResult result = _batchSetService.PurgeBatches(workspaceId, batchSetArtifactId);
		/// </code>
		/// </example>
		BatchProcessResult PurgeBatches(int workspaceId, int entityId);

		/// <summary>
		/// Runs purge bathces operation for a specific <see cref="BatchSet"/>.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a batch set.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>>The <see cref="BatchProcessResult"/> entity.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var userCredentials = new UserCredentials
		/// {
		/// 	Username = "SomeUsername",
		/// 	Password = "SomePassword"
		/// };
		/// BatchProcessResult result = _batchSetService.PurgeBatches(workspaceId, batchSetArtifactId, userCredentials);
		/// </code>
		/// </example>
		BatchProcessResult PurgeBatches(int workspaceId, int entityId, UserCredentials userCredentials);
	}
}
