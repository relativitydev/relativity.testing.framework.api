using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateWorkspaceEntityStrategy<ProductionPlaceholder>))]
	internal class ProductionPlaceholderUpdateStrategyFixture : ApiServiceTestFixture<IUpdateWorkspaceEntityStrategy<ProductionPlaceholder>>
	{
		private ICreateWorkspaceEntityStrategy<ProductionPlaceholder> _createProductionPlaceholderStrategy;
		private IGetWorkspaceEntityByIdStrategy<ProductionPlaceholder> _getWorkspaceEntityById;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_createProductionPlaceholderStrategy = Facade.Resolve<ICreateWorkspaceEntityStrategy<ProductionPlaceholder>>();
			_getWorkspaceEntityById = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<ProductionPlaceholder>>();
		}

		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Update()
		{
			ProductionPlaceholder existingProductionPlaceholder = null;

			Arrange(() =>
			{
				existingProductionPlaceholder = _createProductionPlaceholderStrategy.Create(DefaultWorkspace.ArtifactID, new ProductionPlaceholder());
			});

			var toUpdate = existingProductionPlaceholder.Copy();
			toUpdate.Name = Randomizer.GetString();
			toUpdate.CustomText = "Updated Text";

			Sut.Update(DefaultWorkspace.ArtifactID, toUpdate);

			var result = _getWorkspaceEntityById.Get(DefaultWorkspace.ArtifactID, toUpdate.ArtifactID);
			result.Should().BeEquivalentTo(toUpdate, x => x.Excluding(y => y.FileData));
		}
	}
}
