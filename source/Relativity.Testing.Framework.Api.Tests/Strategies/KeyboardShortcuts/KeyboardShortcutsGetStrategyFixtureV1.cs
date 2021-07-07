using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
	public class KeyboardShortcutsGetStrategyFixtureV1
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
		public void GetKeyboardShortcutsAsync_WithAdminContextWorkspaceId_ThrowsException()
		{
			TestIfGetKeyboardShortcutsAsyncThrowsInvalidWorkspaceIdException(-1);
		}

		[Test]
		public void GetKeyboardShortcutsAsync_WithZeroWorkspaceId_ThrowsException()
		{
			TestIfGetKeyboardShortcutsAsyncThrowsInvalidWorkspaceIdException(0);
		}

		[Test]
		public void GetKeyboardShortcutsAsync_WithValidWorkspaceId_DoesNotThrowException()
		{
			Assert.DoesNotThrowAsync(async () => await _sut.GetKeyboardShortcutsAsync(1, null).ConfigureAwait(false));
		}

		private void TestIfGetKeyboardShortcutsAsyncThrowsInvalidWorkspaceIdException(int workspaceId)
		{
			var result = Assert.ThrowsAsync<ArgumentException>(async () =>
				await _sut.GetKeyboardShortcutsAsync(workspaceId, null).ConfigureAwait(false));

			result.Message.Should().Contain(_INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public async Task GetKeyboardShortcutsAsync_WithNullIncludeOptions_CallsApiWithoutQueryParameters()
		{
			var workspaceId = 1;
			await _sut.GetKeyboardShortcutsAsync(workspaceId, null).ConfigureAwait(false);

			_mockRestService.Verify(
				restService => restService.GetAsync<IEnumerable<KeyboardShortcut>>($"/Relativity.Rest/API/relativity-review/v1/workspaces/{workspaceId}/keyboard-shortcuts", null),
				Times.Once);
		}

		[Test]
		public async Task GetKeyboardShortcutsAsync_WithFilledIncludeOptions_CallsApiWithQueryParameters()
		{
			var workspaceId = 1;
			var includeOptions = new KeyboardShortcutsIncludeOptions
			{
				IncludeSystemShortcuts = false,
				IncludeFieldShortcuts = false
			};
			var expectedApiUrl = $"/Relativity.Rest/API/relativity-review/v1/workspaces/{workspaceId}/keyboard-shortcuts?includeSystemShortcuts={includeOptions.IncludeSystemShortcuts}&includeChoiceShortcuts={includeOptions.IncludeChoiceShortcuts}&includeFieldShortcuts={includeOptions.IncludeFieldShortcuts}";

			await _sut.GetKeyboardShortcutsAsync(workspaceId, includeOptions).ConfigureAwait(false);

			_mockRestService.Verify(
				restService => restService.GetAsync<IEnumerable<KeyboardShortcut>>(expectedApiUrl, null), Times.Once);
		}
	}
}
