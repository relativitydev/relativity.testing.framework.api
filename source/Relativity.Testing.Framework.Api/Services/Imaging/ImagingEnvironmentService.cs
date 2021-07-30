using System.Threading.Tasks;
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

		public async Task<int> GetMassImagingJobSizeAsync()
			=> await _getMassImagingJobSizeStrategy.GetAsync().ConfigureAwait(false);

		public void RemoveInactiveImagingJobs()
			=> _removeInactiveImagingJobsStrategy.Remove();

		public async Task RemoveInactiveImagingJobsAsync()
			=> await _removeInactiveImagingJobsStrategy.RemoveAsync().ConfigureAwait(false);
	}
}
