using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateStrategy<Group>))]
	internal class GroupUpdateStrategyFixture : ApiServiceTestFixture<IUpdateStrategy<Group>>
	{
		private ICreateStrategy<Group> _createStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			_createStrategy = Facade.Resolve<ICreateStrategy<Group>>();
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
			Group existingEntity = null;

			Arrange(() =>
			{
				existingEntity = _createStrategy.Create(new Group());
			});

			var toUpdate = existingEntity.Copy();
			toUpdate.Name = Randomizer.GetString();

			var result = Sut.Update(toUpdate);

			result.Should().BeEquivalentTo(toUpdate, o => o.Excluding(x => x.Client));
			result.Client.Should().BeEquivalentTo(toUpdate.Client, o => o.Excluding(x => x.Number).Excluding(x => x.Status.ArtifactID));
		}
	}
}
