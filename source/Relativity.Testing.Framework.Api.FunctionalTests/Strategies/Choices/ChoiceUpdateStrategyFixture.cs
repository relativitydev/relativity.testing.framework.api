using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateWorkspaceEntityStrategy<Choice>))]
	internal class ChoiceUpdateStrategyFixture : ApiServiceTestFixture<IUpdateWorkspaceEntityStrategy<Choice>>
	{
		private ICreateWorkspaceEntityStrategy<MultipleChoiceField> _createFieldStrategy;
		private ICreateWorkspaceEntityStrategy<Choice> _createChoiceStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_createFieldStrategy = Facade.Resolve<ICreateWorkspaceEntityStrategy<MultipleChoiceField>>();
			_createChoiceStrategy = Facade.Resolve<ICreateWorkspaceEntityStrategy<Choice>>();
		}

		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Update_AdminLevel()
		{
			Choice existingChoice = null;

			Arrange(() =>
			{
				var existingField = _createFieldStrategy.Create(DefaultWorkspace.ArtifactID, new MultipleChoiceField());
				existingChoice = _createChoiceStrategy.Create(DefaultWorkspace.ArtifactID, new Choice { Field = existingField });
			});

			var toUpdate = existingChoice.Copy();
			toUpdate.Name = Randomizer.GetString();
			toUpdate.Order = 100;

			var result = Sut.Update(DefaultWorkspace.ArtifactID, toUpdate);

			result.Should().BeEquivalentTo(toUpdate);
		}

		[Test]
		public void Update_WorkspaceLevel()
		{
			Choice existingChoice = null;

			Arrange(() =>
			{
				var existingField = _createFieldStrategy.Create(DefaultWorkspace.ArtifactID, new MultipleChoiceField());
				existingChoice = _createChoiceStrategy.Create(DefaultWorkspace.ArtifactID, new Choice { Field = existingField });
			});

			var toUpdate = existingChoice.Copy();
			toUpdate.Name = Randomizer.GetString();
			toUpdate.Order = 100;
			toUpdate.Color = ChoiceColor.Orange;

			var result = Sut.Update(DefaultWorkspace.ArtifactID, toUpdate);

			result.Should().BeEquivalentTo(toUpdate);
		}
	}
}
