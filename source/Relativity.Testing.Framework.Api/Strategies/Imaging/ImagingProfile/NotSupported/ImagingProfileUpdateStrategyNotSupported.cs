using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingProfileUpdateStrategyNotSupported : IImagingProfileUpdateStrategy
	{
		public void Update(int workspaceId, ImagingProfile imagingProfile)
		{
			throw new ArgumentException("The method Update imaging profile does not support version of Relativity lower than 12.1.");
		}
	}
}
