using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetAllStrategy<Workspace>))]
	internal class WorkspaceGetAllStrategyFixture : ApiServiceTestFixture<IGetAllStrategy<Workspace>>
	{
		public WorkspaceGetAllStrategyFixture()
		{
		}

		public WorkspaceGetAllStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void GetAll()
		{
			var result = Sut.GetAll();

			result.Length.Should().BePositive();
			var entity = result[0];

			entity.ArtifactID.Should().BePositive();
			entity.Name.Should().NotBeNullOrWhiteSpace();
			entity.Client.Name.Should().NotBeNullOrWhiteSpace();
			entity.Matter.Name.Should().NotBeNullOrWhiteSpace();
			entity.ResourcePool.Name.Should().NotBeNullOrWhiteSpace();
			entity.DefaultFileRepository.Name.Should().NotBeNullOrWhiteSpace();
			entity.DefaultCacheLocation.Name.Should().NotBeNullOrWhiteSpace();
		}
	}
}
