using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Validators;

namespace Relativity.Testing.Framework.Api.Tests.Validators
{
	[TestFixture]
	[TestOf(typeof(IWorkspaceIdValidator))]
	public class WorkspaceIdValidatorFixture
	{
		private const string _INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE = "Workspace ID should be greater than zero or equal to -1 for admin context.";

		private IWorkspaceIdValidator _sut;

		[OneTimeSetUp]
		public void SetUp()
		{
			_sut = new WorkspaceIdValidator();
		}

		[Test]
		public void Validate_WithInvalidNegativeWorkspaceId_ThrowsExpectedException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
							_sut.Validate(-2));

			result.Message.Should().Contain(_INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Validate_WithWorkspaceIdEqualToZero_ThrowsExpectedException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
							_sut.Validate(0));

			result.Message.Should().Contain(_INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Validate_WithAdminContextWorkspaceId_DoesNotThrowException()
		{
			Assert.DoesNotThrow(() => _sut.Validate(-1));
		}

		[Test]
		public void Validate_WithValidPositiveWorkspaceId_DoesNotThrowException()
		{
			Assert.DoesNotThrow(() => _sut.Validate(1));
		}
	}
}
