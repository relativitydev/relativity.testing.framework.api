using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingProfileGetStrategyNotSupported : IImagingProfileGetStrategy
	{
		public ImagingProfile Get(int workspaceId, int imagingProfileId)
		{
			throw new ArgumentException("The method Get imaging profile does not support version of Relativity lower than 12.1.");
		}

		public Task<ImagingProfile> GetAsync(int workspaceId, int imagingProfileId)
		{
			throw new ArgumentException("The method Get imaging profile does not support version of Relativity lower than 12.1.");
		}
	}
}
