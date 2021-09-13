using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingSetGetStrategy
	{
		ImagingSet Get(int workspaceId, int imagingSetId);
	}
}
