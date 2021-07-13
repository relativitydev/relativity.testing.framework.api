using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingSetValidatorV1
	{
		void ValidateImagingRequest(int workspaceId, ImagingSetRequest imagingRequesst);

		void ValidateIds(int workspaceId, int imagingSetId);
	}
}
