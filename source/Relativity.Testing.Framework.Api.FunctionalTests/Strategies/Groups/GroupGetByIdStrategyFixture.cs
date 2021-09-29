using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGroupGetByIdStrategy))]
	internal class GroupGetByIdStrategyFixture : ApiServiceTestFixture<IGroupGetByIdStrategy>
	{
		[VersionRange("<12.1")]
		[Test]
		public void Get_MissingPreOsier()
		{
			var result = Sut.Get(int.MaxValue);

			result.Should().BeNull();
		}

		[VersionRange(">=12.1")]
		[Test]
		public void Get_MissingV1()
		{
			HttpRequestException result = Assert.Throws<HttpRequestException>(() => Sut.Get(int.MaxValue));

			result.Message.Should().Contain("The object does not exist or you do not have permission to access it.");
		}

		[VersionRange("<12.1")]
		[Test]
		public void Get_ExistingPreOsier()
		{
			Group expectedEntity = null;

			Arrange(x => x
				.Create<Group>(3)
					.PickMiddle(out expectedEntity));

			Group result = Sut.Get(expectedEntity.ArtifactID);

			result.Should().BeEquivalentTo(
				expectedEntity,
				o => o.Excluding(x => x.Client)
					.Excluding(x => x.Guids)
					.Excluding(x => x.Meta)
					.Excluding(x => x.Actions)
					.Excluding(x => x.LastModifiedBy)
					.Excluding(x => x.LastModifiedOn)
					.Excluding(x => x.CreatedBy)
					.Excluding(x => x.CreatedOn));
			result.Client.ArtifactID.Should().Be(expectedEntity.Client.ArtifactID);
			result.Client.Name.Should().Be(expectedEntity.Client.Name);
		}

		[VersionRange(">=12.1")]
		[Test]
		public void Get_ExistingV1()
		{
			Group expectedEntity = null;

			Arrange(x => x
				.Create<Group>(3)
					.PickMiddle(out expectedEntity));

			Group result = Sut.Get(expectedEntity.ArtifactID, true, true);

			result.Should().BeEquivalentTo(
				expectedEntity,
				o => o.Excluding(x => x.Client)
					.Excluding(x => x.LastModifiedBy)
					.Excluding(x => x.LastModifiedOn)
					.Excluding(x => x.CreatedBy)
					.Excluding(x => x.CreatedOn));
			result.Client.ArtifactID.Should().Be(expectedEntity.Client.ArtifactID);
			result.Client.Name.Should().Be(expectedEntity.Client.Name);
		}
	}
}
