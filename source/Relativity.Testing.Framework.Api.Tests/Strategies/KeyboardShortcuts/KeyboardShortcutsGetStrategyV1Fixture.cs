using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(KeyboardShortcutsGetStrategyV1))]
	public class KeyboardShortcutsGetStrategyV1Fixture
	{
		private const string _INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE = "Workspace ID should be greater than zero.";

		private KeyboardShortcutsGetStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;

		[OneTimeSetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			var mockArtifactIdValidator = new Mock<IArtifactIdValidator>();
			mockArtifactIdValidator
				.Setup(validator => validator.Validate(It.Is<int>(id => id < 1), "Workspace"))
				.Throws(new ArgumentException(_INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE));
			_sut = new KeyboardShortcutsGetStrategyV1(_mockRestService.Object, mockArtifactIdValidator.Object);
		}

		[Test]
		public void Get_WithAdminContextWorkspaceId_ThrowsException()
		{
			TestIfGetThrowsInvalidWorkspaceIdException(-1);
		}

		[Test]
		public void Get_WithZeroWorkspaceId_ThrowsException()
		{
			TestIfGetThrowsInvalidWorkspaceIdException(0);
		}

		[Test]
		public void Get_WithValidWorkspaceId_DoesNotThrowException()
		{
			Assert.DoesNotThrow(() => _sut.Get(1, null));
		}

		private void TestIfGetThrowsInvalidWorkspaceIdException(int workspaceId)
		{
			var result = Assert.Throws<ArgumentException>(() => _sut.Get(workspaceId, null));

			result.Message.Should().Contain(_INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Get_WithNullIncludeOptions_CallsApiWithoutQueryParameters()
		{
			var workspaceId = 1;

			_sut.Get(workspaceId, null);

			_mockRestService.Verify(
				restService => restService.Get<IEnumerable<KeyboardShortcut>>($"relativity-review/v1/workspaces/{workspaceId}/keyboard-shortcuts", null),
				Times.Once);
		}

		[Test]
		public void Get_WithFilledIncludeOptions_CallsApiWithQueryParameters()
		{
			var workspaceId = 1;
			var includeOptions = new KeyboardShortcutsIncludeOptions
			{
				IncludeSystemShortcuts = false,
				IncludeFieldShortcuts = true,
				IncludeChoiceShortcuts = false
			};
			string expectedApiUrl = GetExpectedApiUrlWithQueryParameters(workspaceId, includeOptions);

			_sut.Get(workspaceId, includeOptions);

			_mockRestService.Verify(
				restService => restService.Get<IEnumerable<KeyboardShortcut>>(expectedApiUrl, null), Times.Once);
		}

		private static string GetExpectedApiUrlWithQueryParameters(int workspaceId, KeyboardShortcutsIncludeOptions includeOptions)
		{
			return $"relativity-review/v1/workspaces/{workspaceId}/keyboard-shortcuts?includeSystemShortcuts={includeOptions.IncludeSystemShortcuts}&includeChoiceShortcuts={includeOptions.IncludeChoiceShortcuts}&includeFieldShortcuts={includeOptions.IncludeFieldShortcuts}";
		}
	}
}
