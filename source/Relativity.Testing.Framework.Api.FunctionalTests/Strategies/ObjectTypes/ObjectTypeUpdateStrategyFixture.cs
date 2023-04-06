using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByNameStrategy<ObjectType>))]
	internal class ObjectTypeUpdateStrategyFixture : ApiServiceTestFixture<IUpdateWorkspaceEntityStrategy<ObjectType>>
	{
		private IGetWorkspaceEntityByIdStrategy<ObjectType> _getWorkspaceEntityByIdStrategy;
		private ICreateWorkspaceEntityStrategy<ObjectType> _createWorkspaceEntityStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			_getWorkspaceEntityByIdStrategy = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<ObjectType>>();
			_createWorkspaceEntityStrategy = Facade.Resolve<ICreateWorkspaceEntityStrategy<ObjectType>>();
		}

		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(-1, null));
		}

		[Test]
		public void Update()
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

			Sut.Update(-1, toUpdate);

			var result = _getWorkspaceEntityByIdStrategy.Get(-1, toUpdate.ArtifactID);
			result.Should().BeEquivalentTo(toUpdate);
		}
	}
}
