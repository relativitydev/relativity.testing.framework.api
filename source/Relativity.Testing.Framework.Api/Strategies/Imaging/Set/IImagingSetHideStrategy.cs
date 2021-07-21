using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingSetHideStrategy
	{
		void Hide(int workspaceId, int imagingSetId);

		Task HideAsync(int workspaceId, int imagingSetId);
	}
}
