using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingJobRetryErrorsStrategyNotSupported : IImagingJobRetryErrorsStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Imaging Job Retry Errors does not support version of Relativity lower than 12.1.";

		public long RetryErrors(int workspaceId, int imagingSetId, ImagingSetJobRequest retryErrorsRequest = null)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		public Task<long> RetryErrorsAsync(int workspaceId, int imagingSetId, ImagingSetJobRequest retryErrorsRequest = null)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
