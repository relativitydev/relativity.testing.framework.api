using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IMatterUpdateStrategy))]
	internal class MatterUpdateStrategyFixture : ApiServiceTestFixture<IMatterUpdateStrategy>
	{
		private ICreateStrategy<Matter> _createStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
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

			Matter result = Sut.Update(toUpdate);

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
