using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ITabGetAllForNavigationStrategy))]
	[VersionRange(">=12.1")]
	internal class TabGetAllForNavigationStrategyV1Fixture : ApiServiceTestFixture<ITabGetAllForNavigationStrategy>
	{
		[Test]
		public void Get_ForDefault()
		{
			var result = new List<Tab>();

			Assert.DoesNotThrow(() => result = Sut.Get(DefaultWorkspace.ArtifactID));

			result.Should().NotBeEmpty();
		}

		[Test]
		public void Get_ForAdminContext()
		{
			var result = new List<Tab>();

			Assert.DoesNotThrow(() => result = Sut.Get(-1));

			result.Should().NotBeEmpty();
		}
	}
}
