using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateWorkspaceEntityStrategy<MarkupSet>))]
	internal class MarkupSetUpdateStrategyFixture : ApiServiceTestFixture<IUpdateWorkspaceEntityStrategy<MarkupSet>>
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
			MarkupSet existingMarkupSet = null;

			ArrangeWorkspace(DefaultWorkspace, x => x.Create(new MarkupSet
			{
				Name = Randomizer.GetString(),
				Order = Randomizer.GetInt(int.MaxValue),
				RedactionText = Randomizer.GetString()
			}).Pick(out existingMarkupSet));

			var toUpdate = existingMarkupSet.Copy();
			toUpdate.Name = Randomizer.GetString();
			toUpdate.Order = Randomizer.GetInt(500);
			toUpdate.RedactionText = Randomizer.GetString();

			var result = Sut.Update(DefaultWorkspace.ArtifactID, toUpdate);

			result.Should().BeEquivalentTo(toUpdate);
		}
	}
}
