using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies.Tabs.DTO;

namespace Relativity.Testing.Framework.Api.Strategies.Tabs
{
	internal interface ITabGetEligibleParentsStrategy
	{
		List<TabEligibleParentV1> Get(int id);
	}
}
