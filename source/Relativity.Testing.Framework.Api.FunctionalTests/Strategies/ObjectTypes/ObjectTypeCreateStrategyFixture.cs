using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<ObjectType>))]
	internal class ObjectTypeCreateStrategyFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<ObjectType>>
	{
		private IGetWorkspaceEntityByNameStrategy<ObjectType> _getWorkspaceEntityByNameStrategy;

		public ObjectTypeCreateStrategyFixture()
		{
		}

		public ObjectTypeCreateStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_getWorkspaceEntityByNameStrategy = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<ObjectType>>();
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
			var result = Sut.Create(DefaultWorkspace.ArtifactID, new ObjectType());

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.ParentArtifactTypeID.Should().Be(10);
		}

		[Test]
		public void Create_WithFilledEntity_WorkspaceLevel()
		{
			ObjectType template = null;

			Arrange(() =>
			{
				template = _getWorkspaceEntityByNameStrategy.Get(DefaultWorkspace.ArtifactID, "Document");
			});

			var entity = new ObjectType
			{
				ParentObjectType = template,
				Name = Randomizer.GetString(),
				CopyInstanceOnParentCopy = true,
				CopyInstanceOnWorkspaceCreation = true,
				EnableSnapshotAuditingOnDelete = true,
				PersistentLists = true,
				Pivot = true,
				Sampling = true
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);

			result.Name.Should().BeEquivalentTo(entity.Name);
			result.CopyInstanceOnParentCopy.Should().Be(entity.CopyInstanceOnParentCopy);
			result.CopyInstanceOnWorkspaceCreation.Should().Be(entity.CopyInstanceOnWorkspaceCreation);
			result.EnableSnapshotAuditingOnDelete.Should().Be(entity.EnableSnapshotAuditingOnDelete);
			result.PersistentLists.Should().Be(entity.PersistentLists);
			result.Pivot.Should().Be(entity.Pivot);
			result.Sampling.Should().Be(entity.Sampling);
			result.ArtifactID.Should().BePositive();
			result.ArtifactTypeID.Should().BePositive();
			result.ParentArtifactTypeID.Should().Be(template.ArtifactTypeID);
		}

		[Test]
		public void Create_WithEmptyEntity_AdminLevel()
		{
			var result = Sut.Create(-1, new ObjectType());

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.ParentArtifactTypeID.Should().Be(1);
		}

		[Test]
		public void Create_WithFilledEntity_AdminLevel()
		{
			ObjectType template = null;

			Arrange(() =>
			{
				template = _getWorkspaceEntityByNameStrategy.Get(-1, "System");
			});

			var entity = new ObjectType
			{
				ParentObjectType = template,
				Name = Randomizer.GetString(),
				CopyInstanceOnParentCopy = false,
				CopyInstanceOnWorkspaceCreation = false,
				EnableSnapshotAuditingOnDelete = true,
				PersistentLists = false,
				Pivot = true,
				Sampling = true
			};

			var result = Sut.Create(-1, entity);

			result.Name.Should().BeEquivalentTo(entity.Name);
			result.CopyInstanceOnParentCopy.Should().Be(entity.CopyInstanceOnParentCopy);
			result.CopyInstanceOnWorkspaceCreation.Should().Be(entity.CopyInstanceOnWorkspaceCreation);
			result.EnableSnapshotAuditingOnDelete.Should().Be(entity.EnableSnapshotAuditingOnDelete);
			result.PersistentLists.Should().Be(entity.PersistentLists);
			result.Pivot.Should().Be(entity.Pivot);
			result.Sampling.Should().Be(entity.Sampling);
			result.ArtifactID.Should().BePositive();
			result.ArtifactTypeID.Should().BePositive();
			result.ParentArtifactTypeID.Should().Be(template.ArtifactTypeID);
		}
	}
}
