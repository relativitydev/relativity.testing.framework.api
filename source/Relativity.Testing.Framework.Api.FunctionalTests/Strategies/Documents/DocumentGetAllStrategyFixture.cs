using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetAllWorkspaceEntitiesStrategy<Document>))]
	internal class DocumentGetAllStrategyFixture : ApiServiceTestFixture<IGetAllWorkspaceEntitiesStrategy<Document>>
	{
		[Test]
		public void GetAll()
		{
			var result = Sut.GetAll(DefaultWorkspace.ArtifactID);

			result.Should().NotBeNullOrEmpty();
		}
	}
}
