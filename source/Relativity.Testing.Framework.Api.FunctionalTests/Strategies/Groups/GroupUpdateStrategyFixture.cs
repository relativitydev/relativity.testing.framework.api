using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGroupUpdateStrategy))]
	internal class GroupUpdateStrategyFixture : ApiServiceTestFixture<IGroupUpdateStrategy>
	{
		private IGroupGetByIdStrategy _getByIdStrategy;
		private ICreateStrategy<Group> _createStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			_getByIdStrategy = Facade.Resolve<IGroupGetByIdStrategy>();
			_createStrategy = Facade.Resolve<ICreateStrategy<Group>>();
		}

		[Test]
		public void Update()
		{
			Group existingEntity = null;

			Arrange(() =>
			{
				existingEntity = _createStrategy.Create(new Group());
			});

			Group toUpdate = existingEntity.Copy();
			toUpdate.Name = Randomizer.GetString();

			Group result = Sut.Update(toUpdate);

			result.Should().BeEquivalentTo(toUpdate, o => o.Excluding(x => x.Client).Excluding(x => x.LastModifiedOn).Excluding(x => x.LastModifiedBy));
			result.Client.Should().BeEquivalentTo(toUpdate.Client, o => o.Excluding(x => x.Number).Excluding(x => x.Status.ArtifactID));
		}
	}
}
