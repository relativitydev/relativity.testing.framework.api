using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingSetUpdateStrategy
	{
		int Update(int workspaceId, int imagingSetId, ImagingSetRequest imagingSetRequest);
	}
}
