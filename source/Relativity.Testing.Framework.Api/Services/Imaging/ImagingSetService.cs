using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ImagingSetService : IImagingSetService
	{
		private readonly IImagingSetCreateStrategy _imagingSetCreateStrategy;
		private readonly IImagingSetGetStrategy _imagingSetGetStrategy;

		public ImagingSetService(
			IImagingSetCreateStrategy imagingSetCreateStrategy,
			IImagingSetGetStrategy imagingSetGetStrategy)
		{
			_imagingSetCreateStrategy = imagingSetCreateStrategy;
			_imagingSetGetStrategy = imagingSetGetStrategy;
		}

		public ImagingSet Create(int workspaceId, ImagingSetRequest imagingSetRequest)
			=> _imagingSetCreateStrategy.Create(workspaceId, imagingSetRequest);

		public async Task<ImagingSet> CreateAsync(int workspaceId, ImagingSetRequest imagingSetRequest)
			=> await _imagingSetCreateStrategy.CreateAsync(workspaceId, imagingSetRequest).ConfigureAwait(false);

		public ImagingSet Get(int workspaceId, int imagingSetId)
			=> _imagingSetGetStrategy.Get(workspaceId, imagingSetId);

		public async Task<ImagingSet> GetAsync(int workspaceId, int imagingSetId)
			=> await _imagingSetGetStrategy.GetAsync(workspaceId, imagingSetId).ConfigureAwait(false);
	}
}
