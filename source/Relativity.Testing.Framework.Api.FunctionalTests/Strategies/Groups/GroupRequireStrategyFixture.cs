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
			var groupToRequire = new Group
			{
				Name = Randomizer.GetString("AT_Name_"),
				Client = _client
			};

			Group result = Sut.Require(groupToRequire);

			result.ArtifactID.Should().BePositive();
			result.Client.ArtifactID.Should().Be(groupToRequire.Client.ArtifactID);
			result.Should().BeEquivalentTo(
				groupToRequire,
				o => o.Excluding(group => group.Client)
					.Excluding(group => group.ArtifactID)
					.Excluding(group => group.Actions)
					.Excluding(group => group.Meta)
					.Excluding(group => group.Guids)
					.Excluding(group => group.LastModifiedBy)
					.Excluding(group => group.LastModifiedOn)
					.Excluding(group => group.CreatedBy)
					.Excluding(group => group.CreatedOn));
		}
	}
}
