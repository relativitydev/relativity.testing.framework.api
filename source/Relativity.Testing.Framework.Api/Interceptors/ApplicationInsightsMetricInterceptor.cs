using System.Collections.Generic;
using Castle.DynamicProxy;
using Microsoft.ApplicationInsights;

namespace Relativity.Testing.Framework.Api.Interceptors
{
	public class ApplicationInsightsMetricInterceptor : ApplicationInsightsInterceptor
	{
		public ApplicationInsightsMetricInterceptor(IRelativityFacade relativityFacade)
			: base(relativityFacade)
		{
		}

		public override void Intercept(IInvocation invocation)
		{
#pragma warning disable CS0618 // Type or member is obsolete
			if (IsEnabled)
#pragma warning restore CS0618 // Type or member is obsolete
			{
				var processingTime = DoInvocation(invocation);
				var properties = BuildInvocationProperties(invocation);
				Track(processingTime, invocation, properties);
			}
			else
			{
				invocation.Proceed();
			}
		}

		private void Track(double processingTime, IInvocation invocation, Dictionary<string, string> properties)
		{
			var metricName = $"Processing time of {invocation.TargetType.Name}.{invocation.Method.Name}";
			TelemetryClient.TrackMetric(metricName, processingTime, properties);
		}
	}
}
