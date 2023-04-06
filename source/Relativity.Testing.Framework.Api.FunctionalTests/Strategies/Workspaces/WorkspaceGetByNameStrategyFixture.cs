using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetByNameStrategy<Workspace>))]
	internal class WorkspaceGetByNameStrategyFixture : ApiServiceTestFixture<IGetByNameStrategy<Workspace>>
	{
		[Test]
		public void Get_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Get(null));
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(Guid.NewGuid().ToString());

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			var result = Sut.Get(DefaultWorkspace.Name);

			result.Should().BeEquivalentTo(DefaultWorkspace);
		}
	}
}
