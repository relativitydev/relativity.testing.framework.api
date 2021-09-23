using System;
using System.Collections.Generic;
using Castle.DynamicProxy;

namespace Relativity.Testing.Framework.Api.Interceptors
{
	public class ApplicationInsightsEventInterceptor : ApplicationInsightsInterceptor
	{
		public ApplicationInsightsEventInterceptor(IRelativityFacade relativityFacade)
			: base(relativityFacade)
		{
		}

		public override void Intercept(IInvocation invocation)
		{
#pragma warning disable CS0618 // Type or member is obsolete
			if (IsEnabled)
#pragma warning restore CS0618 // Type or member is obsolete
			{
				try
				{
					double processingTime = DoInvocation(invocation);
					Dictionary<string, string> properties = BuildInvocationProperties(invocation);
					Track(processingTime, invocation, properties);
				}
				catch (Exception ex)
				{
					try
					{
						Dictionary<string, string> properties = BuildInvocationProperties(invocation);
						TelemetryClient.TrackException(ex, properties);
					}
					catch
					{
						TelemetryClient.TrackException(ex);
					}

					throw;
				}
			}
			else
			{
				invocation.Proceed();
			}
		}

		private void Track(double processingTime, IInvocation invocation, Dictionary<string, string> properties)
		{
			var metrics = new Dictionary<string, double> { { "ProcessingTime", processingTime } };
			TelemetryClient.TrackEvent($"{invocation.TargetType.Name}.{invocation.Method.Name}", properties, metrics);
		}
	}
}
