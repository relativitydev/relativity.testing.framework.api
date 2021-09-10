using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<MarkupSet>))]
	internal class MarkupSetGetByIdFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<MarkupSet>>
	{
		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			MarkupSet existingMarkupSet = null;

			Arrange(() =>
			{
				existingMarkupSet = Facade.Resolve<ICreateWorkspaceEntityStrategy<MarkupSet>>()
					.Create(DefaultWorkspace.ArtifactID, new MarkupSet
					{
						Name = Randomizer.GetString(),
						Order = Randomizer.GetInt(int.MaxValue),
						RedactionText = Randomizer.GetString()
					});
			});

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingMarkupSet.ArtifactID);

			result.Should().BeEquivalentTo(existingMarkupSet);
		}
	}
}
