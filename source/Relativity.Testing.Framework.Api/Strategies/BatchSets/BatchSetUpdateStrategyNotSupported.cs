using System;
using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class BatchSetUpdateStrategyNotSupported : IUpdateBatchSetStrategy
	{
		public BatchSet Update(int workspaceId, BatchSet entity, UserCredentials userCredentials = null)
		{
			throw new ArgumentException("The method Update does not support version of Relativity lower than 12.1.");
		}
	}
}
