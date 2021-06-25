using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<OcrProfile>))]
	internal class OcrProfileCreateStrategyFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<OcrProfile>>
	{
		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Create(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Create_WithEmptyEntity()
		{
			var result = Sut.Create(DefaultWorkspace.ArtifactID, new OcrProfile());

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.ImageTimeout.Should().Be(60);
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			var entity = new OcrProfile
			{
				Name = "My script Name",
				ImageTimeout = 120
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);

			result.Should().NotBeNull();

			result.ArtifactID.Should().BePositive();
			result.Name.Should().BeEquivalentTo(entity.Name);
			result.ImageTimeout.Should().Be(entity.ImageTimeout);
		}
	}
}
