using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IFolderDeleteUnusedStrategy))]
	internal class FolderDeleteUnusedStrategyFixture : ApiServiceTestFixture<IFolderDeleteUnusedStrategy>
	{
		private IFolderDeleteUnusedStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IFolderDeleteUnusedStrategy>();
		}

		[Test]
		public void Delete_WithInvalidWorkspaceArtifactID_ThrowsHttpRequestException()
		{
			int invalidWorkspaceArtifactID = int.MaxValue;
			HttpRequestException exception = Assert.Throws<HttpRequestException>(() =>
				_sut.Delete(invalidWorkspaceArtifactID));

			exception.Message.Should().Contain($"Could not retrieve ApplicationID #{invalidWorkspaceArtifactID}.");
		}

		[Test]
		public void Delete_WithValidWorkspaceArtifactID_ReturnsSuccess()
		{
			QueryResult<Artifact> result = _sut.Delete(DefaultWorkspace.ArtifactID);

			result.Should().NotBeNull();
			result.Success.Should().BeTrue();
		}
	}
}
