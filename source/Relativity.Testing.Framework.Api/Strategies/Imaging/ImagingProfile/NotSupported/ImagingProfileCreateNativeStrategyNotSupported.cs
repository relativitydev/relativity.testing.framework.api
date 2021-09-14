using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingProfileCreateNativeStrategyNotSupported : IImagingProfileCreateNativeStrategy
	{
		public ImagingProfile Create(int workspaceId, CreateNativeImagingProfileDTO dto)
		{
			throw new ArgumentException("The method Create basic imaging profile does not support version of Relativity lower than 12.1.");
		}
	}
}
