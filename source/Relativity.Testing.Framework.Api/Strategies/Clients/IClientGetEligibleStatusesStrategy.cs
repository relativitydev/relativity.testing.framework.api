using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IClientGetEligibleStatusesStrategy
	{
		IList<ArtifactIdNamePair> Get();
	}
}
