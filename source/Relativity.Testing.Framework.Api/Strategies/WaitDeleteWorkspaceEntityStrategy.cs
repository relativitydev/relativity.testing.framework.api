using System;
using System.Diagnostics;
using System.Threading;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the base wait for delete workspace entity strategy.
	/// </summary>
	internal class WaitDeleteWorkspaceEntityStrategy : IWaitDeleteWorkspaceEntityStrategy
	{
		private readonly IRelativityFacade _relativityFacade;
		private readonly TimeSpan _deletionTimeout = TimeSpan.FromMinutes(1);

		public WaitDeleteWorkspaceEntityStrategy(IRelativityFacade relativityFacade)
		{
			_relativityFacade = relativityFacade;
		}

		public void Wait<T>(int workspaceId, int entityId)
		{
			var strategy = _relativityFacade.Resolve<IGetWorkspaceEntityByIdStrategy<T>>();

			Stopwatch watch = Stopwatch.StartNew();
			bool keepPolling;

			do
			{
				keepPolling = strategy.Get(workspaceId, entityId) != null;

				if (keepPolling)
				{
					if (watch.Elapsed > _deletionTimeout)
					{
						throw new InvalidOperationException(
							$"Workspace entity with workspaceId={workspaceId}, and entityId={entityId} was not deleted within the 1 minute time limit." +
							"Please check the error log in Relativity, or confirm that the deletion took longer than expected.");
					}

					Thread.Sleep(1000);
				}
			}
			while (keepPolling);
		}
	}
}
