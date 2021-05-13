using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IRequireStrategy<Matter>))]
	internal class MatterRequireStrategyFixture : ApiServiceTestFixture<IRequireStrategy<Matter>>
	{
		private Client _client;

		public MatterRequireStrategyFixture()
		{
		}

		public MatterRequireStrategyFixture(string relativityInstanceAlias)
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
			Matter existingMatter = null;

			Arrange(x => x.Create(new Matter { Client = _client })
				.Pick(out existingMatter));

			var toUpdate = existingMatter.Copy();
			toUpdate.Name = Randomizer.GetString();

			var result = Sut.Require(toUpdate);

			result.Client.ArtifactID.Should().Be(toUpdate.Client.ArtifactID);
			result.Should().BeEquivalentTo(toUpdate, o => o.Excluding(x => x.Client));
		}

		[Test]
		public void Require_Missing()
		{
			var matter = new Matter
			{
				Name = Randomizer.GetString("AT_Name_"),
				Client = _client
			};

			var result = Sut.Require(matter);

			result.ArtifactID.Should().BePositive();
			result.Client.ArtifactID.Should().Be(matter.Client.ArtifactID);
			result.Should().BeEquivalentTo(matter, o => o.Excluding(x => x.ArtifactID).Excluding(x => x.Client));
		}
	}
}
