using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingJobRunStrategyNotSupported : IImagingJobRunStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Run Imaging Job does not support version of Relativity lower than 12.1.";

		public long Run(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		public Task<long> RunAsync(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
