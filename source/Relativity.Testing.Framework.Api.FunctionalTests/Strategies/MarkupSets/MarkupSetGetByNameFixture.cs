using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByNameStrategy<MarkupSet>))]
	internal class MarkupSetGetByNameFixture : ApiServiceTestFixture<IGetWorkspaceEntityByNameStrategy<MarkupSet>>
	{
		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, Guid.NewGuid().ToString());

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

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingMarkupSet.Name);

			result.Should().BeEquivalentTo(existingMarkupSet);
		}
	}
}
