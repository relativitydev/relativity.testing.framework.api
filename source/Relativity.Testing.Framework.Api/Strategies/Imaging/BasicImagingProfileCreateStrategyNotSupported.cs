using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class BasicImagingProfileCreateStrategyNotSupported : IBasicImagingProfileCreateStrategy
	{
		public ImagingProfile Create(int workspaceId, CreateBasicImagingProfileDTO dto)
		{
			throw new ArgumentException("The method Create basic imaging profile does not support version of Relativity lower than 12.1.");
		}

		public Task<ImagingProfile> CreateAsync(int workspaceId, CreateBasicImagingProfileDTO dto)
		{
			throw new ArgumentException("The method Create basic imaging profile does not support version of Relativity lower than 12.1.");
		}
	}
}
