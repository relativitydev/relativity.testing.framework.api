using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ImagingProfileService : IImagingProfileService
	{
		private readonly IImagingProfileCreateBasicStrategy _basicImagingProfileCreateStrategy;
		private readonly IImagingProfileCreateNativeStrategy _nativeImagingProfileCreateStrategy;
		private readonly IImagingProfileUpdateStrategy _imagingProfileUpdateStrategy;
		private readonly IImagingProfileGetStrategy _imagingProfileGetStrategy;
		private readonly IImagingProfileDeleteStrategy _imagingProfileDeleteStrategy;

		public ImagingProfileService(
			IImagingProfileCreateBasicStrategy basicImagingProfileCreateStrategy,
			IImagingProfileCreateNativeStrategy nativeImagingProfileCreateStrategy,
			IImagingProfileUpdateStrategy imagingProfileUpdateStrategy,
			IImagingProfileGetStrategy imagingProfileGetStrategy,
			IImagingProfileDeleteStrategy imagingProfileDeleteStrategy)
		{
			_basicImagingProfileCreateStrategy = basicImagingProfileCreateStrategy;
			_nativeImagingProfileCreateStrategy = nativeImagingProfileCreateStrategy;
			_imagingProfileUpdateStrategy = imagingProfileUpdateStrategy;
			_imagingProfileGetStrategy = imagingProfileGetStrategy;
			_imagingProfileDeleteStrategy = imagingProfileDeleteStrategy;
		}

		public ImagingProfile CreateBasic(int workspaceId, CreateBasicImagingProfileDTO dto)
			=> _basicImagingProfileCreateStrategy.Create(workspaceId, dto);

		public ImagingProfile CreateNative(int workspaceId, CreateNativeImagingProfileDTO dto)
			=> _nativeImagingProfileCreateStrategy.Create(workspaceId, dto);

		public ImagingProfile Update(int workspaceId, ImagingProfile imagingProfile)
			=> _imagingProfileUpdateStrategy.Update(workspaceId, imagingProfile);

		public ImagingProfile Get(int workspaceId, int imagingProfileId)
			=> _imagingProfileGetStrategy.Get(workspaceId, imagingProfileId);

		public void Delete(int workspaceId, int imagingProfileId)
			=> _imagingProfileDeleteStrategy.Delete(workspaceId, imagingProfileId);
	}
}
