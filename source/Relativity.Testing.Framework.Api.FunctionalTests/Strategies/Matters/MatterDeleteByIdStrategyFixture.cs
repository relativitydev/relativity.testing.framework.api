using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteByIdStrategy<Matter>))]
	internal class MatterDeleteByIdStrategyFixture : ApiServiceTestFixture<IDeleteByIdStrategy<Matter>>
	{
		private const string _V1_NOT_FOUND_EXCEPTION_MESSAGE = "The object does not exist or you do not have permission to access it.";

		[VersionRange("<12.1")]
		[Test]
		public void DeletePreOsier_Missing()
		{
			int id = 9_999_999;

			HttpRequestException exception = Assert.Throws<HttpRequestException>(() =>
				Sut.Delete(id));

			exception.Message.Should().StartWith($"Matter ArtifactID {id} is invalid.");
		}

		[VersionRange(">=12.1")]
		[Test]
		public void DeleteV1_Missing()
		{
			int id = 9_999_999;

			HttpRequestException exception = Assert.Throws<HttpRequestException>(() =>
				Sut.Delete(id));

			exception.Message.Should().Contain("The object does not exist or you do not have permission to access it.");
		}

		[VersionRange("<12.1")]
		[Test]
		public void DeletePreOsier_Existing_DeletesMatter()
		{
			Matter toDelete = null;

			Arrange(x => x.Create(out toDelete));

			Sut.Delete(toDelete.ArtifactID);

			Facade.Resolve<IMatterGetByIdStrategy>().Get(toDelete.ArtifactID).
				Should().BeNull();
		}

		[VersionRange(">=12.1")]
		[Test]
		public void DeleteV1_Existing_DeletesMatter()
		{
			Matter toDelete = null;

			Arrange(x => x.Create(out toDelete));

			Sut.Delete(toDelete.ArtifactID);

			HttpRequestException exception = Assert.Throws<HttpRequestException>(() =>
				Facade.Resolve<IMatterGetByIdStrategy>().Get(toDelete.ArtifactID));

			exception.Message.Should().Contain(_V1_NOT_FOUND_EXCEPTION_MESSAGE);
		}
	}
}
