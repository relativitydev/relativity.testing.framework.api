﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Castle.Core;
using Relativity.Testing.Framework.Api.Interceptors;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[Interceptor(typeof(ApplicationInsightsMetricInterceptor))]
	internal class WaitUserRemoveFromGroupStrategy : IWaitUserRemoveFromGroupStrategy
	{
		private readonly IUserGetGroupsStrategy _userGetGroupsStrategy;
		private readonly TimeSpan _waitTimeout;
		private readonly TimeSpan _pollingDelay;

		public WaitUserRemoveFromGroupStrategy(IUserGetGroupsStrategy userGetGroupsStrategy)
		{
			_userGetGroupsStrategy = userGetGroupsStrategy;
			_waitTimeout = TimeSpan.FromSeconds(300);
			_pollingDelay = TimeSpan.FromSeconds(5);
		}

		public void Wait(int groupId, int userArtifactId)
		{
			var watch = new Stopwatch();
			bool keepPolling = true;
			while (keepPolling && watch.Elapsed < _waitTimeout)
			{
				var groups = _userGetGroupsStrategy.GetGroups(userArtifactId);
				if (groups.FirstOrDefault(g => g.ArtifactID == groupId) == null)
				{
					keepPolling = false;
				}
				else
				{
					Thread.Sleep(_pollingDelay);
				}
			}
		}
	}
}
