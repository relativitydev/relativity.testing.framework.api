using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IUpdateProductionsDataSourceStrategy
	{
		ProductionDataSource Update(int workspaceId, int productionId, ProductionDataSource entity);
	}
}
