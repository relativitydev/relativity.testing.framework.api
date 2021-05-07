using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<Tab>))]
	[NonParallelizable] // We're seeing a lot of tab tests fail, so I'm hoping this will help alleviate it. https://jira.kcura.com/browse/RTF-854
	internal class TabCreateStrategyFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<Tab>>
	{
		public TabCreateStrategyFixture()
		{
		}

		public TabCreateStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			ObjectType objectType = null;

			ArrangeWorkingWorkspace(x => x.Create(out objectType));

			var entity = new Tab
			{
				Name = Randomizer.GetString("AT_"),
				ObjectType = objectType
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o.
				Excluding(x => x.ArtifactID));
		}

		[Test]
		public void Create_WithEntityWhichContainsLinkTypeAsLinkAndFilledInObjectType_ShouldNotFailed()
		{
			ObjectType objectType = null;

			ArrangeWorkingWorkspace(x => x.Create(out objectType));

			var entity = new Tab
			{
				Name = Randomizer.GetString("AT_"),
				LinkType = TabLinkType.Link,
				Link = "http://some.link.us/",
				ObjectType = objectType
			};

			var result = new Tab();

			Assert.DoesNotThrow(() => result = Sut.Create(DefaultWorkspace.ArtifactID, entity));

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o.
				Excluding(x => x.ArtifactID));
		}
	}
}
