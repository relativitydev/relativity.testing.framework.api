using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.0")]
	internal class LayoutRequireStrategyV2 : IRequireWorkspaceEntityStrategy<Layout>
	{
		public Layout Require(int workspaceId, Layout entity)
		{
			throw new ArgumentException("The method Require does not support version of Relativity lower than 12.");
		}
	}
}
