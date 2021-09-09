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
		private readonly IImagingSetReleaseStrategy _imagingSetReleaseStrategy;

		public ImagingSetService(
			IImagingSetCreateStrategy imagingSetCreateStrategy,
			IImagingSetGetStrategy imagingSetGetStrategy,
			IImagingSetStatusGetStrategy imagingSetStatusGetStrategy,
			IImagingSetHideStrategy imagingSetHideStrategy,
			IImagingSetUpdateStrategy imagingSetUpdateStrategy,
			IImagingSetDeleteStrategy imagingSetDeleteStrategy,
			IImagingSetReleaseStrategy imagingSetReleaseStrategy)
		{
			_imagingSetCreateStrategy = imagingSetCreateStrategy;
			_imagingSetGetStrategy = imagingSetGetStrategy;
			_imagingSetUpdateStrategy = imagingSetUpdateStrategy;
			_imagingSetStatusGetStrategy = imagingSetStatusGetStrategy;
			_imagingSetDeleteStrategy = imagingSetDeleteStrategy;
			_imagingSetHideStrategy = imagingSetHideStrategy;
			_imagingSetReleaseStrategy = imagingSetReleaseStrategy;
		}

		public ImagingSet Create(int workspaceId, ImagingSetRequest imagingSetRequest)
			=> _imagingSetCreateStrategy.Create(workspaceId, imagingSetRequest);

		public void Delete(int workspaceId, int imagingSetId)
			=> _imagingSetDeleteStrategy.Delete(workspaceId, imagingSetId);

		public ImagingSet Get(int workspaceId, int imagingSetId)
			=> _imagingSetGetStrategy.Get(workspaceId, imagingSetId);

		public int Update(int workspaceId, int imagingSetId, ImagingSetRequest imagingSetRequest)
			=> _imagingSetUpdateStrategy.Update(workspaceId, imagingSetId, imagingSetRequest);

		public ImagingSetDetailedStatus GetStatus(int workspaceId, int imagingSetId)
			=> _imagingSetStatusGetStrategy.Get(workspaceId, imagingSetId);

		public void Hide(int workspaceId, int imagingSetId)
			=> _imagingSetHideStrategy.Hide(workspaceId, imagingSetId);

		public void Release(int workspaceId, int imagingSetId)
			=> _imagingSetReleaseStrategy.Release(workspaceId, imagingSetId);
	}
}
