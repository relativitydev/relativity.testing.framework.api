using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingJobUpdatePriorityStrategy
	{
		ImagingJobActionResponse UpdatePriority(int workspaceId, long imagingJobId, ImagingJobPriorityRequest updateJobPriorityRequest);

		Task<ImagingJobActionResponse> UpdatePriorityAsync(int workspaceId, long imagingJobId, ImagingJobPriorityRequest updateJobPriorityRequest);
	}
}
