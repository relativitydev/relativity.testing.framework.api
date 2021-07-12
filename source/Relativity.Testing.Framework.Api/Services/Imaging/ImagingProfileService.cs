using System.Threading.Tasks;
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

		public async Task<ImagingProfile> CreateBasicAsync(int workspaceId, CreateBasicImagingProfileDTO dto)
			=> await _basicImagingProfileCreateStrategy.CreateAsync(workspaceId, dto).ConfigureAwait(false);

		public ImagingProfile CreateNative(int workspaceId, CreateNativeImagingProfileDTO dto)
			=> _nativeImagingProfileCreateStrategy.Create(workspaceId, dto);

		public async Task<ImagingProfile> CreateNativeAsync(int workspaceId, CreateNativeImagingProfileDTO dto)
			=> await _nativeImagingProfileCreateStrategy.CreateAsync(workspaceId, dto).ConfigureAwait(false);

		public void Update(int workspaceId, ImagingProfile imagingProfile)
			=> _imagingProfileUpdateStrategy.Update(workspaceId, imagingProfile);

		public async Task UpdateAsync(int workspaceId, ImagingProfile imagingProfile)
			=> await _imagingProfileUpdateStrategy.UpdateAsync(workspaceId, imagingProfile).ConfigureAwait(false);

		public ImagingProfile Get(int workspaceId, int imagingProfileId)
			=> _imagingProfileGetStrategy.Get(workspaceId, imagingProfileId);

		public async Task<ImagingProfile> GetAsync(int workspaceId, int imagingProfileId)
			=> await _imagingProfileGetStrategy.GetAsync(workspaceId, imagingProfileId).ConfigureAwait(false);

		public void Delete(int workspaceId, int imagingProfileId)
			=> _imagingProfileDeleteStrategy.Delete(workspaceId, imagingProfileId);

		public async Task DeleteAsync(int workspaceId, int imagingProfileId)
			=> await _imagingProfileDeleteStrategy.DeleteAsync(workspaceId, imagingProfileId).ConfigureAwait(false);
	}
}
