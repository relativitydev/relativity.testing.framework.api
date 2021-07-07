using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IKeyboardShortcutsGetStrategy))]
	internal class KeyboardShortcutsGetStrategyFixture : ApiServiceTestFixture<IKeyboardShortcutsGetStrategy>
	{
		[Test]
		[VersionRange(">=12.1")]
		public async Task GetKeyboardShortcuts_ValidWorkspaceId_ReturnsNotNull()
		{
			var result = await Sut.GetKeyboardShortcutsAsync(DefaultWorkspace.ArtifactID, null).ConfigureAwait(false);

			result.Should().NotBeNull();
		}

		[Test]
		[VersionRange("<12.1")]
		public void GetKeyboardShortcuts_PrePrairieSmoke_ThrowsNotSupportedException()
		{
			var result = Assert.ThrowsAsync<ArgumentException>(async () =>
				await Sut.GetKeyboardShortcutsAsync(DefaultWorkspace.ArtifactID, null).ConfigureAwait(false));

			result.Message.Should().Contain("The method GetKeyboardShortcuts does not support version of Relativity lower than 12.1.");
		}
	}
}
