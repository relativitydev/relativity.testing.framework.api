using System;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingProfileDeleteStrategyNotSupported : IImagingProfileDeleteStrategy
	{
		public void Delete(int workspaceId, int imagingProfileId)
		{
			throw new ArgumentException("The method Delete imaging profile does not support version of Relativity lower than 12.1.");
		}
	}
}
