using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IBatchSetDTOMapStrategyV1
	{
		BatchSet Map(BatchSetDetailedDTOV1 batchSetDto);
	}
}
