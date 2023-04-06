using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingSetDeleteStrategyNotSupported))]
	public class ImagingSetDeleteStrategyNotSupportedFixture
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Delete Imaging Set does not support version of Relativity lower than 12.1.";
		private const int _WORKSPACE_ID = 10000;
		private const int _IMAGING_SET_ID = 10001;

		private ImagingSetDeleteStrategyNotSupported _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new ImagingSetDeleteStrategyNotSupported();
		}

		[Test]
		public void Delete_ForNotSupportedVersion_ThrowsArgumentException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
				_sut.Delete(_WORKSPACE_ID, _IMAGING_SET_ID));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
