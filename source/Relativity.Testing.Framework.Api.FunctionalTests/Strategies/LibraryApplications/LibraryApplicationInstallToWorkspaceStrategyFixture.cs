using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(LibraryApplicationInstallToWorkspaceStrategy))]
	internal class LibraryApplicationInstallToWorkspaceStrategyFixture : ApiServiceTestFixture<ILibraryApplicationInstallToWorkspaceStrategy>
	{
		private IGetByNameStrategy<LibraryApplication> _getByNameStrategy;
		private ILibraryApplicationIsInstalledInWorkspaceStrategy _relativityApplicationIsInstalledInWorkspaceStrategy;

		public LibraryApplicationInstallToWorkspaceStrategyFixture()
		{
		}

		public LibraryApplicationInstallToWorkspaceStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();

			_getByNameStrategy = Facade.Resolve<IGetByNameStrategy<LibraryApplication>>();
			_relativityApplicationIsInstalledInWorkspaceStrategy = Facade.Resolve<ILibraryApplicationIsInstalledInWorkspaceStrategy>();
		}

		[Test]
		public void Install_IntoMissingWorkspace()
		{
			int relativityApplicationArtifactId = _getByNameStrategy.Get("Imaging").ArtifactID;

			Assert.Throws<HttpRequestException>(() =>
				Sut.InstallToWorkspace(int.MaxValue, relativityApplicationArtifactId));
		}

		[Test]
		public void Install_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.InstallToWorkspace(DefaultWorkspace.ArtifactID, int.MaxValue));
		}

		[Test]
		public void Install_Existing()
		{
			int relativityApplicationArtifactId = _getByNameStrategy.Get("Imaging").ArtifactID;

			Sut.InstallToWorkspace(DefaultWorkspace.ArtifactID, relativityApplicationArtifactId);

			_relativityApplicationIsInstalledInWorkspaceStrategy.IsInstalledInWorkspace(DefaultWorkspace.ArtifactID, relativityApplicationArtifactId)
				.Should().BeTrue();
		}
	}
}
