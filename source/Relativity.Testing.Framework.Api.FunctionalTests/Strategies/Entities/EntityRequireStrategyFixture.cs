using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IRequireWorkspaceEntityStrategy<Entity>))]
	internal class EntityRequireStrategyFixture : ApiServiceTestFixture<IRequireWorkspaceEntityStrategy<Entity>>
	{
		private ICreateWorkspaceEntityStrategy<Entity> _createWorkspaceEntityStrategy;

		public EntityRequireStrategyFixture()
		{
		}

		public EntityRequireStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_createWorkspaceEntityStrategy = Facade.Resolve<ICreateWorkspaceEntityStrategy<Entity>>();
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
			Entity existingEntity = null;

			Arrange(() =>
			{
				existingEntity = _createWorkspaceEntityStrategy.Create(DefaultWorkspace.ArtifactID, new Entity());
			});

			var toUpdate = existingEntity.Copy();
			toUpdate.FullName = Randomizer.GetString();

			var result = Sut.Require(DefaultWorkspace.ArtifactID, toUpdate);

			result.Should().BeEquivalentTo(toUpdate);
		}

		[Test]
		public void Require_Missing()
		{
			var entity = new Entity
			{
				FirstName = Randomizer.GetString(),
				LastName = Randomizer.GetString(),
				DocumentNumberingPrefix = Randomizer.GetString(),
				Type = new NamedArtifact { Name = "Person" }
			};

			var result = Sut.Require(DefaultWorkspace.ArtifactID, entity);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, y => y.
				Excluding(x => x.ArtifactID).Excluding(o => o.Type.Name));
		}
	}
}
