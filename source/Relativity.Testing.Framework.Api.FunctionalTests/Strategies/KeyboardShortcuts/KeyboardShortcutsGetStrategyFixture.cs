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
		public async Task GetAsync_ValidWorkspaceId_ReturnsNotNull()
		{
			var result = await Sut.GetAsync(DefaultWorkspace.ArtifactID, null).ConfigureAwait(false);

			result.Should().NotBeNull();
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Get_ValidWorkspaceId_ReturnsNotNull()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, null);

			result.Should().NotBeNull();
		}

		[Test]
		[VersionRange("<12.1")]
		public void Get_PrePrairieSmoke_ThrowsNotSupportedException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
				Sut.GetAsync(DefaultWorkspace.ArtifactID, null));

			result.Message.Should().Contain("The method GetAsync fro KeyboardShortcuts does not support version of Relativity lower than 12.1.");
		}

		[Test]
		[VersionRange("<12.1")]
		public void GetAsync_PrePrairieSmoke_ThrowsNotSupportedException()
		{
			var result = Assert.ThrowsAsync<ArgumentException>(async () =>
				await Sut.GetAsync(DefaultWorkspace.ArtifactID, null).ConfigureAwait(false));

			result.Message.Should().Contain("The method GetAsync fro KeyboardShortcuts does not support version of Relativity lower than 12.1.");
		}
	}
}
