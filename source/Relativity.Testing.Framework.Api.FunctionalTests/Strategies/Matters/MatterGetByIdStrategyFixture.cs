using System;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IMatterGetByIdStrategy))]
	internal class MatterGetByIdStrategyFixture : ApiServiceTestFixture<IMatterGetByIdStrategy>
	{
		private const string _V1_NOT_FOUND_EXCEPTION_MESSAGE = "The object does not exist or you do not have permission to access it.";
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Get with extended metadata does not support version of Relativity lower than 12.1";

		[VersionRange("<12.1")]
		[Test]
		public void GetPreOsier_Missing_ReturnsNull()
		{
			Matter result = Sut.Get(int.MaxValue);

			result.Should().BeNull();
		}

		[VersionRange(">=12.1")]
		[Test]
		public void GetV1_Missing_ThrowsException()
		{
			HttpRequestException exception = Assert.Throws<HttpRequestException>(() => Sut.Get(int.MaxValue));

			exception.Message.Should().Contain(_V1_NOT_FOUND_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Get_Existing_ReturnsExpectedMatter()
		{
			Matter expectedEntity = null;
			expectedEntity = ArrangeMatter(expectedEntity);

			Matter result = Sut.Get(expectedEntity.ArtifactID);

			result.Should().BeEquivalentTo(
				expectedEntity,
				o => o.Excluding(x => x.Client).Including(x => x.Client.ArtifactID).Including(x => x.Client.Name));
		}

		[VersionRange(">=12.1")]
		[Test]
		public void GetWithExtendedMetadataV1_Missing_ThrowsException()
		{
			HttpRequestException exception = Assert.Throws<HttpRequestException>(() => Sut.Get(int.MaxValue, true));

			exception.Message.Should().Contain(_V1_NOT_FOUND_EXCEPTION_MESSAGE);
		}

		[VersionRange(">=12.1")]
		[Test]
		public void GetWithExtendedMetadata_Existing_ReturnsExpectedMatter()
		{
			Matter expectedEntity = null;
			expectedEntity = ArrangeMatter(expectedEntity);

			Matter result = Sut.Get(expectedEntity.ArtifactID, true);

			result.Should().BeEquivalentTo(
				expectedEntity,
				o => o.Excluding(x => x.Client).Including(x => x.Client.ArtifactID).Including(x => x.Client.Name));
		}

		[VersionRange("<12.1")]
		[Test]
		public void GetWithExtendedMetadataPreOsier_ThrowsArgumentException()
		{
			ArgumentException exception = Assert.Throws<ArgumentException>(() => Sut.Get(1, true));

			exception.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		private Matter ArrangeMatter(Matter expectedEntity)
		{
			Arrange(x => x
				.Create(out Client client)
				.Create(3, new Matter { Client = client })
				.PickMiddle(out expectedEntity));
			return expectedEntity;
		}
	}
}
