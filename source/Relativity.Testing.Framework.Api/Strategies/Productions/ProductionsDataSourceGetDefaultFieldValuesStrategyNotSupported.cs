using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ProductionsDataSourceGetDefaultFieldValuesStrategyNotSupported : IProductionsDataSourceGetDefaultFieldValuesStrategy
	{
		public ProductionDataSourceDefaultValues Get(int workspaceArtifactID)
		{
			throw new System.ArgumentException("The method Get Productions Data Source Default Field Values does not support version of Relativity lower than 12.1.");
		}
	}
}
