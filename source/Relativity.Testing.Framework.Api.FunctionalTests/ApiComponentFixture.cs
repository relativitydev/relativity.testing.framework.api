using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Configuration;

namespace Relativity.Testing.Framework.Api.FunctionalTests
{
	[TestOf(typeof(ApiComponent))]
	public class ApiComponentFixture : ApiTestFixture
	{
		[SetUp]
		public void SetUp()
		{
			FacadeHost.SetUpFacade();
		}

		[Test]
		public void RelyOn_WithoutCoreComponent()
		{
			Assert.Throws<RelativityComponentEnsuringException>(() =>
				Facade.RelyOn<ApiComponent>());
		}

		[Test]
		public void RelyOn_Default()
		{
			FacadeHost.AddCoreToFacade();

			Facade.RelyOn<ApiComponent>();

			Facade.Resolve<IRestService>().Should().NotBeNull();
		}

		[Test]
		public void RelyOn_WithCoreComponentWithoutConfiguration()
		{
			Facade.RelyOn(new CoreComponent
			{
				ConfigurationRoot = new ConfigurationBuilder().Build()
			});

			var exception = Assert.Throws<RelativityComponentEnsuringException>(() =>
				Facade.RelyOn<ApiComponent>());

			exception.InnerException.Should().BeOfType<ConfigurationKeyNotFoundException>();
		}
	}
}
