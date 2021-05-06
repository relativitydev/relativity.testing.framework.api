using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<ProductionPlaceholder>))]
	internal class ProductionPlaceholderCreateStrategyFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<ProductionPlaceholder>>
	{
		public ProductionPlaceholderCreateStrategyFixture()
		{
		}

		public ProductionPlaceholderCreateStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Create(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Create_WithEmptyEntity()
		{
			var result = Sut.Create(DefaultWorkspace.ArtifactID, new ProductionPlaceholder());
			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.CustomText.Should().NotBeNullOrEmpty();
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			const string fileName = "single_image.jpg";

			var base64String = Convert.ToBase64String(File.ReadAllBytes($@"{AppDomain.CurrentDomain.BaseDirectory}\files\{fileName}"));

			var entity = new ProductionPlaceholder
			{
				Name = Randomizer.GetString(),
				PlaceholderType = PlaceholderType.Image,
				FileName = fileName,
				FileData = base64String
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);
			result.Should().BeEquivalentTo(entity, x => x.Excluding(y => y.FileData));
		}
	}
}
