using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IFolderQueryStrategy))]
	internal class FolderQueryStrategyFixture : ApiServiceTestFixture<IFolderQueryStrategy>
	{
		private IFolderQueryStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IFolderQueryStrategy>();
		}

		[Test]
		public void Query_Existing_ReturnsQueryResult()
		{
			Folder existingFolder = Facade.Resolve<IFolderGetWorkspaceRootFolderStrategy>().Get(DefaultWorkspace.ArtifactID);

			var query = new Query
			{
				Condition = $"'Name' == '{existingFolder.Name}'"
			};

			QueryResult<NamedArtifact> queryResult = _sut.Query(DefaultWorkspace.ArtifactID, query, 1);

			queryResult.Should().NotBeNull();
			queryResult.Success.Should().BeTrue();
			queryResult.TotalCount.Should().BeGreaterOrEqualTo(1);
			queryResult.Results.Should().NotBeNullOrEmpty();
			queryResult.Results.Count.Should().Be(1);
			QuerySingleResult<NamedArtifact> firstResult = queryResult.Results.FirstOrDefault();
			firstResult.Should().NotBeNull();
			firstResult.Success.Should().BeTrue();
			firstResult.Artifact.Should().NotBeNull();
			firstResult.Artifact.Name.Should().Be(existingFolder.Name);
			firstResult.Artifact.ArtifactID.Should().Be(existingFolder.ArtifactID);
		}

		[Test]
		public void Query_Missing_ReturnsQueryResultWithEmptyResults()
		{
			string folderName = Randomizer.GetString("AT_");

			var query = new Query
			{
				Condition = $"'Name' == '{folderName}'"
			};

			QueryResult<NamedArtifact> queryResult = _sut.Query(DefaultWorkspace.ArtifactID, query, 1);

			queryResult.Should().NotBeNull();
			queryResult.TotalCount.Should().Be(0);
			queryResult.Results.Should().BeNullOrEmpty();
		}
	}
}
