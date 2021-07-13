using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingSetCreateStrategy
	{
		ImagingSet Create(int workspaceId, ImagingSetRequest imagingSetRequest);

		Task<ImagingSet> CreateAsync(int workspaceId, ImagingSetRequest imagingSetRequest);
	}
}
