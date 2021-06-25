using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ClientRequireStrategy))]
	public class ClientRequireStrategyFixture : ApiTestFixture
	{
		private IRequireStrategy<Client> _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IRequireStrategy<Client>>();
		}

		[Test]
		public void Require_WithNull()
		{
			var result = _sut.Require(null);

			result.Should().NotBeNull();
			result.ArtifactID.Should().BePositive();
		}

		[Test]
		public void Require_WithEmptyEntity()
		{
			var result = _sut.Require(new Client());

			result.Should().NotBeNull();
			result.ArtifactID.Should().BePositive();
		}

		[Test]
		[VersionRange("<12.1")]
		public void Require_WithArtifactIdThatMissing_PrePrairieSmoke()
		{
			Assert.Throws<ObjectNotFoundException>(() =>
				_sut.Require(new Client { ArtifactID = int.MaxValue }));
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Require_WithArtifactIdThatMissing()
		{
			Assert.Throws<HttpRequestException>(() =>
				_sut.Require(new Client { ArtifactID = int.MaxValue }));
		}

		[Test]
		public void Require_WithArtifactIdThatExists()
		{
			Client entity = null;

			Arrange(x => x.Create(out entity));

			var result = _sut.Require(new Client { ArtifactID = entity.ArtifactID });

			result.Status.Name.Should().Be(entity.Status.Name);
			result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.Status));
		}

		[Test]
		public void Require_WithNameThatMissing()
		{
			var entity = new Client
			{
				Name = Randomizer.GetString("AT_")
			};

			var result = _sut.Require(entity.Copy());

			result.ArtifactID.Should().BePositive();
			result.Name.Should().Be(entity.Name);
		}

		[Test]
		public void Require_WithNameThatExists()
		{
			Client entity = null;

			Arrange(x => x.Create(out entity));

			var result = _sut.Require(new Client { Name = entity.Name });

			result.Status.Name.Should().Be(entity.Status.Name);
			result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.Status));
		}
	}
}
