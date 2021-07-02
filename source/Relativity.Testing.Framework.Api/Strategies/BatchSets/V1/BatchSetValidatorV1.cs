using System;
using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchSetValidatorV1 : IBatchSetValidatorV1
	{
		private readonly IWorkspaceIdValidator _workspaceIdValidator;
		private readonly IArtifactIdValidator _artifactIdValidator;
		private readonly IExistsBatchSetByIdStrategy _existsBatchSetByIdStrategy;

		public BatchSetValidatorV1(
			IWorkspaceIdValidator workspaceIdValidator,
			IArtifactIdValidator artifactIdValidator,
			IExistsBatchSetByIdStrategy existsBatchSetByIdStrategy)
		{
			_workspaceIdValidator = workspaceIdValidator;
			_artifactIdValidator = artifactIdValidator;
			_existsBatchSetByIdStrategy = existsBatchSetByIdStrategy;
		}

		public void ValidateWorkspaceId(int workspaceId)
			=> _workspaceIdValidator.Validate(workspaceId);

		public void ValidateWorkspaceIdAndExistingBatchSetId(int workspaceId, int batchSetId, UserCredentials userCredentials = null)
		{
			ValidateWorkspaceId(workspaceId);
			_artifactIdValidator.Validate(batchSetId, "Batch Set");
			ValidateBatchSetExists(workspaceId, batchSetId, userCredentials);
		}

		public void ValidateUpdateArguments(int workspaceId, BatchSet batchSet, UserCredentials userCredentials = null)
		{
			if (batchSet == null)
			{
				throw new ArgumentNullException(nameof(batchSet));
			}

			ValidateWorkspaceIdAndExistingBatchSetId(workspaceId, batchSet.ArtifactID, userCredentials);
		}

		private void ValidateBatchSetExists(int workspaceId, int batchSetId, UserCredentials userCredentials = null)
		{
			if (!_existsBatchSetByIdStrategy.Exists(workspaceId, batchSetId, userCredentials: userCredentials))
			{
				throw new ArgumentException($"The batch set with ID: {batchSetId} does not exists.");
			}
		}
	}
}
