﻿using System;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateBatchSetStrategy))]
	internal class BatchSetCreateFixture : ApiServiceTestFixture<ICreateBatchSetStrategy>
	{
		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Create(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		[VersionRange("<12.1")]
		public void Create_WithFilledEntity_PrePrairieSmoke()
		{
			var batchSet = ArrangeBatchSet();

			var result = Sut.Create(DefaultWorkspace.ArtifactID, batchSet);

			TestIfCreatedEntityIsEquivalentToExpectedPrePrairieSmoke(batchSet, result);
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Create_WithFilledEntity()
		{
			BatchSet batchSet = PrepareBatchSet();

			var result = Sut.Create(DefaultWorkspace.ArtifactID, batchSet);

			CheckIfCreatedEntityIsEquivalentToExpectedV1(batchSet, result);
		}

		[Test]
		[VersionRange("<12.1")]
		public void Create_FilledEntityAndWithNullAsUserCredentialsOverride_PrePrairieSmoke()
		{
			var batchSet = ArrangeBatchSet();

			var result = Sut.Create(DefaultWorkspace.ArtifactID, batchSet, userCredentials: null);

			result.Should().BeEquivalentTo(batchSet, x => x.Excluding(y => y.ArtifactID)
				.Excluding(y => y.DataSource.Name).Excluding(y => y.BatchUnitField.Name)
				.Excluding(y => y.FamilyField.Name).Excluding(y => y.ReviewedField.Name));
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Create_FilledEntityAndWithNullAsUserCredentialsOverride()
		{
			BatchSet batchSet = PrepareBatchSet();

			var result = Sut.Create(DefaultWorkspace.ArtifactID, batchSet, userCredentials: null);

			CheckIfCreatedEntityIsEquivalentToExpectedV1(batchSet, result);
		}

		private BatchSet PrepareBatchSet()
		{
			var keywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>().Create(DefaultWorkspace.ArtifactID, new KeywordSearch());

			var batchSet = new BatchSet
			{
				Name = Randomizer.GetString(),
				DataSource = new NamedArtifact
				{
					ArtifactID = keywordSearch.ArtifactID,
					Name = keywordSearch.Name
				}
			};
			return batchSet;
		}

		private static void TestIfCreatedEntityIsEquivalentToExpectedPrePrairieSmoke(BatchSet batchSet, BatchSet result)
		{
			result.Should().BeEquivalentTo(
				batchSet,
				x => x.Excluding(y => y.ArtifactID)
					.Excluding(y => y.DataSource.Name)
					.Excluding(y => y.BatchUnitField.Name)
					.Excluding(y => y.FamilyField.Name)
					.Excluding(y => y.ReviewedField.Name));
		}

		private static void CheckIfCreatedEntityIsEquivalentToExpectedV1(BatchSet batchSet, BatchSet result)
		{
			result.Should().BeEquivalentTo(
				batchSet,
				x => x.Excluding(y => y.ArtifactID)
					.Excluding(y => y.DataSource.Name)
					.Excluding(y => y.BatchUnitField)
					.Excluding(y => y.FamilyField)
					.Excluding(y => y.ReviewedField)
					.Excluding(y => y.AutoBatchSettings)
					.Excluding(y => y.BatchProcessResult));
		}

		[Test]
		public void Create_WithFakeUserCredentialsOverride_ShouldThrowUnauthorizedException()
		{
			var batchSet = ArrangeBatchSet();

			var exception = Assert.Throws<HttpRequestException>(() => Sut.Create(DefaultWorkspace.ArtifactID, batchSet, new Models.UserCredentials { Username = "FakeAccount", Password = "FakePassword" }));

			Assert.True(exception.Message.Contains("Unauthorized"));
		}

		private BatchSet ArrangeBatchSet()
		{
			KeywordSearch keywordSearch = null;

			Field batchUnitField = null;
			Field familyField = null;
			Field reviewedField = null;

			Arrange(() =>
			{
				keywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>().Create(DefaultWorkspace.ArtifactID, new KeywordSearch());

				batchUnitField = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<Field>>().Get(DefaultWorkspace.ArtifactID, "Custodian");
				familyField = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<Field>>().Get(DefaultWorkspace.ArtifactID, "Group Identifier");
				reviewedField = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<Field>>().Get(DefaultWorkspace.ArtifactID, "Classification Index");
			});

			var batchSet = new BatchSet
			{
				Name = Randomizer.GetString(),
				BatchSize = 1500,
				BatchPrefix = Randomizer.GetString("BS", 3),
				DataSource = new NamedArtifact { ArtifactID = keywordSearch.ArtifactID },
				BatchUnitField = new NamedArtifact { ArtifactID = batchUnitField.ArtifactID },
				FamilyField = new NamedArtifact { ArtifactID = familyField.ArtifactID },
				ReviewedField = new NamedArtifact { ArtifactID = reviewedField.ArtifactID },
				AutoBatchSettings = new AutoBatchSettings
				{
					AutoCreateRateInMinutes = 10,
					MinimumBatchSize = 10
				}
			};

			return batchSet;
		}
	}
}
