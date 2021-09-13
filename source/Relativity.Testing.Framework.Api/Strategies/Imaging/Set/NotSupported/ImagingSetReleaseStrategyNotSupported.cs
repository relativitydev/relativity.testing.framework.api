using System;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingSetReleaseStrategyNotSupported : IImagingSetReleaseStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Release Imaging Set does not support version of Relativity lower than 12.1.";

		public void Release(int workspaceId, int imagingSetId)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
