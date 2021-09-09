using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingProfileGetStrategy
	{
		ImagingProfile Get(int workspaceId, int imagingProfileId);
	}
}
