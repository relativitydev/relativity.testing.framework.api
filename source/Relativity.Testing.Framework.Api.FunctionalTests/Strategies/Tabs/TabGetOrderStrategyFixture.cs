using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ITabGetOrderStrategy))]
	internal class TabGetOrderStrategyFixture : ApiServiceTestFixture<ITabGetOrderStrategy>
	{
		[Test]
		public void Get_ForDefault_ShouldReturnTabsInAscendingOrder()
		{
			List<Tab> result = Sut.Get(DefaultWorkspace.ArtifactID);

			CheckIfTabsAreInAscendingOrder(result);
		}

		[Test]
		public void Get_ForDefault_ShouldReturnValidTabsOrder()
		{
			List<Tab> result = Sut.Get(DefaultWorkspace.ArtifactID);
			CheckIfTabsHaveOrderResponsePropertiesFilled(result);
		}

		[Test]
		public void Get_ForAdminContext_ShouldReturnTabsInAscendingOrder()
		{
			List<Tab> result = Sut.Get(-1);

			CheckIfTabsAreInAscendingOrder(result);
		}

		[Test]
		public void Get_ForAdminContext_ShouldReturnValidTabsOrder()
		{
			List<Tab> result = Sut.Get(-1);

			CheckIfTabsHaveOrderResponsePropertiesFilled(result);
		}

		private static void CheckIfTabsAreInAscendingOrder(List<Tab> result)
		{
			result.Should().NotBeEmpty();
			result.Should().BeInAscendingOrder(tab => tab.Order);
		}

		private static void CheckIfTabsHaveOrderResponsePropertiesFilled(List<Tab> result)
		{
			result.Should().NotBeEmpty();
			result[0].Should().NotBeNull();
			result[0].Name.Should().NotBeNullOrWhiteSpace();
			result[0].ArtifactID.Should().BeGreaterThan(0);
			result[0].Parent.ArtifactID.Should().BeGreaterThan(0);
		}
	}
}
