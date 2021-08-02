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
		private readonly IImagingJobRetryErrorsStrategy _imagingJobRetryErrorsStrategy;
		private readonly IImagingJobUpdatePriorityStrategy _imagingJobUpdatePriorityStrategy;

		public ImagingJobService(
			IImagingJobRunStrategy imagingJobRunStrategy,
			IWaitForImagingJobToCompleteStrategy waitForImagingJobToCompleteStrategy,
			IImagingJobSubmitSingleDocumentStrategy imagingJobSubmitSingleDocumentStrategy,
			IImagingJobSubmitMassDocumentStrategy imagingJobSubmitMassDocumentStrategy,
			ICancelImagingJobStrategy cancelImagingJobStrategy,
			IImagingJobRetryErrorsStrategy imagingJobRetryErrorsStrategy,
			IImagingJobUpdatePriorityStrategy imagingJobUpdatePriorityStrategy)
		{
			_imagingJobRunStrategy = imagingJobRunStrategy;
			_waitForImagingJobToCompleteStrategy = waitForImagingJobToCompleteStrategy;
			_imagingJobSubmitSingleDocumentStrategy = imagingJobSubmitSingleDocumentStrategy;
			_imagingJobSubmitMassDocumentStrategy = imagingJobSubmitMassDocumentStrategy;
			_cancelImagingJobStrategy = cancelImagingJobStrategy;
			_imagingJobRetryErrorsStrategy = imagingJobRetryErrorsStrategy;
			_imagingJobUpdatePriorityStrategy = imagingJobUpdatePriorityStrategy;
		}

		public long Run(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null)
			=> _imagingJobRunStrategy.Run(workspaceId, imagingSetId, imagingSetJobRequest);

		public async Task<long> RunAsync(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null)
			=> await _imagingJobRunStrategy.RunAsync(workspaceId, imagingSetId, imagingSetJobRequest).ConfigureAwait(false);

		public long SubmitSingleDocument(int workspaceId, int documentArtifactId, SingleDocumentImagingJobRequest singleDocumentImagingJobRequest)
			=> _imagingJobSubmitSingleDocumentStrategy.SubmitSingleDocument(workspaceId, documentArtifactId, singleDocumentImagingJobRequest);

		public async Task<long> SubmitSingleDocumentAsync(int workspaceId, int documentArtifactId, SingleDocumentImagingJobRequest singleDocumentImagingJobRequest)
			=> await _imagingJobSubmitSingleDocumentStrategy.SubmitSingleDocumentAsync(workspaceId, documentArtifactId, singleDocumentImagingJobRequest).ConfigureAwait(false);

		public long SubmitMassDocument(int workspaceId, ImagingMassJobRequest imagingMassJobRequest)
			=> _imagingJobSubmitMassDocumentStrategy.SubmitMassDocument(workspaceId, imagingMassJobRequest);

		public async Task<long> SubmitMassDocumentAsync(int workspaceId, ImagingMassJobRequest imagingMassJobRequest)
			=> await _imagingJobSubmitMassDocumentStrategy.SubmitMassDocumentAsync(workspaceId, imagingMassJobRequest).ConfigureAwait(false);

		public ImagingJobActionResponse Cancel(int workspaceId, long imagingJobId, ImagingJobRequest cancelImagingJobRequest = null)
			=> _cancelImagingJobStrategy.Cancel(workspaceId, imagingJobId, cancelImagingJobRequest);

		public async Task<ImagingJobActionResponse> CancelAsync(int workspaceId, long imagingJobId, ImagingJobRequest cancelImagingJobRequest = null)
			=> await _cancelImagingJobStrategy.CancelAsync(workspaceId, imagingJobId, cancelImagingJobRequest).ConfigureAwait(false);

		public long RetryErrors(int workspaceId, int imagingSetId, ImagingSetJobRequest retryErrorsRequest = null)
			=> _imagingJobRetryErrorsStrategy.RetryErrors(workspaceId, imagingSetId, retryErrorsRequest);

		public async Task<long> RetryErrorsAsync(int workspaceId, int imagingSetId, ImagingSetJobRequest retryErrorsRequest = null)
			=> await _imagingJobRetryErrorsStrategy.RetryErrorsAsync(workspaceId, imagingSetId, retryErrorsRequest).ConfigureAwait(false);

		public ImagingJobActionResponse UpdatePriority(int workspaceId, long imagingJobId, ImagingJobPriorityRequest updateJobPriorityRequest)
			=> _imagingJobUpdatePriorityStrategy.UpdatePriority(workspaceId, imagingJobId, updateJobPriorityRequest);

		public async Task<ImagingJobActionResponse> UpdatePriorityAsync(int workspaceId, long imagingJobId, ImagingJobPriorityRequest updateJobPriorityRequest)
			=> await _imagingJobUpdatePriorityStrategy.UpdatePriorityAsync(workspaceId, imagingJobId, updateJobPriorityRequest).ConfigureAwait(false);

		public void WaitForTheJobToComplete(int workspaceId, int imagingSetId, double timeout = 2.0)
			=> _waitForImagingJobToCompleteStrategy.Wait(workspaceId, imagingSetId, timeout);

		public async Task WaitForTheJobToCompleteAsync(int workspaceId, int imagingSetId, double timeout = 2.0)
			=> await _waitForImagingJobToCompleteStrategy.WaitAsync(workspaceId, imagingSetId, timeout).ConfigureAwait(false);
	}
}
