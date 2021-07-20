using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingJobRunStrategy
	{
		int Run(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null);

		Task<int> RunAsync(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null);
	}
}
