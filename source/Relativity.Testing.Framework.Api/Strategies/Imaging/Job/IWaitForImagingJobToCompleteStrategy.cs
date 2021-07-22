using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IWaitForImagingJobToCompleteStrategy
	{
		void Wait(int workspaceId, int imagingSetId, double timeout = 2.0);

		Task WaitAsync(int workspaceId, int imagingSetId, double timeout = 2.0);
	}
}
