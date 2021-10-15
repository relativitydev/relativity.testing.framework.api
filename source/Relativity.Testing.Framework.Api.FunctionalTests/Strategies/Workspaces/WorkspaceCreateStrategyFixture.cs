using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateStrategy<Workspace>))]
	internal class WorkspaceCreateStrategyFixture : ApiServiceTestFixture<ICreateStrategy<Workspace>>
	{
		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Create(null));
		}

		[Test]
		public void Create_WithEmptyEntity()
		{
			var result = Sut.Create(new Workspace());

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.Client.Should().NotBeNull();
			result.Matter.Name.Should().NotBeNull();
			result.ResourcePool.Name.Should().NotBeNull();
			result.DefaultFileRepository.Name.Should().NotBeNull();
			result.DefaultCacheLocation.Name.Should().NotBeNull();
			result.SqlFullTextLanguage.Should().NotBeNull();

			if (IsVersionComparable("<12.1"))
			{
				result.DatabaseLocation.Name.Should().NotBeNull();
			}
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			Client client = null;
			Matter matter = null;
			Group workspaceAdminGroup = null;

			Arrange(x => x
				.Create(out client)
				.Create(new Matter { Client = client })
					.Pick(out matter)
				.Create(out workspaceAdminGroup));

			var entity = new Workspace
			{
				Name = Randomizer.GetString("AT_"),
				Client = client,
				Matter = matter,
				WorkspaceAdminGroup = workspaceAdminGroup
			};

			var result = Sut.Create(entity.Copy());

			result.ArtifactID.Should().BePositive();
			result.Name.Should().Be(entity.Name);
			result.Matter.Name.Should().Be(entity.Matter.Name);
			result.Client.Name.Should().Be(entity.Client.Name);
			result.ResourcePool.Name.Should().NotBeNull();
			result.DefaultFileRepository.Name.Should().NotBeNull();
			result.DefaultCacheLocation.Name.Should().NotBeNull();
			result.WorkspaceAdminGroup.Name.Should().Be(entity.WorkspaceAdminGroup.Name);

			if (IsVersionComparable("<12.1"))
			{
				result.DatabaseLocation.Name.Should().NotBeNull();
			}
		}

		[Test]
		public void Create_WithoutSpecifyingMatter()
		{
			Client client = null;
			Matter matter = null;

			Arrange(x => x
				.Create(out client)
				.Create(new Matter { Client = client })
					.Pick(out matter));

			var entity = new Workspace
			{
				Name = Randomizer.GetString("AT_"),
				Client = client,
			};

			var result = Sut.Create(entity.Copy());

			result.ArtifactID.Should().BePositive();
			result.Name.Should().Be(entity.Name);
			result.Matter.Name.Should().Be(matter.Name);
			result.Client.Name.Should().Be(entity.Client.Name);
			result.ResourcePool.Name.Should().NotBeNull();
			result.DefaultFileRepository.Name.Should().NotBeNull();
			result.DefaultCacheLocation.Name.Should().NotBeNull();

			if (IsVersionComparable("<12.1"))
			{
				result.DatabaseLocation.Name.Should().NotBeNull();
			}
		}
	}
}
