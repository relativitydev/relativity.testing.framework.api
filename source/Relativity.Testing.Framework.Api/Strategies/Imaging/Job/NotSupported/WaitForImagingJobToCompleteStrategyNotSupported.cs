using System;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class WaitForImagingJobToCompleteStrategyNotSupported : IWaitForImagingJobToCompleteStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Wait for Imaging Job to Complete does not support version of Relativity lower than 12.1.";

		public void Wait(int workspaceId, int imagingSetId, double timeout = 2.0)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
