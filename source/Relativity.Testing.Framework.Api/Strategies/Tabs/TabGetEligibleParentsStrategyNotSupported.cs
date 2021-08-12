using System;
using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies.Tabs.DTO;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies.Tabs
{
	[VersionRange("<12.1")]
	internal class TabGetEligibleParentsStrategyNotSupported : ITabGetEligibleParentsStrategy
	{
		List<TabEligibleParentV1> ITabGetEligibleParentsStrategy.Get(int id)
		{
			throw new ArgumentException("The method Get does not support version of Relativity lower than 12.1.");
		}
	}
}
