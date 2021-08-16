using System.Collections.Generic;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface ITabGetEligibleParentsStrategy
	{
		List<TabEligibleParent> Get(int workspaceId);
	}
}
