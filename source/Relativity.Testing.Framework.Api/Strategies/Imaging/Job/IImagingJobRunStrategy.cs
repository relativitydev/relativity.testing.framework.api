using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingJobRunStrategy
	{
		long Run(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null);

		Task<long> RunAsync(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null);
	}
}
