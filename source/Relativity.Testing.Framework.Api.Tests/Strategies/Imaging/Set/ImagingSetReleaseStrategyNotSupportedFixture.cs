using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingSetReleaseStrategyNotSupported))]
	public class ImagingSetReleaseStrategyNotSupportedFixture
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Release Imaging Set does not support version of Relativity lower than 12.1.";
		private const int _WORKSPACE_ID = 10000;
		private const int _IMAGING_SET_ID = 10001;

		private ImagingSetReleaseStrategyNotSupported _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new ImagingSetReleaseStrategyNotSupported();
		}

		[Test]
		public void Release_ForNotSupportedVersion_ThrowsArgumentException()
		{
			ArgumentException result = Assert.Throws<ArgumentException>(() =>
				_sut.Release(_WORKSPACE_ID, _IMAGING_SET_ID));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		[Test]
		public void ReleaseAsync_ForNotSupportedVersion_ThrowsArgumentException()
		{
			ArgumentException result = Assert.ThrowsAsync<ArgumentException>(async () =>
				await _sut.ReleaseAsync(_WORKSPACE_ID, _IMAGING_SET_ID).ConfigureAwait(false));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
