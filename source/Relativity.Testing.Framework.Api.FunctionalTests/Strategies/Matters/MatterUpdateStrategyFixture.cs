using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateStrategy<Matter>))]
	internal class MatterUpdateStrategyFixture : ApiServiceTestFixture<IUpdateStrategy<Matter>>
	{
		private IGetByIdStrategy<Matter> _getByIdStrategy;
		private ICreateStrategy<Matter> _createStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			_getByIdStrategy = Facade.Resolve<IGetByIdStrategy<Matter>>();
			_createStrategy = Facade.Resolve<ICreateStrategy<Matter>>();
		}

		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(null));
		}

		[Test]
		public void Update()
		{
			Matter existingEntity = null;

			Arrange(() =>
			{
				existingEntity = _createStrategy.Create(new Matter());
			});

			Matter toUpdate = existingEntity.Copy();
			toUpdate.Name = Randomizer.GetString();

			Sut.Update(toUpdate);

			Matter result = _getByIdStrategy.Get(toUpdate.ArtifactID);

			result.Should().BeEquivalentTo(
				toUpdate,
				o => o.Excluding(x => x.Client)
					.Excluding(x => x.Actions)
					.Excluding(x => x.CreatedOn)
					.Excluding(x => x.CreatedBy)
					.Excluding(x => x.LastModifiedOn)
					.Excluding(x => x.LastModifiedBy));
			result.Client.Should().BeEquivalentTo(toUpdate.Client, o => o.Excluding(x => x.Number).Excluding(x => x.Status.ArtifactID));
		}
	}
}
