using System.Threading;
using Castle.Core;
using Relativity.Testing.Framework.Api.Interceptors;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[Interceptor(typeof(ApplicationInsightsMetricInterceptor))]
	internal class WaitUserAddedToGroupStrategy : IWaitUserAddedToGroupStrategy
	{
		private readonly IInstanceSettingGetByNameAndSectionStrategy _instanceSettingGetByNameAndSectionStrategy;

		public WaitUserAddedToGroupStrategy(
			IInstanceSettingGetByNameAndSectionStrategy instanceSettingGetByNameAndSectionStrategy)
		{
			_instanceSettingGetByNameAndSectionStrategy = instanceSettingGetByNameAndSectionStrategy;
		}

		public void Wait(int workspaceId, int groupId, int userArtifactId)
		{
			var lockboxEnabled = _instanceSettingGetByNameAndSectionStrategy.Get("EnableCustomerLockbox", "Relativity.Core").Value.Equals("True");
			if (lockboxEnabled)
			{
				Thread.Sleep(30000);
			}
		}
	}
}
