using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Services
{
	[TestFixture]
	[TestOf(typeof(IClientService))]
	public class ClientServiceFixture
	{
		private const string _INVALID_ID_EXCEPTION_MESSAGE = "Client Artifact ID must be greater than zero.";
		private const string _INVALID_CLIENT_NAME_EXCEPTION_MESSAGE = "Client name must not be empty nor whitespace.";
		private IClientService _sut;

		[OneTimeSetUp]
		public void SetUp()
		{
			var mockCreateStrategy = new Mock<ICreateStrategy<Client>>();
			var mockRequireStrategy = new Mock<IRequireStrategy<Client>>();
			var mockDeleteStrategy = new Mock<IDeleteByIdStrategy<Client>>();
			var mockGetByIdStrategy = new Mock<IGetByIdStrategy<Client>>();
			var mockGetByNameStrategy = new Mock<IGetByNameStrategy<Client>>();
			var mockUpdateStrategy = new Mock<IUpdateStrategy<Client>>();
			var mockClientDomainRequestKeyStrategy = new Mock<IClientDomainRequestKeyStrategy>();

			_sut = new ClientService(
				mockCreateStrategy.Object,
				mockRequireStrategy.Object,
				mockDeleteStrategy.Object,
				mockGetByIdStrategy.Object,
				mockGetByNameStrategy.Object,
				mockUpdateStrategy.Object,
				mockClientDomainRequestKeyStrategy.Object);
		}

		[Test]
		public void Delete_WithNegativeId_ThrowsArgumentException()
		{
			var clientId = -2;

			var exception = Assert.Throws<ArgumentException>(() =>
				_sut.Delete(clientId));
			exception.Message.Should().Contain(_INVALID_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Get_WithNegativeId_ThrowsArgumentException()
		{
			var clientId = -2;

			var exception = Assert.Throws<ArgumentException>(() =>
				_sut.Get(clientId));
			exception.Message.Should().Contain(_INVALID_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Get_WithNullName_ThrowsArgumentException()
		{
			var exception = Assert.Throws<ArgumentException>(() =>
				_sut.Get(null));
			exception.Message.Should().Contain(_INVALID_CLIENT_NAME_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Get_WithEmptyName_ThrowsArgumentException()
		{
			var exception = Assert.Throws<ArgumentException>(() =>
				_sut.Get(string.Empty));
			exception.Message.Should().Contain(_INVALID_CLIENT_NAME_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Get_WithWhitespaceName_ThrowsArgumentException()
		{
			var exception = Assert.Throws<ArgumentException>(() =>
				_sut.Get("	"));
			exception.Message.Should().Contain(_INVALID_CLIENT_NAME_EXCEPTION_MESSAGE);
		}
	}
}
