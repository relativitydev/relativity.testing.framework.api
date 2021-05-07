using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AgentDeleteStrategy : DeleteByIdStrategy<Agent>
	{
		private readonly IRestService _restService;

		private readonly IObjectService _objectService;

		private readonly TimeSpan _deletionTimeout = TimeSpan.FromMinutes(1);

		public AgentDeleteStrategy(
			IRestService restService,
			IObjectService objectService)
		{
			_restService = restService;
			_objectService = objectService;
		}

		protected override void DoDelete(int id)
		{
			var dto = new
			{
				force = true
			};

			_restService.Delete($"relativity.agents/workspace/-1/agents/{id}", dto);

			WaitUntilDeleted(id);
		}

		private void WaitUntilDeleted(int id)
		{
			Stopwatch watch = Stopwatch.StartNew();
			bool keepPolling;

			do
			{
				keepPolling = _objectService.Query<Agent>().
					FetchOnlyArtifactID().
					Where(x => x.ArtifactID, id).
					Any();

				if (keepPolling)
				{
					if (watch.Elapsed > _deletionTimeout)
					{
						throw new InvalidOperationException($"Failed to delete an agent with ID={id}.");
					}
					else
					{
						Thread.Sleep(1000);
					}
				}
			}
			while (keepPolling);
		}
	}
}
