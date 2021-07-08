using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IBasicImagingProfileCreateStrategy
	{
		ImagingProfile Create(int workspaceId, CreateBasicImagingProfileDTO dto);

		Task<ImagingProfile> CreateAsync(int workspaceId, CreateBasicImagingProfileDTO dto);
	}
}
