using System;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ImagingSetValidatorV1 : IImagingSetValidatorV1
	{
		private readonly IWorkspaceIdValidator _workspaceIdValidator;
		private readonly IArtifactIdValidator _artifactIdValidator;

		public ImagingSetValidatorV1(IWorkspaceIdValidator workspaceIdValidator, IArtifactIdValidator artifactIdValidator)
		{
			_workspaceIdValidator = workspaceIdValidator;
			_artifactIdValidator = artifactIdValidator;
		}

		public void ValidateImagingSetCreateRequest(int workspaceId, ImagingSetRequest imagingRequesst)
		{
			_workspaceIdValidator.Validate(workspaceId);
			ValidateImagingSetRequest(imagingRequesst);
		}

		public void ValidateImagingSetUpdateRequest(int workspaceId, int imagingSetId, ImagingSetRequest imagingRequesst)
		{
			ValidateIds(workspaceId, imagingSetId);
			ValidateImagingSetRequest(imagingRequesst);
		}

		private void ValidateImagingSetRequest(ImagingSetRequest imagingRequesst)
		{
			if (imagingRequesst is null)
			{
				throw new ArgumentNullException(nameof(imagingRequesst));
			}

			_artifactIdValidator.Validate(imagingRequesst.DataSourceID, "Data Source");
			_artifactIdValidator.Validate(imagingRequesst.ImagingProfileID, "Imaging Profile");

			if (string.IsNullOrWhiteSpace(imagingRequesst.Name))
			{
				throw new ArgumentException("Name must be filled and not empty.");
			}
		}

		public void ValidateIds(int workspaceId, int imagingSetId)
		{
			_workspaceIdValidator.Validate(workspaceId);
			_artifactIdValidator.Validate(imagingSetId, "Imaging Set");
		}
	}
}
