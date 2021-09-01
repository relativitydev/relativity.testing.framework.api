using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetAllByClientNameStrategy<Workspace>))]
	internal class WorkspaceGetAllByClientNameStrategyFixture : ApiServiceTestFixture<IGetAllByClientNameStrategy<Workspace>>
	{
		[Test]
		public void GetAllByClientName_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.GetAllByClientName(null));
		}

		[Test]
		public void GetAllByClientName_Missing()
		{
			var result = Sut.GetAllByClientName(Guid.NewGuid().ToString());

			result.Should().BeEmpty();
		}

		// TODO: Rewrite this test.
		[Test]
		public void GetAllByClientName_Existing()
		{
			const string existingClientName = "Relativity Template";
			var result = Sut.GetAllByClientName(existingClientName).
				ToArray()[0];

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrWhiteSpace();
			result.Client.Name.Should().Be(existingClientName);
			result.Status.Should().Be("Active");
			result.Matter.Name.Should().NotBeNullOrWhiteSpace();
			result.ResourcePool.Name.Should().NotBeNullOrWhiteSpace();
			result.DefaultFileRepository.Name.Should().NotBeNullOrWhiteSpace();
			result.DefaultCacheLocation.Name.Should().NotBeNullOrWhiteSpace();
		}
	}
}
