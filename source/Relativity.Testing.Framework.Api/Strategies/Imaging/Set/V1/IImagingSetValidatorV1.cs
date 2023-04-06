using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingSetValidatorV1
	{
		void ValidateImagingSetCreateRequest(int workspaceId, ImagingSetRequest imagingRequesst);

		void ValidateImagingSetUpdateRequest(int workspaceId, int imagingSetId, ImagingSetRequest imagingRequesst);

		void ValidateIds(int workspaceId, int imagingSetId);
	}
}
