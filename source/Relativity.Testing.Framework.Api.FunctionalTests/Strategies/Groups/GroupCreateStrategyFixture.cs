using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateStrategy<Group>))]
	public class GroupCreateStrategyFixture : ApiTestFixture
	{
		private ICreateStrategy<Group> _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<ICreateStrategy<Group>>();
		}

		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				_sut.Create(null));
		}

		[Test]
		public void Create_WithEmptyEntity()
		{
			var result = _sut.Create(new Group());

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.Client.Should().NotBeNull();
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			Client client = null;

			Arrange(x => x.Create(out client));

			var entity = new Group
			{
				Name = Randomizer.GetString("AT_"),
				Client = client
			};

			var result = _sut.Create(entity.Copy());

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.ArtifactID).Excluding(x => x.Client.Number).Excluding(x => x.Client.Status.ArtifactID));
		}
	}
}
