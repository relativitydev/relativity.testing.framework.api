using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<Production>))]
	internal class ProductionsCreateStrategyFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<Production>>
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
			var result = Sut.Create(DefaultWorkspace.ArtifactID, new Production());
			result.ArtifactID.Should().BePositive();
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			const string fieldName = "Control Number";

			var entity = new Production
			{
				Name = Randomizer.GetString(),
				Numbering = new ProductionNumbering
				{
					NumberingType = NumberingType.PageLevel,
					BatesPrefix = "Prefix",
					BatesSuffix = "Suffix",
					BatesStartNumber = 1,
					NumberOfDigitsForDocumentNumbering = 7,
					AttachmentRelationalField = new NamedArtifact { Name = string.Empty }
				},
				Headers = new ProductionHeaders
				{
					LeftHeader = new HeaderFooter { Type = HeaderFooterType.ProductionBatesNumber },
					CenterHeader = new HeaderFooter { Type = HeaderFooterType.OriginalImageNumber },
					RightHeader = new HeaderFooter { Type = HeaderFooterType.DocumentIdentifierAndPageNumber }
				},
				Footers = new ProductionFooters
				{
					LeftFooter = new HeaderFooter { Type = HeaderFooterType.Field, Field = new NamedArtifact { Name = fieldName } },
					CenterFooter = new HeaderFooter { Type = HeaderFooterType.FreeText, FreeText = Randomizer.GetString() },
					RightFooter = new HeaderFooter { Type = HeaderFooterType.AdvancedFormatting, AdvancedFormatting = fieldName }
				}
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);
			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNull();
			result.Should().BeEquivalentTo(entity, o => o.
				Excluding(x => x.ArtifactID).
				Excluding(x => x.Name).
				Excluding(x => x.ProductionMetadata));
		}

		[Test]
		public void Create_WithDocumentLevel_NumberingType()
		{
			var entity = new Production
			{
				Name = Randomizer.GetString(),
				Numbering = new ProductionNumbering
				{
					NumberingType = NumberingType.DocumentLevel,
					BatesPrefix = "Prefix",
					BatesSuffix = "Suffix",
					BatesStartNumber = 1,
					NumberOfDigitsForDocumentNumbering = 7,
					IncludePageNumbers = true,
					DocumentNumberPageNumberSeparator = "-",
					NumberOfDigitsForPageNumbering = 3,
					StartNumberingOnSecondPage = false,
					AttachmentRelationalField = new NamedArtifact { Name = string.Empty }
				}
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);
			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNull();
			result.Should().BeEquivalentTo(entity, o => o.
				Excluding(x => x.ArtifactID).
				Excluding(x => x.Name).
				Excluding(x => x.ProductionMetadata));
		}

		[Test]
		public void Create_WithOriginalImage_NumberingType()
		{
			var entity = new Production
			{
				Name = Randomizer.GetString(),
				Numbering = new ProductionNumbering
				{
					NumberingType = NumberingType.OriginalImage,
					AttachmentRelationalField = new NamedArtifact { Name = string.Empty }
				}
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);
			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNull();
			result.Should().BeEquivalentTo(entity, o => o.
				Excluding(x => x.ArtifactID).
				Excluding(x => x.Name).
				Excluding(x => x.ProductionMetadata));
		}

		[Test]
		public void Create_WithDocumentField_NumberingType()
		{
			const string fieldName = "Control Number";

			var entity = new Production
			{
				Name = Randomizer.GetString(),
				Numbering = new ProductionNumbering
				{
					NumberingType = NumberingType.DocumentField,
					BatesPrefix = "Prefix",
					BatesSuffix = "Suffix",
					NumberingField = new NamedArtifact { Name = fieldName },
					IncludePageNumbers = true,
					DocumentNumberPageNumberSeparator = "_",
					NumberOfDigitsForPageNumbering = 1,
					StartNumberingOnSecondPage = false,
					AttachmentRelationalField = new NamedArtifact { Name = string.Empty }
				}
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);
			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNull();
			result.Should().BeEquivalentTo(entity, o => o.
				Excluding(x => x.ArtifactID).
				Excluding(x => x.Name).
				Excluding(x => x.Numbering.NumberingField.ArtifactID).
				Excluding(x => x.ProductionMetadata));
		}
	}
}
