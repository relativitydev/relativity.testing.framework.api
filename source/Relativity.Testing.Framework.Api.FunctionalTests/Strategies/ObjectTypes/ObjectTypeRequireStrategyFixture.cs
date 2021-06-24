using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ObjectTypeRequireStrategy))]
	internal class ObjectTypeRequireStrategyFixture : ApiServiceTestFixture<IRequireWorkspaceEntityStrategy<ObjectType>>
	{
		private ICreateWorkspaceEntityStrategy<ObjectType> _createWorkspaceEntityStrategy;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_createWorkspaceEntityStrategy = Facade.Resolve<ICreateWorkspaceEntityStrategy<ObjectType>>();
		}

		[Test]
		public void Require_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Require(-1, null));
		}

		[Test]
		public void Require_Existing()
		{
			ObjectType existingObjectType = null;

			Arrange(() =>
			{
				existingObjectType = _createWorkspaceEntityStrategy.Create(-1, new ObjectType());
			});

			var toUpdate = existingObjectType.Copy();
			toUpdate.Name = Randomizer.GetString();
			toUpdate.EnableSnapshotAuditingOnDelete = true;
			toUpdate.PivotEnabled = true;
			toUpdate.SamplingEnabled = true;

			var result = Sut.Require(-1, toUpdate);

			result.Should().BeEquivalentTo(toUpdate);
		}

		[Test]
		public void Require_Missing()
		{
			var objectType = new ObjectType
			{
				Name = Randomizer.GetString("AT_Name_"),
				EnableSnapshotAuditingOnDelete = true,
				PivotEnabled = true,
				SamplingEnabled = true,
			};

			var result = Sut.Require(-1, objectType);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(objectType, o => o.Excluding(x => x.ArtifactID)
				.Excluding(x => x.ArtifactTypeID)
				.Excluding(x => x.IsDynamic)
				.Excluding(x => x.IsSystem)
				.Excluding(x => x.Guids)
				.Excluding(x => x.RelativityApplications)
				.Excluding(x => x.IsViewEnabled)
				.Excluding(x => x.ParentObjectType));
		}
	}
}
