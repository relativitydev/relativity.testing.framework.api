using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(GroupGetAllByNamesStrategyFixture))]
	public class GroupGetAllByNamesStrategyFixture : ApiTestFixture
	{
		private IGetAllByNamesStrategy<Group> _sut;

		public GroupGetAllByNamesStrategyFixture()
		{
		}

		public GroupGetAllByNamesStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IGetAllByNamesStrategy<Group>>();
		}

		[Test]
		public void Get_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				_sut.GetAll(null));
		}

		[Test]
		public void Get_WithEmptyList()
		{
			Assert.Throws<ArgumentException>(() =>
				_sut.GetAll(new List<string>()));
		}

		[Test]
		public void Get_Missing()
		{
			var result = _sut.GetAll(new List<string> { Guid.NewGuid().ToString() });

			result.Should().HaveCount(0);
		}

		[Test]
		public void Get_ExistingOne()
		{
			Group expectedEntity = null;

			Arrange(x => x
				.Create<Group>(3)
					.PickMiddle(out expectedEntity));

			var groups = _sut.GetAll(new List<string> { expectedEntity.Name }).ToArray();

			groups.Should().HaveCount(1);

			groups[0].Should().BeEquivalentTo(
				expectedEntity,
				o => o.Excluding(x => x.Client).Including(x => x.Client.ArtifactID).Including(x => x.Client.Name));
		}

		[Test]
		public void Get_ExistingList()
		{
			Group[] existingEntities = null;

			Arrange(x => x.Create(3, out existingEntities));

			var groups = _sut.GetAll(existingEntities.Select(x => x.Name)).ToArray();

			groups.Should().BeEquivalentTo(
				existingEntities,
				o => o.Excluding(x => x.Client).Including(x => x.Client.ArtifactID).Including(x => x.Client.Name));
		}
	}
}
