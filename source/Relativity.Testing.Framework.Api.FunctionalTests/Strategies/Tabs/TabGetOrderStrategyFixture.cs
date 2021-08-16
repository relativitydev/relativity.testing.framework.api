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
		public void Get_ForDefault()
		{
			List<Tab> result = Sut.Get(DefaultWorkspace.ArtifactID);

			result.Should().NotBeEmpty();
		}
	}
}
