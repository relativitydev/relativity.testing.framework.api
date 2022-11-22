using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Castle.Core;
using Relativity.Testing.Framework.Api.Interceptors;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[Interceptor(typeof(ApplicationInsightsMetricInterceptor))]
	internal class WaitUserAddedToGroupStrategy : IWaitUserAddedToGroupStrategy
	{
		private readonly IInstanceSettingGetByNameAndSectionStrategy _instanceSettingGetByNameAndSectionStrategy;
		private readonly IUserGetGroupsStrategy _userGetGroupsStrategy;
		private readonly TimeSpan _waitTimeout;
		private readonly TimeSpan _pollingDelay;

		public WaitUserAddedToGroupStrategy(
			IInstanceSettingGetByNameAndSectionStrategy instanceSettingGetByNameAndSectionStrategy,
			IUserGetGroupsStrategy userGetGroupsStrategy)
		{
			_instanceSettingGetByNameAndSectionStrategy = instanceSettingGetByNameAndSectionStrategy;
			_userGetGroupsStrategy = userGetGroupsStrategy;
			_waitTimeout = TimeSpan.FromSeconds(300);
			_pollingDelay = TimeSpan.FromSeconds(5);
		}

		public void Wait(int workspaceId, int groupId, int userArtifactId)
		{
			var watch = Stopwatch.StartNew();
			bool keepPolling = true;
			while (keepPolling && watch.Elapsed < _waitTimeout)
			{
				var groups = _userGetGroupsStrategy.GetGroupsByGroupId(userArtifactId, groupId);
				if (groups.Any())
				{
					keepPolling = false;
				}
				else
				{
					Thread.Sleep(_pollingDelay);
				}
			}

			var lockboxEnabled = _instanceSettingGetByNameAndSectionStrategy.Get("EnableCustomerLockbox", "Relativity.Core").Value.Equals("True");
			if (lockboxEnabled)
			{
				Thread.Sleep(30000);
			}
		}
	}
}
