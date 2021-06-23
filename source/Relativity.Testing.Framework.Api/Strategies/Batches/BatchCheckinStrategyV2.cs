using System;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class BatchCheckinStrategyV2 : IBatchCheckinStrategy
	{
		public void Checkin(int workspaceId, int batchId, bool isCompleted)
		{
			throw new ArgumentException("The method Checkin does not support version of Relativity lower than 12.1.");
		}
	}
}
