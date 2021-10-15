using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateStrategy<Workspace>))]
	[VersionRange(">=12.1")]
	internal class WorkspaceUpdateStrategyFixture : ApiServiceTestFixture<IUpdateStrategy<Workspace>>
	{
		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(null));
		}

		[Test]
		public void Update()
		{
			Workspace toUpdate = null;

			Arrange(x => x
				.Create<Workspace>(out toUpdate));

			toUpdate.Name = Randomizer.GetString("AT_{0}");
			toUpdate.SqlFullTextLanguage = SqlFullTextLanguage.Chinese;

			Sut.Update(toUpdate);

			var result = Facade.Resolve<IGetByIdStrategy<Workspace>>().Get(toUpdate.ArtifactID);

			result.Should().BeEquivalentTo(toUpdate);
		}
	}
}
