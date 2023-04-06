using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(LibraryApplicationDeleteByIdStrategyNotSupported))]
	public class LibraryApplicationDeleteByIdStrategyNotSupportedFixture
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Delete does not support version of Relativity lower than 12.1.";
		private const int _APPLICATION_ID = 10000;

		private LibraryApplicationDeleteByIdStrategyNotSupported _strategy;

		[SetUp]
		public void SetUp()
		{
			_strategy = new LibraryApplicationDeleteByIdStrategyNotSupported();
		}

		[Test]
		public void Delete_ForNotSupportedVersion_ThrowsArgumentException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
				_strategy.Delete(_APPLICATION_ID));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
