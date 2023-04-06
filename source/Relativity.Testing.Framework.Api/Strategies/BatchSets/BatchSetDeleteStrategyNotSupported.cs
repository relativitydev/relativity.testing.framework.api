using System;
using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies.BatchSets
{
	[VersionRange("<12.1")]
	internal class BatchSetDeleteStrategyNotSupported : IDeleteBatchSetStrategy
	{
		public void Delete(int workspaceId, int entityId, UserCredentials userCredentials = null)
		{
			throw new ArgumentException("The method Delete does not support version of Relativity lower than 12.1.");
		}
	}
}
