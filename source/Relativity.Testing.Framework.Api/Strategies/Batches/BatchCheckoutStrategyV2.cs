using System;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class BatchCheckoutStrategyV2 : IBatchCheckoutStrategy
	{
		public void Checkout(int workspaceId, int batchId, int userId)
		{
			throw new ArgumentException("The method Checkout does not support version of Relativity lower than 12.1.");
		}
	}
}
