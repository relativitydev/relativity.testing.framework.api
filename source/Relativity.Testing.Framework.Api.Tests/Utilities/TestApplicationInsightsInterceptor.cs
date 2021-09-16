using System;
using Castle.DynamicProxy;
using Relativity.Testing.Framework.Api.Interceptors;

namespace Relativity.Testing.Framework.Api.Tests.Utilities
{
	/// <summary>
	/// This class exists to ensure that tweaking a production interceptor
	/// won't have a chance of impacting tests of the base-class functionality.
	/// </summary>
	public class TestApplicationInsightsInterceptor : ApplicationInsightsInterceptor
	{
		public TestApplicationInsightsInterceptor(IRelativityFacade facade)
			: base(facade)
		{
		}

		public override void Intercept(IInvocation invocation)
		{
			// Nothing (yet), just need access to public properties
			throw new NotImplementedException();
		}
	}
}
