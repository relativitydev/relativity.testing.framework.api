using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Validators;

namespace Relativity.Testing.Framework.Api.Tests.Validators
{
	[TestFixture]
	[TestOf(typeof(IArtifactIdValidator))]
	public class ArtifactIdValidatorFixture
	{
		private const string _INVALID_ARTIFACT_ID_EXCEPTION_MESSAGE = "Test Entity ID should be greater than zero.";
		private const string _TEST_ENTITY_NAME = "Test Entity";

		private IArtifactIdValidator _sut;

		[OneTimeSetUp]
		public void SetUp()
		{
			_sut = new ArtifactIdValidator();
		}

		[Test]
		public void Validate_WithInvalidNegativeId_ThrowsExpectedException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
							_sut.Validate(-2, _TEST_ENTITY_NAME));

			result.Message.Should().Contain(_INVALID_ARTIFACT_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Validate_WithValidPositiveId_DoesNotThrowException()
		{
			Assert.DoesNotThrow(() => _sut.Validate(1, _TEST_ENTITY_NAME));
		}
	}
}
