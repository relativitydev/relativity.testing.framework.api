using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ImagingJobService : IImagingJobService
	{
		private readonly IImagingJobRunStrategy _imagingJobRunStrategy;
		private readonly IWaitForImagingJobToCompleteStrategy _waitForImagingJobToCompleteStrategy;

		public ImagingJobService(
			IImagingJobRunStrategy imagingJobRunStrategy,
			IWaitForImagingJobToCompleteStrategy waitForImagingJobToCompleteStrategy)
		{
			_imagingJobRunStrategy = imagingJobRunStrategy;
			_waitForImagingJobToCompleteStrategy = waitForImagingJobToCompleteStrategy;
		}

		public int Run(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null)
			=> _imagingJobRunStrategy.Run(workspaceId, imagingSetId, imagingSetJobRequest);

		public async Task<int> RunAsync(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null)
			=> await _imagingJobRunStrategy.RunAsync(workspaceId, imagingSetId, imagingSetJobRequest).ConfigureAwait(false);

		public void WaitForTheJobToComplete(int workspaceId, int imagingSetId, double timeout = 2.0)
			=> _waitForImagingJobToCompleteStrategy.Wait(workspaceId, imagingSetId, timeout);

		public async Task WaitForTheJobToCompleteAsync(int workspaceId, int imagingSetId, double timeout = 2.0)
			=> await _waitForImagingJobToCompleteStrategy.WaitAsync(workspaceId, imagingSetId, timeout).ConfigureAwait(false);
	}
}
