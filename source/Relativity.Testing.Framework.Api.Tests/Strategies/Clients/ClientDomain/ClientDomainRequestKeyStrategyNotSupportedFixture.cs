using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ClientDomainRequestKeyStrategyNotSupported))]
	public class ClientDomainRequestKeyStrategyNotSupportedFixture
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Request Key for Client Domain does not support version of Relativity lower than 12.1.";
		private const int _CLIENT_ARTIFACT_ID = 10000;

		private ClientDomainRequestKeyStrategyNotSupported _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new ClientDomainRequestKeyStrategyNotSupported();
		}

		[Test]
		public void Run_ForNotSupportedVersion_ThrowsArgumentException()
		{
			ArgumentException result = Assert.Throws<ArgumentException>(() =>
				_sut.Request(_CLIENT_ARTIFACT_ID));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
