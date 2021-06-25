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
				ParentObjectType = new ObjectType.WrappedObjectType { Value = template },
				Name = Randomizer.GetString(),
				CopyInstancesOnParentCopy = true,
				CopyInstancesOnCaseCreation = true,
				EnableSnapshotAuditingOnDelete = true,
				PersistentListsEnabled = true,
				PivotEnabled = true,
				SamplingEnabled = true
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);

			result.Name.Should().BeEquivalentTo(entity.Name);
			result.CopyInstancesOnParentCopy.Should().Be(entity.CopyInstancesOnParentCopy);
			result.CopyInstancesOnCaseCreation.Should().Be(entity.CopyInstancesOnCaseCreation);
			result.EnableSnapshotAuditingOnDelete.Should().Be(entity.EnableSnapshotAuditingOnDelete);
			result.PersistentListsEnabled.Should().Be(entity.PersistentListsEnabled);
			result.PivotEnabled.Should().Be(entity.PivotEnabled);
			result.SamplingEnabled.Should().Be(entity.SamplingEnabled);
			result.ArtifactID.Should().BePositive();
			result.ArtifactTypeID.Should().BePositive();
		}

		[Test]
		public void Create_WithEmptyEntity_AdminLevel()
		{
			var result = Sut.Create(-1, new ObjectType());

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
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
				ParentObjectType = new ObjectType.WrappedObjectType { Value = template },
				Name = Randomizer.GetString(),
				CopyInstancesOnParentCopy = false,
				CopyInstancesOnCaseCreation = false,
				EnableSnapshotAuditingOnDelete = true,
				PersistentListsEnabled = false,
				PivotEnabled = true,
				SamplingEnabled = true
			};

			var result = Sut.Create(-1, entity);

			result.Name.Should().BeEquivalentTo(entity.Name);
			result.CopyInstancesOnParentCopy.Should().Be(entity.CopyInstancesOnParentCopy);
			result.CopyInstancesOnCaseCreation.Should().Be(entity.CopyInstancesOnCaseCreation);
			result.EnableSnapshotAuditingOnDelete.Should().Be(entity.EnableSnapshotAuditingOnDelete);
			result.PersistentListsEnabled.Should().Be(entity.PersistentListsEnabled);
			result.PivotEnabled.Should().Be(entity.PivotEnabled);
			result.SamplingEnabled.Should().Be(entity.SamplingEnabled);
			result.ArtifactID.Should().BePositive();
			result.ArtifactTypeID.Should().BePositive();
		}
	}
}
