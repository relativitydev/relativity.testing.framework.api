using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ImagingJobService : IImagingJobService
	{
		private readonly IImagingJobRunStrategy _imagingJobRunStrategy;

		public ImagingJobService(IImagingJobRunStrategy imagingJobRunStrategy)
		{
			_imagingJobRunStrategy = imagingJobRunStrategy;
		}

		public int Run(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null)
			=> _imagingJobRunStrategy.Run(workspaceId, imagingSetId, imagingSetJobRequest);

		public async Task<int> RunAsync(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null)
			=> await _imagingJobRunStrategy.RunAsync(workspaceId, imagingSetId, imagingSetJobRequest).ConfigureAwait(false);
	}
}
