using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ILayoutGetCategoriesStrategy))]
	[VersionRange(">=12.0")]
	internal class LayoutGetCategoriesStrategy : ApiServiceTestFixture<ILayoutGetCategoriesStrategy>
	{
		public LayoutGetCategoriesStrategy()
		{
		}

		public LayoutGetCategoriesStrategy(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void GetCategories()
		{
			Layout existingLayout = null;

			ArrangeWorkingWorkspace(x => x.Create(new Layout()).Pick(out existingLayout));

			List<Category> categories = Sut.GetCategories(DefaultWorkspace.ArtifactID, existingLayout);

			categories.Should().NotBeNullOrEmpty();
			categories.First().GroupId.Should().BePositive();
		}
	}
}
