using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12")]
	internal class LayoutCreateStrategyNotSupported : ICreateWorkspaceEntityStrategy<Layout>
	{
		public Layout Create(int workspaceId, Layout entity)
		{
			throw new ArgumentException("The method Create does not support version of Relativity lower than 12.");
		}
	}
}
