using System;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Configuration;

namespace Relativity.Testing.Framework.Api.FunctionalTests
{
	[TestOf(typeof(HttpService))]
	[TestFixture]
	public class HttpServiceFixture : ApiTestFixture
	{
		private HttpService _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new HttpService(
				$"{RelativityInstanceConfiguration.ServerBindingType}://{RelativityInstanceConfiguration.RestServicesHostAddress}",
				RelativityInstanceConfiguration.AdminUsername,
				RelativityInstanceConfiguration.AdminPassword);
		}

		[Test]
		public void Post()
		{
			string result = _sut.Post<string>("/relativity.rest/api/Relativity.Services.InstanceDetails.IInstanceDetailsModule/InstanceDetailsService/GetRelativityVersionAsync");

			result.Should().NotBeNullOrWhiteSpace();
		}

		[Test]
		public void Post_RawResponse_OK()
		{
			using (HttpResponseMessage result = _sut.Post<HttpResponseMessage>("/relativity.rest/api/Relativity.Services.InstanceDetails.IInstanceDetailsModule/InstanceDetailsService/GetRelativityVersionAsync"))
			{
				result.StatusCode.Should().Be(HttpStatusCode.OK);
			}
		}

		[Test]
		public void Get_NotFound()
		{
			Exception exception = Assert.Throws<HttpRequestException>(() =>
				_sut.Get<string>("/relativity.rest/api/unknownendpoint"));

			exception.Message.Should().ContainAll("StatusCode: 404", "Not Found");
		}

		[Test]
		public void Get_MethodNotAllowed()
		{
			Exception exception = Assert.Throws<HttpRequestException>(() =>
				_sut.Get<string>("/relativity.rest/api/Relativity.Services.InstanceDetails.IInstanceDetailsModule/InstanceDetailsService/GetRelativityVersionAsync"));

			exception.Message.Should().ContainAll("StatusCode: 405", "Method Not Allowed");
		}

		[Test]
		public void Get_RawResponse_MethodNotAllowed()
		{
			using (HttpResponseMessage result = _sut.Get<HttpResponseMessage>("/relativity.rest/api/Relativity.Services.InstanceDetails.IInstanceDetailsModule/InstanceDetailsService/GetRelativityVersionAsync"))
			{
				result.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed, "GET is not supported for this endpoint.");
			}
		}
	}
}
