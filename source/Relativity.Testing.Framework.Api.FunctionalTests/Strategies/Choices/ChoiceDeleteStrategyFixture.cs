using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ObjectTypeDeleteByIdStrategy))]
	internal class ChoiceDeleteStrategyFixture : ApiServiceTestFixture<IDeleteWorkspaceEntityByIdStrategy<Choice>>
	{
		private ICreateWorkspaceEntityStrategy<MultipleChoiceField> _createFieldStrategy;
		private ICreateWorkspaceEntityStrategy<Choice> _createChoiceStrategy;
		private IGetWorkspaceEntityByIdStrategy<Choice> _getWorkspaceEntityById;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_createFieldStrategy = Facade.Resolve<ICreateWorkspaceEntityStrategy<MultipleChoiceField>>();
			_createChoiceStrategy = Facade.Resolve<ICreateWorkspaceEntityStrategy<Choice>>();
			_getWorkspaceEntityById = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<Choice>>();
		}

		[Test]
		public void Delete_Missing()
		{
			Assert.Throws<ObjectNotFoundException>(() =>
				Sut.Delete(-1, int.MaxValue));
		}

		[Test]
		public void Delete_AdminLevel()
		{
			Choice existingChoice = null;

			Arrange(() =>
			{
				var existingField = _createFieldStrategy.Create(-1, new MultipleChoiceField());
				existingChoice = _createChoiceStrategy.Create(-1, new Choice { Field = existingField });
			});

			var toDelete = existingChoice.Copy();
			toDelete.Name = Randomizer.GetString();
			toDelete.Order = 100;

			Sut.Delete(-1, toDelete.ArtifactID);

			var result = _getWorkspaceEntityById.Get(-1, toDelete.ArtifactID);
			result.Should().BeNull();
		}

		[Test]
		public void Delete_WorkspaceLevel()
		{
			Choice existingChoice = null;

			Arrange(() =>
			{
				var existingField = _createFieldStrategy.Create(DefaultWorkspace.ArtifactID, new MultipleChoiceField());
				existingChoice = _createChoiceStrategy.Create(DefaultWorkspace.ArtifactID, new Choice { Field = existingField });
			});

			var toDelete = existingChoice.Copy();
			toDelete.Name = Randomizer.GetString();
			toDelete.Order = 100;

			Sut.Delete(DefaultWorkspace.ArtifactID, toDelete.ArtifactID);

			var result = _getWorkspaceEntityById.Get(DefaultWorkspace.ArtifactID, toDelete.ArtifactID);
			result.Should().BeNull();
		}
	}
}
