using System;
using System.Collections.Generic;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class TabGetEligibleParentsStrategyNotSupported : ITabGetEligibleParentsStrategy
	{
		List<TabEligibleParent> ITabGetEligibleParentsStrategy.Get(int workspaceId)
		{
			throw new ArgumentException("The method Get does not support version of Relativity lower than 12.1.");
		}
	}
}
