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

		public long SubmitSingleDocument(int workspaceId, int documentArtifactId, SingleDocumentImagingJobRequest singleDocumentImagingJobRequest)
			=> _imagingJobSubmitSingleDocumentStrategy.SubmitSingleDocument(workspaceId, documentArtifactId, singleDocumentImagingJobRequest);

		public long SubmitMassDocument(int workspaceId, ImagingMassJobRequest imagingMassJobRequest)
			=> _imagingJobSubmitMassDocumentStrategy.SubmitMassDocument(workspaceId, imagingMassJobRequest);

		public ImagingJobActionResponse Cancel(int workspaceId, long imagingJobId, ImagingJobRequest cancelImagingJobRequest = null)
			=> _cancelImagingJobStrategy.Cancel(workspaceId, imagingJobId, cancelImagingJobRequest);

		public long RetryErrors(int workspaceId, int imagingSetId, ImagingSetJobRequest retryErrorsRequest = null)
			=> _imagingJobRetryErrorsStrategy.RetryErrors(workspaceId, imagingSetId, retryErrorsRequest);

		public ImagingJobActionResponse UpdatePriority(int workspaceId, long imagingJobId, ImagingJobPriorityRequest updateJobPriorityRequest)
			=> _imagingJobUpdatePriorityStrategy.UpdatePriority(workspaceId, imagingJobId, updateJobPriorityRequest);

		public void WaitForTheJobToComplete(int workspaceId, int imagingSetId, double timeout = 2.0)
			=> _waitForImagingJobToCompleteStrategy.Wait(workspaceId, imagingSetId, timeout);
	}
}
