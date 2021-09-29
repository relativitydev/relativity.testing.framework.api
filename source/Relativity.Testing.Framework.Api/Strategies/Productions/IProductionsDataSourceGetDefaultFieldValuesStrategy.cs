using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IProductionsDataSourceGetDefaultFieldValuesStrategy
	{
		ProductionDataSourceDefaultValues Get(int workspaceArtifactID);
	}
}
