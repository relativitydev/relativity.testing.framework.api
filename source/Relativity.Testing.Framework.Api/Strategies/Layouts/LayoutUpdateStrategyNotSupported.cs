using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12")]
	internal class LayoutUpdateStrategyNotSupported : IUpdateWorkspaceEntityStrategy<Layout>
	{
		public void Update(int workspaceId, Layout entity)
		{
			throw new ArgumentException("The method Update does not support version of Relativity lower than 12.");
		}
	}
}
