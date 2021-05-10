using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[Ignore("Refactor when item support will be implemented. By item, we mean ObjectType or Field or FieldCategory.")]
	[TestOf(typeof(ItemGroupSelectorGetStrategy))]
	internal class ItemGroupSelectorGetStrategyFixture : ApiServiceTestFixture<IItemGroupSelectorGetStrategy>
	{
		public ItemGroupSelectorGetStrategyFixture()
		{
		}

		public ItemGroupSelectorGetStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Get_MissingWorkspace()
		{
			int workspaceId = 9_999_999;

			var exception = Assert.Throws<HttpRequestException>(() =>
				Sut.Get(workspaceId, 1035231));

			exception.Message.Should().StartWith($"Workspace {workspaceId} is invalid.");
		}

		[Test]
		public void Get_Existing()
		{
			List<string> defaultDisabledGroupNames = new List<string>
			{
				"Domain Users",
				"First Level Group",
				"Second Level Group",
				"Third Level Group",
				"Level 1",
				"Level 2",
				"Level 3"
			};

			var result = Sut.Get(DefaultWorkspace.ArtifactID, 1036758);

			result.Should().NotBeNull();
			result.DisabledGroups.Select(x => x.Name).Should().Contain(defaultDisabledGroupNames);
			result.DisabledGroups[0].Name.Should().NotBeNullOrEmpty();
			result.DisabledGroups[0].ArtifactID.Should().BePositive();
			result.LastModified.Should().NotBe(DateTime.MinValue);
		}
	}
}
