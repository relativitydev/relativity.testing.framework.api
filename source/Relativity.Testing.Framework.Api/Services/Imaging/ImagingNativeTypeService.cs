using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ImagingNativeTypeService : IImagingNativeTypeService
	{
		private readonly INativeTypeGetStrategy _nativeTypeGetStrategy;

		public ImagingNativeTypeService(INativeTypeGetStrategy nativeTypeGetStrategy)
		{
			_nativeTypeGetStrategy = nativeTypeGetStrategy;
		}

		public NativeType Get(int workspaceId, int nativeTypeId)
		{
			return _nativeTypeGetStrategy.Get(workspaceId, nativeTypeId);
		}

		public async Task<NativeType> GetAsync(int workspaceId, int nativeTypeId)
		{
			return await _nativeTypeGetStrategy.GetAsync(workspaceId, nativeTypeId).ConfigureAwait(false);
		}
	}
}
