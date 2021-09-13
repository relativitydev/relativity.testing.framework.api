using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingJobRunStrategyNotSupported))]
	public class ImagingJobRunStrategyNotSupportedFixture
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Run Imaging Job does not support version of Relativity lower than 12.1.";
		private const int _WORKSPACE_ID = 10000;
		private const int _IMAGING_SET_ID = 10001;

		private ImagingJobRunStrategyNotSupported _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new ImagingJobRunStrategyNotSupported();
		}

		[Test]
		public void Run_ForNotSupportedVersion_ThrowsArgumentException()
		{
			ArgumentException result = Assert.Throws<ArgumentException>(() =>
				_sut.Run(_WORKSPACE_ID, _IMAGING_SET_ID));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
