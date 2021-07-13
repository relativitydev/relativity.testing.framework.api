using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingSetGetStrategy
	{
		ImagingSet Get(int workspaceId, int imagingSetId);

		Task<ImagingSet> GetAsync(int workspaceId, int imagingSetId);
	}
}
