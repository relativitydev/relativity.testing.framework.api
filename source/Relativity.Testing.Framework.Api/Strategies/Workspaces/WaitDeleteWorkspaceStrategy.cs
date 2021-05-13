using System;
using System.Diagnostics;
using System.Threading;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WaitDeleteWorkspaceStrategy : IWaitDeleteWorkspaceStrategy
	{
		private readonly IRelativityFacade _relativityFacade;
		private readonly TimeSpan _deletionTimeout = TimeSpan.FromSeconds(15);

		public WaitDeleteWorkspaceStrategy(IRelativityFacade relativityFacade)
		{
			_relativityFacade = relativityFacade;
		}

		public void Wait(int workspaceId)
		{
			IExistsByIdStrategy<Workspace> existsById = _relativityFacade.Resolve<IExistsByIdStrategy<Workspace>>();
			Stopwatch watch = Stopwatch.StartNew();
			bool keepPolling;

			do
			{
				keepPolling = existsById.Exists(workspaceId);

				if (keepPolling)
				{
					if (watch.Elapsed > _deletionTimeout)
					{
						throw new InvalidOperationException(
							$"Workspace with id={workspaceId} was not deleted within the 15 second time limit." +
							"Please check the error log in Relativity, or confirm that the deletion took longer than expected.");
					}

					Thread.Sleep(1000);
				}
			}
			while (keepPolling);
		}
	}
}
