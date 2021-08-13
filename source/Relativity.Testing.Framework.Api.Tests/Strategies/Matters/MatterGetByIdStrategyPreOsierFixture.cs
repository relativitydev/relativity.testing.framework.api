using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(MatterGetByIdStrategyPreOsier))]
	public class MatterGetByIdStrategyPreOsierFixture
	{
		private const string _NOT_SUPPORTED_METHOD_EXCEPTION_MESSAGE = "The method Get with extended metadata does not support version of Relativity lower than 12.1";

		private Mock<IObjectService> _mockObjectService;
		private MatterGetByIdStrategyPreOsier _sut;

		[SetUp]
		public void SetUp()
		{
			_mockObjectService = new Mock<IObjectService>();
			_sut = new MatterGetByIdStrategyPreOsier(_mockObjectService.Object);
		}

		[Test]
		public void GetWithExtendedMetadata_WithAnyId_ThrowsArgumentsException()
		{
			ArgumentException exception = Assert.Throws<ArgumentException>(() => _sut.Get(1, true));
			exception.Message.Should().Contain(_NOT_SUPPORTED_METHOD_EXCEPTION_MESSAGE);
		}
	}
}
