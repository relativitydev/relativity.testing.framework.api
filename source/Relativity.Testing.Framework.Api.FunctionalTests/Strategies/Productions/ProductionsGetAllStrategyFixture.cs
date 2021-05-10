using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetAllWorkspaceEntitiesStrategy<Production>))]
	internal class ProductionsGetAllStrategyFixture : ApiServiceTestFixture<IGetAllWorkspaceEntitiesStrategy<Production>>
	{
		public ProductionsGetAllStrategyFixture()
		{
		}

		public ProductionsGetAllStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void GetAll_Missing()
		{
			var result = Sut.GetAll(DefaultWorkspace.ArtifactID);

			result.Should().NotBeNull();
			result.Length.Should().BeGreaterOrEqualTo(0);
		}

		[Test]
		public void GetAll_Existing()
		{
			Arrange(() =>
			{
				Facade.Resolve<ICreateWorkspaceEntityStrategy<Production>>()
					.Create(DefaultWorkspace.ArtifactID, new Production());
			});

			var result = Sut.GetAll(DefaultWorkspace.ArtifactID);

			result.Length.Should().BePositive();

			var entity = result[0];

			entity.ArtifactID.Should().BePositive();
			entity.Name.Should().NotBeNullOrWhiteSpace();
		}
	}
}
