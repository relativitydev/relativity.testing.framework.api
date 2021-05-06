using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ILibraryApplicationIsInstalledInWorkspaceStrategy))]
	internal class LibraryApplicationIsInstalledInWorkspaceStrategyFixture : ApiServiceTestFixture<ILibraryApplicationIsInstalledInWorkspaceStrategy>
	{
		private IGetAllWorkspaceEntitiesStrategy<RelativityApplication> _getAllWorkspaceEntities;
		private IGetAllStrategy<LibraryApplication> _getAllStrategy;

		public LibraryApplicationIsInstalledInWorkspaceStrategyFixture()
		{
		}

		public LibraryApplicationIsInstalledInWorkspaceStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_getAllWorkspaceEntities = Facade.Resolve<IGetAllWorkspaceEntitiesStrategy<RelativityApplication>>();
			_getAllStrategy = Facade.Resolve<IGetAllStrategy<LibraryApplication>>();
		}

		[Test]
		public void Check_Missing()
		{
			var workspaceApplications = _getAllWorkspaceEntities.GetAll(DefaultWorkspace.ArtifactID);
			var libraryApplications = _getAllStrategy.GetAll();

			int relativityApplicationArtifactId = libraryApplications.First(x => workspaceApplications.All(y => y.Name != x.Name)).ArtifactID;

			var result = Sut.IsInstalledInWorkspace(DefaultWorkspace.ArtifactID, relativityApplicationArtifactId);

			result.Should().BeFalse();
		}

		[Test]
		public void Check_Existing()
		{
			var workspaceApplications = _getAllWorkspaceEntities.GetAll(DefaultWorkspace.ArtifactID);
			var libraryApplications = _getAllStrategy.GetAll();

			int relativityApplicationArtifactId = libraryApplications.First(x => workspaceApplications.Any(y => y.Name == x.Name)).ArtifactID;
			var result = Sut.IsInstalledInWorkspace(DefaultWorkspace.ArtifactID, relativityApplicationArtifactId);

			result.Should().BeTrue();
		}
	}
}
