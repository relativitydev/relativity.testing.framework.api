using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Castle.DynamicProxy;

namespace Relativity.Testing.Framework.Api.Interceptors
{
	public class RetryInterceptor : IInterceptor
	{
		private readonly IRelativityFacade _relativityFacade;
		private bool _isSendMetrix;

		public RetryInterceptor(IRelativityFacade relativityFacade)
		{
			_relativityFacade = relativityFacade;
		}

		public void Intercept(IInvocation invocation)
		{
			ExecuteRetry(invocation, 2);

			if (_isSendMetrix)
			{
				SendInfo(invocation);
			}
		}

		private void ExecuteRetry(IInvocation invocation, int retryCount)
		{
			bool isSucceeded = false;
			do
			{
				try
				{
					invocation.Proceed();

					isSucceeded = true;
				}
				catch (Exception ex)
				{
					if (!ex.Message.Contains("'Internal Server Error'") || retryCount == 0)
					{
						throw;
					}

					_isSendMetrix = true;

					Thread.Sleep(5000);
				}
				finally
				{
					retryCount--;
				}
			}
			while (!isSucceeded && retryCount >= 0);
		}

		private void SendInfo(IInvocation invocation)
		{
			var properties = new Dictionary<string, string>();

			properties.Add("RelativityVersion", _relativityFacade.RelativityInstanceVersion);
			properties.Add("Class", invocation.TargetType.Name);
			properties.Add("Method", invocation.Method.Name);
			properties.Add("Parameters", string.Join(" && ", invocation.Arguments.Where(x => x != null)));
			properties.Add("RelativityTestingFrameworkVersion", Assembly.GetAssembly(typeof(IApplicationInsightsTelemetryClient)).GetName().Version.ToString());
			properties.Add("RetrySucceeded", "true");

			_relativityFacade.Resolve<IApplicationInsightsTelemetryClient>().Instance
				.TrackEvent($"{invocation.TargetType.Name}.{invocation.Method.Name}", properties);
		}
	}
}
