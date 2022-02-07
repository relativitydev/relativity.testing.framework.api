using System;
using System.Diagnostics;
using System.Threading;
using Castle.Core;
using Relativity.Testing.Framework.Api.Interceptors;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[Interceptor(typeof(ApplicationInsightsMetricInterceptor))]
	internal class WaitUserDeletedStrategy : IWaitUserDeletedStrategy
	{
		private readonly IUserGetByEmailStrategy _userGetByEmailStrategy;
		private readonly IGetByIdStrategy<User> _userGetByIdStrategy;
		private readonly TimeSpan _deletionTimeout;

		public WaitUserDeletedStrategy(IUserGetByEmailStrategy userGetByEmailStrategy, IGetByIdStrategy<User> getByIdStrategy, TimeSpan timeSpan = default)
		{
			_userGetByEmailStrategy = userGetByEmailStrategy;
			_userGetByIdStrategy = getByIdStrategy;
			_deletionTimeout = timeSpan == default ? TimeSpan.FromSeconds(300) : timeSpan;
		}

		public void Wait(int artifactId)
		{
			var watch = Stopwatch.StartNew();
			bool keepPolling;

			do
			{
				keepPolling = _userGetByIdStrategy.Get(artifactId) != null;

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
				keepPolling = _userGetByEmailStrategy.Get(email) != null;

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
