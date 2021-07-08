using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingProfileDeleteStrategy
	{
		void Delete(int workspaceId, int imagingProfileId);

		Task DeleteAsync(int workspaceId, int imagingProfileId);
	}
}
