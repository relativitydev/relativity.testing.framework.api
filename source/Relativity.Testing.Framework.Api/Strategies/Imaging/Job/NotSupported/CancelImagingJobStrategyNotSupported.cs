using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class CancelImagingJobStrategyNotSupported : ICancelImagingJobStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Cancel Imaging Job does not support version of Relativity lower than 12.1.";

		public ImagingJobActionResponse Cancel(int workspaceId, long imagingJobId, ImagingJobRequest cancelImagingJobRequest)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		public Task<ImagingJobActionResponse> CancelAsync(int workspaceId, long imagingJobId, ImagingJobRequest cancelImagingJobRequest)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
