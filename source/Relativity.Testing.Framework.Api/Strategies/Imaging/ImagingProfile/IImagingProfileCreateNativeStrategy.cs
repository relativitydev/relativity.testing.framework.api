using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingProfileCreateNativeStrategy
	{
		ImagingProfile Create(int workspaceId, CreateNativeImagingProfileDTO dto);
	}
}
