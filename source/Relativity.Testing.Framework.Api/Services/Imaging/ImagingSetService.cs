using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ImagingSetService : IImagingSetService
	{
		private readonly IImagingSetCreateStrategy _imagingSetCreateStrategy;
		private readonly IImagingSetGetStrategy _imagingSetGetStrategy;
		private readonly IImagingSetStatusGetStrategy _imagingSetStatusGetStrategy;
		private readonly IImagingSetUpdateStrategy _imagingSetUpdateStrategy;
		private readonly IImagingSetDeleteStrategy _imagingSetDeleteStrategy;
		private readonly IImagingSetHideStrategy _imagingSetHideStrategy;

		public ImagingSetService(
			IImagingSetCreateStrategy imagingSetCreateStrategy,
			IImagingSetGetStrategy imagingSetGetStrategy,
			IImagingSetStatusGetStrategy imagingSetStatusGetStrategy,
			IImagingSetHideStrategy imagingSetHideStrategy,
			IImagingSetUpdateStrategy imagingSetUpdateStrategy,
			IImagingSetDeleteStrategy imagingSetDeleteStrategy)
		{
			_imagingSetCreateStrategy = imagingSetCreateStrategy;
			_imagingSetGetStrategy = imagingSetGetStrategy;
			_imagingSetUpdateStrategy = imagingSetUpdateStrategy;
			_imagingSetStatusGetStrategy = imagingSetStatusGetStrategy;
			_imagingSetDeleteStrategy = imagingSetDeleteStrategy;
			_imagingSetHideStrategy = imagingSetHideStrategy;
		}

		public ImagingSet Create(int workspaceId, ImagingSetRequest imagingSetRequest)
			=> _imagingSetCreateStrategy.Create(workspaceId, imagingSetRequest);

		public async Task<ImagingSet> CreateAsync(int workspaceId, ImagingSetRequest imagingSetRequest)
			=> await _imagingSetCreateStrategy.CreateAsync(workspaceId, imagingSetRequest).ConfigureAwait(false);

		public void Delete(int workspaceId, int imagingSetId)
			=> _imagingSetDeleteStrategy.Delete(workspaceId, imagingSetId);

		public async Task DeleteAsync(int workspaceId, int imagingSetId)
			=> await _imagingSetDeleteStrategy.DeleteAsync(workspaceId, imagingSetId).ConfigureAwait(false);

		public ImagingSet Get(int workspaceId, int imagingSetId)
			=> _imagingSetGetStrategy.Get(workspaceId, imagingSetId);

		public async Task<ImagingSet> GetAsync(int workspaceId, int imagingSetId)
			=> await _imagingSetGetStrategy.GetAsync(workspaceId, imagingSetId).ConfigureAwait(false);

		public int Update(int workspaceId, int imagingSetId, ImagingSetRequest imagingSetRequest)
			=> _imagingSetUpdateStrategy.Update(workspaceId, imagingSetId, imagingSetRequest);

		public async Task<int> UpdateAsync(int workspaceId, int imagingSetId, ImagingSetRequest imagingSetRequest)
			=> await _imagingSetUpdateStrategy.UpdateAsync(workspaceId, imagingSetId, imagingSetRequest).ConfigureAwait(false);

		public ImagingSetDetailedStatus GetStatus(int workspaceId, int imagingSetId)
			=> _imagingSetStatusGetStrategy.Get(workspaceId, imagingSetId);

		public async Task<ImagingSetDetailedStatus> GetStatusAsync(int workspaceId, int imagingSetId)
			=> await _imagingSetStatusGetStrategy.GetAsync(workspaceId, imagingSetId).ConfigureAwait(false);

		public void Hide(int workspaceId, int imagingSetId)
			=> _imagingSetHideStrategy.Hide(workspaceId, imagingSetId);

		public async Task HideAsync(int workspaceId, int imagingSetId)
			=> await _imagingSetHideStrategy.HideAsync(workspaceId, imagingSetId).ConfigureAwait(false);
	}
}
