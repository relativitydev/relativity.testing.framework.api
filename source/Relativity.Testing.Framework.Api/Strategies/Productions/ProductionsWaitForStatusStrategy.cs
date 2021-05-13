using System;
using System.Diagnostics;
using System.Threading;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionsWaitForStatusStrategy : IProductionsWaitForStatusStrategy
	{
		private readonly IRelativityFacade _relativityFacade;

		public ProductionsWaitForStatusStrategy(IRelativityFacade relativityFacade)
		{
			_relativityFacade = relativityFacade;
		}

		public void WaitForStatus(int workspaceId, int entityId, ProductionStatus expectedStatus, int seconds)
		{
			var strategy = _relativityFacade.Resolve<IGetProductionStatusStrategy>();

			Stopwatch watch = Stopwatch.StartNew();
			bool keepPolling;
			var timeout = TimeSpan.FromSeconds(seconds);

			do
			{
				keepPolling = strategy.GetStatus(workspaceId, entityId) != expectedStatus;

				if (keepPolling)
				{
					if (strategy.GetStatus(workspaceId, entityId).ToString().Contains("Error"))
					{
						throw new Exception($"Production {entityId} is in error status on workspaceId={workspaceId}");
					}

					if (watch.Elapsed > timeout)
					{
						throw new InvalidOperationException($"Failed to wait on expected status {expectedStatus} of a production={entityId} " +
							$"on workspaceId={workspaceId} with timeout={timeout}");
					}

					Thread.Sleep(TimeSpan.FromSeconds(1));
				}
			}
			while (keepPolling);
		}
	}
}
