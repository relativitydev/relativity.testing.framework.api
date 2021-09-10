using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingProfileCreateBasicStrategy
	{
		ImagingProfile Create(int workspaceId, CreateBasicImagingProfileDTO dto);
	}
}
