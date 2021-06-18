using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ChoiceRequireStrategy))]
	internal class ChoiceRequireStrategyFixture : ApiServiceTestFixture<IRequireWorkspaceEntityStrategy<Choice>>
	{
		private ICreateWorkspaceEntityStrategy<MultipleChoiceField> _createFieldStrategy;
		private ICreateWorkspaceEntityStrategy<Choice> _createChoiceStrategy;

		public ChoiceRequireStrategyFixture()
		{
		}

		public ChoiceRequireStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_createFieldStrategy = Facade.Resolve<ICreateWorkspaceEntityStrategy<MultipleChoiceField>>();
			_createChoiceStrategy = Facade.Resolve<ICreateWorkspaceEntityStrategy<Choice>>();
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
			Choice existingChoice = null;

			Arrange(() =>
			{
				var existingField = _createFieldStrategy.Create(DefaultWorkspace.ArtifactID, new MultipleChoiceField());
				existingChoice = _createChoiceStrategy.Create(DefaultWorkspace.ArtifactID, new Choice { Field = existingField });
			});

			var toUpdate = existingChoice.Copy();
			toUpdate.Name = Randomizer.GetString();
			toUpdate.Order = 100;

			var result = Sut.Require(DefaultWorkspace.ArtifactID, toUpdate);

			result.Should().BeEquivalentTo(toUpdate);
		}

		[Test]
		public void Require_Missing()
		{
			Choice missingChoice = null;
			MultipleChoiceField existingField = null;
			Arrange(() =>
			{
				existingField = _createFieldStrategy.Create(DefaultWorkspace.ArtifactID, new MultipleChoiceField());
				missingChoice = new Choice { Field = existingField };
			});

			var result = Sut.Require(DefaultWorkspace.ArtifactID, missingChoice);

			result.ArtifactID.Should().BePositive();
			result.Field.Name.Should().Be(existingField.Name);
			result.ObjectType.Name.Should().Be(existingField.ObjectType.Name);
			result.Should().BeEquivalentTo(missingChoice, o => o.Excluding(x => x.ArtifactID).
				Excluding(x => x.Color).
				Excluding(x => x.ObjectType).
				Excluding(x => x.Field).
				Excluding(x => x.Parent));
		}
	}
}
