using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingJobRetryErrorsStrategy
	{
		long RetryErrors(int workspaceId, int imagingSetId, ImagingSetJobRequest retryErrorsRequest = null);

		Task<long> RetryErrorsAsync(int workspaceId, int imagingSetId, ImagingSetJobRequest retryErrorsRequest = null);
	}
}
