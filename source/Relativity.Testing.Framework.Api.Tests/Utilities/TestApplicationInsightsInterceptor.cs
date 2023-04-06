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

		/// <summary>
		/// No-Op.
		/// This class does not need an implementation (yet).
		/// It only exists to enable test classes to exercise any business logic of the base class.
		/// </summary>
		/// <param name="invocation">An ignored invocation instance.</param>
		public override void Intercept(IInvocation invocation)
		{
			// There may be cases in the future where we want to exercise other methods directly,
			// not in the context of the implementing class (e.g. BuildInvocationProperties).
			// A lot of times, it makes more sense to test the implementation directly.
		}
	}
}
