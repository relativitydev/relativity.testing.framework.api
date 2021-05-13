using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IRequireStrategy<Group>))]
	internal class GroupRequireStrategyFixture : ApiServiceTestFixture<IRequireStrategy<Group>>
	{
		private Client _client;

		public GroupRequireStrategyFixture()
		{
		}

		public GroupRequireStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			Arrange(x => x.Create(out _client));
		}

		[Test]
		public void Require_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Require(null));
		}

		[Test]
		public void Require_Existing()
		{
			Group existingGroup = null;

			Arrange(x => x.Create(new Group { Client = _client })
				.Pick(out existingGroup));

			var toUpdate = existingGroup.Copy();
			toUpdate.Name = Randomizer.GetString();

			var result = Sut.Require(toUpdate);

			result.Client.ArtifactID.Should().Be(toUpdate.Client.ArtifactID);
			result.Should().BeEquivalentTo(toUpdate, o => o.Excluding(x => x.Client));
		}

		[Test]
		public void Require_Missing()
		{
			var group = new Group
			{
				Name = Randomizer.GetString("AT_Name_"),
				Client = _client
			};

			var result = Sut.Require(group);

			result.ArtifactID.Should().BePositive();
			result.Client.ArtifactID.Should().Be(group.Client.ArtifactID);
			result.Should().BeEquivalentTo(group, o => o.Excluding(x => x.ArtifactID).Excluding(x => x.Client));
		}
	}
}
