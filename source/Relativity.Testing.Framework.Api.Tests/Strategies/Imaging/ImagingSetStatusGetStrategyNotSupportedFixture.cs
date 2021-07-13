using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies.Imaging
{
	[TestFixture]
	[TestOf(typeof(ImagingSetStatusGetStrategyNotSupported))]
	public class ImagingSetStatusGetStrategyNotSupportedFixture
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Get Imaging Set Status does not support version of Relativity lower than 12.1.";
		private const int _WORKSPACE_ID = 10000;
		private const int _IMAGING_SET_ID = 10001;

		private ImagingSetStatusGetStrategyNotSupported _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new ImagingSetStatusGetStrategyNotSupported();
		}

		[Test]
		public void Get_ForNotSupportedVersion_ThrowsArgumentException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
				_sut.Get(_WORKSPACE_ID, _IMAGING_SET_ID));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		[Test]
		public void GetAsync_ForNotSupportedVersion_ThrowsArgumentException()
		{
			var result = Assert.ThrowsAsync<ArgumentException>(async () =>
				await _sut.GetAsync(_WORKSPACE_ID, _IMAGING_SET_ID).ConfigureAwait(false));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
