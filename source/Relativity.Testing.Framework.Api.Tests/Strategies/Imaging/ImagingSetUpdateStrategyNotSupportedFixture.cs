using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingSetUpdateStrategyNotSupported))]
	public class ImagingSetUpdateStrategyNotSupportedFixture
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Update Imaging Set does not support version of Relativity lower than 12.1.";
		private const int _WORKSPACE_ID = 10000;
		private const int _IMAGING_SET_ID = 10001;

		private readonly ImagingSetRequest _defaultRequest = new ImagingSetRequest
		{
			DataSourceID = 1,
			ImagingProfileID = 2,
			Name = "Test Imaging Set"
		};

		private ImagingSetUpdateStrategyNotSupported _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new ImagingSetUpdateStrategyNotSupported();
		}

		[Test]
		public void Update_ForNotSupportedVersion_ThrowsArgumentException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
				_sut.Update(_WORKSPACE_ID, _IMAGING_SET_ID, _defaultRequest));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		[Test]
		public void UpdateAsync_ForNotSupportedVersion_ThrowsArgumentException()
		{
			var result = Assert.ThrowsAsync<ArgumentException>(async () =>
				await _sut.UpdateAsync(_WORKSPACE_ID, _IMAGING_SET_ID, _defaultRequest).ConfigureAwait(false));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
