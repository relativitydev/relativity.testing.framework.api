using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ImagingJobService : IImagingJobService
	{
		private readonly IImagingJobRunStrategy _imagingJobRunStrategy;
		private readonly IImagingJobSubmitSingleDocumentStrategy _imagingJobSubmitSingleDocumentStrategy;
		private readonly IWaitForImagingJobToCompleteStrategy _waitForImagingJobToCompleteStrategy;
		private readonly IImagingJobSubmitMassDocumentStrategy _imagingJobSubmitMassDocumentStrategy;
		private readonly ICancelImagingJobStrategy _cancelImagingJobStrategy;

		public ImagingJobService(
			IImagingJobRunStrategy imagingJobRunStrategy,
			IWaitForImagingJobToCompleteStrategy waitForImagingJobToCompleteStrategy,
			IImagingJobSubmitSingleDocumentStrategy imagingJobSubmitSingleDocumentStrategy,
			IImagingJobSubmitMassDocumentStrategy imagingJobSubmitMassDocumentStrategy,
			ICancelImagingJobStrategy cancelImagingJobStrategy)
		{
			_imagingJobRunStrategy = imagingJobRunStrategy;
			_waitForImagingJobToCompleteStrategy = waitForImagingJobToCompleteStrategy;
			_imagingJobSubmitSingleDocumentStrategy = imagingJobSubmitSingleDocumentStrategy;
			_imagingJobSubmitMassDocumentStrategy = imagingJobSubmitMassDocumentStrategy;
			_cancelImagingJobStrategy = cancelImagingJobStrategy;
		}

		public int Run(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null)
			=> _imagingJobRunStrategy.Run(workspaceId, imagingSetId, imagingSetJobRequest);

		public async Task<int> RunAsync(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null)
			=> await _imagingJobRunStrategy.RunAsync(workspaceId, imagingSetId, imagingSetJobRequest).ConfigureAwait(false);

		public long SubmitSingleDocument(int workspaceId, int documentArtifactId, SingleDocumentImagingJobRequest singleDocumentImagingJobRequest)
			=> _imagingJobSubmitSingleDocumentStrategy.SubmitSingleDocument(workspaceId, documentArtifactId, singleDocumentImagingJobRequest);

		public async Task<long> SubmitSingleDocumentAsync(int workspaceId, int documentArtifactId, SingleDocumentImagingJobRequest singleDocumentImagingJobRequest)
			=> await _imagingJobSubmitSingleDocumentStrategy.SubmitSingleDocumentAsync(workspaceId, documentArtifactId, singleDocumentImagingJobRequest).ConfigureAwait(false);

		public long SubmitMassDocument(int workspaceId, ImagingMassJobRequest imagingMassJobRequest)
			=> _imagingJobSubmitMassDocumentStrategy.SubmitMassDocument(workspaceId, imagingMassJobRequest);

		public async Task<long> SubmitMassDocumentAsync(int workspaceId, ImagingMassJobRequest imagingMassJobRequest)
			=> await _imagingJobSubmitMassDocumentStrategy.SubmitMassDocumentAsync(workspaceId, imagingMassJobRequest).ConfigureAwait(false);

		public ImagingJobActionResponse Cancel(int workspaceId, long imagingJobId, ImagingJobRequest cancelImagingJobRequest)
			=> _cancelImagingJobStrategy.Cancel(workspaceId, imagingJobId, cancelImagingJobRequest);

		public async Task<ImagingJobActionResponse> CancelAsync(int workspaceId, long imagingJobId, ImagingJobRequest cancelImagingJobRequest)
			=> await _cancelImagingJobStrategy.CancelAsync(workspaceId, imagingJobId, cancelImagingJobRequest).ConfigureAwait(false);

		public void WaitForTheJobToComplete(int workspaceId, int imagingSetId, double timeout = 2.0)
			=> _waitForImagingJobToCompleteStrategy.Wait(workspaceId, imagingSetId, timeout);

		public async Task WaitForTheJobToCompleteAsync(int workspaceId, int imagingSetId, double timeout = 2.0)
			=> await _waitForImagingJobToCompleteStrategy.WaitAsync(workspaceId, imagingSetId, timeout).ConfigureAwait(false);
	}
}
