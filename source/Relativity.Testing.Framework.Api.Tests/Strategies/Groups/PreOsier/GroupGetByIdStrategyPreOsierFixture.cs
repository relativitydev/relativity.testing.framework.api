using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(GroupGetByIdStrategyPreOsier))]
	internal class GroupGetByIdStrategyPreOsierFixture
	{
		private const int _VALID_GROUP_ARTIFACT_ID = 1;
		private const string _UNSUPPORTED_PARAMETERS_EXCEPTION_MESSAGE = "The method Get Group for version of Relativity lower than 12.1 does not support including Metadata nor Actions.";

		private Mock<IObjectService> _mockObjectService;
		private GroupGetByIdStrategyPreOsier _sut;

		[SetUp]
		public void SetUp()
		{
			_mockObjectService = new Mock<IObjectService>();
			_sut = new GroupGetByIdStrategyPreOsier(_mockObjectService.Object);
		}

		[Test]
		public void Get_WithValidGroupArtifactIDWithMetaAndActions_ShouldThrowArgumentException()
		{
			ArgumentException exception = Assert.Throws<ArgumentException>(() =>
				_sut.Get(_VALID_GROUP_ARTIFACT_ID, true, true));

			exception.Message.Should().Contain(_UNSUPPORTED_PARAMETERS_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Get_WithValidGroupArtifactIDWithoutMetaWithActions_ShouldThrowArgumentException()
		{
			ArgumentException exception = Assert.Throws<ArgumentException>(() =>
				_sut.Get(_VALID_GROUP_ARTIFACT_ID, false, true));

			exception.Message.Should().Contain(_UNSUPPORTED_PARAMETERS_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Get_WithValidGroupArtifactIDWithMetaWithoutActions_ShouldThrowArgumentException()
		{
			ArgumentException exception = Assert.Throws<ArgumentException>(() =>
				_sut.Get(_VALID_GROUP_ARTIFACT_ID, true, false));

			exception.Message.Should().Contain(_UNSUPPORTED_PARAMETERS_EXCEPTION_MESSAGE);
		}
	}
}
