using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingProfileUpdateStrategy
	{
		ImagingProfile Update(int workspaceId, ImagingProfile imagingProfile);
	}
}
