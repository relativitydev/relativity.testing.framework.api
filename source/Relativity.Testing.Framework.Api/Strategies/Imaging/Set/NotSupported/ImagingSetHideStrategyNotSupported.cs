using System;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingSetHideStrategyNotSupported : IImagingSetHideStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Hide Imaging Set does not support version of Relativity lower than 12.1.";

		public void Hide(int workspaceId, int imagingSetId)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
