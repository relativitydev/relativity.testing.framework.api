using System;
using System.Diagnostics;
using System.Threading;
using Castle.Core;
using Relativity.Testing.Framework.Api.Interceptors;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[Interceptor(typeof(ApplicationInsightsMetricInterceptor))]
	internal class WaitUserDeletedStrategy : IWaitUserDeletedStrategy
	{
		private readonly IExistsByIdStrategy<User> _existsByIdStrategy;
		private readonly IUserExistsByEmailStrategy _existsByEmailStrategy;
		private readonly TimeSpan _deletionTimeout;

		public WaitUserDeletedStrategy(IExistsByIdStrategy<User> existsByIdStrategy, IUserExistsByEmailStrategy existsByEmailStrategy, TimeSpan timeSpan = default)
		{
			_existsByIdStrategy = existsByIdStrategy;
			_existsByEmailStrategy = existsByEmailStrategy;
			_deletionTimeout = timeSpan == default ? TimeSpan.FromSeconds(300) : timeSpan;
		}

		public void Wait(int artifactId)
		{
			var watch = Stopwatch.StartNew();
			bool keepPolling;

			do
			{
				keepPolling = _existsByIdStrategy.Exists(artifactId);

				if (keepPolling)
				{
					if (watch.Elapsed > _deletionTimeout)
					{
						throw new InvalidOperationException($"The user with the ArtifactID {artifactId} was not deleted after {_deletionTimeout.TotalSeconds} seconds.");
					}
					else
					{
						Thread.Sleep(1000);
					}
				}
			}
			while (keepPolling);
		}

		public void Wait(string email)
		{
			var watch = Stopwatch.StartNew();
			bool keepPolling;

			do
			{
				keepPolling = _existsByEmailStrategy.Exists(email);

				if (keepPolling)
				{
					if (watch.Elapsed > _deletionTimeout)
					{
						throw new InvalidOperationException($"The user with the email address {email} was not deleted after {_deletionTimeout.TotalSeconds} seconds.");
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
