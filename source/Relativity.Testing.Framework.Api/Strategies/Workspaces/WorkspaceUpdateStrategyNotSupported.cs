using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class WorkspaceUpdateStrategyNotSupported : IUpdateStrategy<Workspace>
	{
		public void Update(Workspace entity)
		{
			throw new ArgumentException("The method Update does not support version of Relativity lower than 12.1.");
		}
	}
}
