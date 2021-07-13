using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingSetStatusGetStrategy
	{
		ImagingSetDetailedStatus Get(int workspaceId, int imagingSetId);

		Task<ImagingSetDetailedStatus> GetAsync(int workspaceId, int imagingSetId);
	}
}
