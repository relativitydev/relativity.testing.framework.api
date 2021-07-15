using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingSetDeleteStrategy
	{
		void Delete(int workspaceId, int imagingSetId);

		Task DeleteAsync(int workspaceId, int imagingSetId);
	}
}
