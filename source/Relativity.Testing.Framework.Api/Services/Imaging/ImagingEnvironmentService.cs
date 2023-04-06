using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Services.Imaging
{
	internal class ImagingEnvironmentService : IImagingEnvironmentService
	{
		private readonly IGetMassImagingJobSizeStrategy _getMassImagingJobSizeStrategy;
		private readonly IRemoveInactiveImagingJobsStrategy _removeInactiveImagingJobsStrategy;

		public ImagingEnvironmentService(
			IGetMassImagingJobSizeStrategy getMassImagingJobSizeStrategy,
			IRemoveInactiveImagingJobsStrategy removeInactiveImagingJobsStrategy)
		{
			_getMassImagingJobSizeStrategy = getMassImagingJobSizeStrategy;
			_removeInactiveImagingJobsStrategy = removeInactiveImagingJobsStrategy;
		}

		public int GetMassImagingJobSize()
			=> _getMassImagingJobSizeStrategy.Get();

		public void RemoveInactiveImagingJobs()
			=> _removeInactiveImagingJobsStrategy.Remove();
	}
}
