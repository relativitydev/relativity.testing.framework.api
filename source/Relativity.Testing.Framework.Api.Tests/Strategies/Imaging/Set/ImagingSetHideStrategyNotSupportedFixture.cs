using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingSetHideStrategyNotSupported))]
	public class ImagingSetHideStrategyNotSupportedFixture
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Hide Imaging Set does not support version of Relativity lower than 12.1.";
		private const int _WORKSPACE_ID = 10000;
		private const int _IMAGING_SET_ID = 10001;

		private ImagingSetHideStrategyNotSupported _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new ImagingSetHideStrategyNotSupported();
		}

		[Test]
		public void Hide_ForNotSupportedVersion_ThrowsArgumentException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
				_sut.Hide(_WORKSPACE_ID, _IMAGING_SET_ID));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		[Test]
		public void HideAsync_ForNotSupportedVersion_ThrowsArgumentException()
		{
			var result = Assert.ThrowsAsync<ArgumentException>(async () =>
				await _sut.HideAsync(_WORKSPACE_ID, _IMAGING_SET_ID).ConfigureAwait(false));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
