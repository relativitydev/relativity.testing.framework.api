using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ObjectTypeRequireStrategy))]
	internal class MarkupSetRequireStrategyFixture : ApiServiceTestFixture<IRequireWorkspaceEntityStrategy<MarkupSet>>
	{
		private ICreateWorkspaceEntityStrategy<MarkupSet> _createWorkspaceEntityStrategy;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_createWorkspaceEntityStrategy = Facade.Resolve<ICreateWorkspaceEntityStrategy<MarkupSet>>();
		}

		[Test]
		public void Require_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Require(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Require_Existing()
		{
			MarkupSet existingMarkupSet = null;

			Arrange(() =>
			{
				existingMarkupSet = _createWorkspaceEntityStrategy.Create(DefaultWorkspace.ArtifactID, new MarkupSet
				{
					Name = Randomizer.GetString(),
					Order = Randomizer.GetInt(int.MaxValue),
					RedactionText = Randomizer.GetString()
				});
			});

			var toUpdate = existingMarkupSet.Copy();
			toUpdate.Name = Randomizer.GetString();
			toUpdate.Order = Randomizer.GetInt(100);
			toUpdate.RedactionText = Randomizer.GetString();

			var result = Sut.Require(DefaultWorkspace.ArtifactID, toUpdate);

			result.Should().BeEquivalentTo(toUpdate);
		}

		[Test]
		public void Require_Missing()
		{
			var markupSet = new MarkupSet
			{
				Name = Randomizer.GetString(),
				Order = Randomizer.GetInt(50),
				RedactionText = Randomizer.GetString()
			};

			var result = Sut.Require(DefaultWorkspace.ArtifactID, markupSet);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(markupSet, o => o.
				Excluding(x => x.ArtifactID));
		}
	}
}
