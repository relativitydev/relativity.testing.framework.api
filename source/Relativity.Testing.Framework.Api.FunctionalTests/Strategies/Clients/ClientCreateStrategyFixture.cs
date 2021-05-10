using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ClientCreateStrategy))]
	public class ClientCreateStrategyFixture : ApiTestFixture
	{
		private ICreateStrategy<Client> _sut;

		public ClientCreateStrategyFixture()
		{
		}

		public ClientCreateStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<ICreateStrategy<Client>>();
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
			var entity = new Client();

			var result = _sut.Create(entity);

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.Number.Should().NotBeNullOrEmpty();
			result.Status.ArtifactID.Should().BePositive();
			result.Status.Name.Should().Be(ClientStatus.Active.ToString());
			result.Keywords.Should().BeEmpty();
			result.Notes.Should().BeEmpty();
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			var entity = new Client
			{
				Name = Randomizer.GetString("AT_"),
				Number = Randomizer.GetString(),
				Status = new NamedArtifact { Name = ClientStatus.Inactive.ToString() },
				Keywords = Randomizer.GetString(),
				Notes = Randomizer.GetString()
			};

			var result = _sut.Create(entity.Copy());

			result.ArtifactID.Should().BePositive();
			result.Status.ArtifactID.Should().BePositive();
			result.Status.Name.Should().Be(entity.Status.Name);
			result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.ArtifactID).Excluding(x => x.Status));
		}
	}
}
