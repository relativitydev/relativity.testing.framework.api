using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<Choice>))]
	internal class ChoiceCreateStrategyFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<Choice>>
	{
		private ICreateWorkspaceEntityStrategy<MultipleChoiceField> _createWorkspaceEntityStrategy;

		public ChoiceCreateStrategyFixture()
		{
		}

		public ChoiceCreateStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_createWorkspaceEntityStrategy = Facade.Resolve<ICreateWorkspaceEntityStrategy<MultipleChoiceField>>();
		}

		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Create(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Create_WithEmptyEntity_WorkspaceLevel()
		{
			var exeption = Assert.Throws<ArgumentException>(
				() => Sut.Create(DefaultWorkspace.ArtifactID, new Choice()));

			exeption.Message.Should().StartWith($"{nameof(Choice)} model should have {nameof(Choice.Field)} set.");
		}

		[Test]
		public void Create_WithFilledEntity_WorkspaceLevel()
		{
			Field existingField = null;

			Arrange(() =>
			{
				existingField = _createWorkspaceEntityStrategy.Create(DefaultWorkspace.ArtifactID, new MultipleChoiceField());
			});

			var entity = new Choice
			{
				Name = Randomizer.GetString(),
				Field = existingField
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);

			result.Name.Should().BeEquivalentTo(entity.Name);
			result.ArtifactID.Should().BePositive();
		}

		[Test]
		public void Create_WithEmptyEntity_AdminLevel()
		{
			var exeption = Assert.Throws<ArgumentException>(
				() => Sut.Create(-1, new Choice()));

			exeption.Message.Should().StartWith($"{nameof(Choice)} model should have {nameof(Choice.Field)} set.");
		}

		[Test]
		public void Create_WithFilledEntity_AdminLevel()
		{
			Field existingField = null;

			Arrange(() =>
			{
				existingField = _createWorkspaceEntityStrategy.Create(-1, new MultipleChoiceField());
			});

			var entity = new Choice
			{
				Name = Randomizer.GetString(),
				Field = existingField
			};

			var result = Sut.Create(-1, entity);

			result.Name.Should().BeEquivalentTo(entity.Name);
			result.ArtifactID.Should().BePositive();
		}
	}
}
