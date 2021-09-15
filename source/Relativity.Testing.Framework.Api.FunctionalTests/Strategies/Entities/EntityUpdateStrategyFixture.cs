using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateWorkspaceEntityStrategy<Entity>))]
	internal class EntityUpdateStrategyFixture : ApiServiceTestFixture<IUpdateWorkspaceEntityStrategy<Entity>>
	{
		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(-1, null));
		}

		[Test]
		public void Update()
		{
			Entity existingEntity = null;

			ArrangeWorkspace(DefaultWorkspace, x => x.Create(new Entity()).Pick(out existingEntity));

			var toUpdate = existingEntity.Copy();
			toUpdate.FirstName = Randomizer.GetString();
			toUpdate.LastName = Randomizer.GetString();
			toUpdate.DocumentNumberingPrefix = Randomizer.GetString();

			var result = Sut.Update(DefaultWorkspace.ArtifactID, toUpdate);

			result.Should().BeEquivalentTo(toUpdate);
		}
	}
}
