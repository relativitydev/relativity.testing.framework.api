using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12")]
	internal class LayoutGetByIdStrategyV2 : IGetWorkspaceEntityByIdStrategy<Layout>
	{
		public Layout Get(int workspaceId, int entityId)
		{
			throw new ArgumentException("The method Get does not support version of Relativity lower than 12.");
		}
	}
}
