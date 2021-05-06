using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<ProductionPlaceholder>))]
	internal class ProductionPlaceholderGetByIdStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<ProductionPlaceholder>>
	{
		public ProductionPlaceholderGetByIdStrategyFixture()
		{
		}

		public ProductionPlaceholderGetByIdStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			ProductionPlaceholder existingProductionPlaceholder = null;

			const string fileName = "single_image.jpg";

			var base64String = Convert.ToBase64String(File.ReadAllBytes($@"{AppDomain.CurrentDomain.BaseDirectory}\files\{fileName}"));

			var entity = new ProductionPlaceholder
			{
				Name = Randomizer.GetString(),
				PlaceholderType = PlaceholderType.Image,
				FileName = fileName,
				FileData = base64String
			};

			Arrange(() =>
			{
				existingProductionPlaceholder = Facade.Resolve<ICreateWorkspaceEntityStrategy<ProductionPlaceholder>>()
					.Create(DefaultWorkspace.ArtifactID, entity);
			});

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingProductionPlaceholder.ArtifactID);

			result.Should().BeEquivalentTo(existingProductionPlaceholder, o => o.Excluding(x => x.FileData));
		}
	}
}
