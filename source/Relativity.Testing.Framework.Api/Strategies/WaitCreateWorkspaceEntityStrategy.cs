using System;
using System.Diagnostics;
using System.Threading;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the base wait for create workspace entity strategy.
	/// </summary>
	internal class WaitCreateWorkspaceEntityStrategy : IWaitCreateWorkspaceEntityStrategy
	{
		private readonly IRelativityFacade _relativityFacade;
		private readonly TimeSpan _creationTimeout = TimeSpan.FromMinutes(1);

		public WaitCreateWorkspaceEntityStrategy(IRelativityFacade relativityFacade)
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
				keepPolling = strategy.Get(workspaceId, entityId) == null;

				if (keepPolling)
				{
					if (watch.Elapsed > _creationTimeout)
					{
						throw new InvalidOperationException(
							$"Workspace entity with workspaceId={workspaceId}, and entityId={entityId} was not created within the 1 minute time limit." +
							"Please check the error log in Relativity, or confirm that the creation took longer than expected.");
					}

					Thread.Sleep(1000);
				}
			}
			while (keepPolling);
		}
	}
}
