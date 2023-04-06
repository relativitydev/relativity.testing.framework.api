using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingSetCreateStrategyNotSupported))]
	public class ImagingSetCreateStrategyNotSupportedFixture
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Create Imaging Set does not support version of Relativity lower than 12.1.";
		private const int _WORKSPACE_ID = 10000;

		private readonly ImagingSetRequest _defaultRequest = new ImagingSetRequest
		{
			DataSourceID = 1,
			ImagingProfileID = 2,
			Name = "Test Imaging Set"
		};

		private ImagingSetCreateStrategyNotSupported _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new ImagingSetCreateStrategyNotSupported();
		}

		[Test]
		public void Create_ForNotSupportedVersion_ThrowsArgumentException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
				_sut.Create(_WORKSPACE_ID, _defaultRequest));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
