using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IApplicationFieldCodeDeleteStrategy))]
	internal class ApplicationFieldCodeDeleteStrategyFixture : ApiServiceTestFixture<IApplicationFieldCodeDeleteStrategy>
	{
		private IApplicationFieldCodeCreateStrategy _applicationFieldCodeCreateStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_applicationFieldCodeCreateStrategy = Facade.Resolve<IApplicationFieldCodeCreateStrategy>();
		}

		[Test]
		public void Delete_NotExistingApplicationFieldCode_ShouldThrowNotFoundException()
		{
			var exception = Assert.Throws<HttpRequestException>(() => Sut.Delete(DefaultWorkspace.ArtifactID, int.MaxValue));

			exception.Message.Should().StartWith("StatusCode: 404, ReasonPhrase: 'Not Found'");
		}

		[Test]
		public void DeleteAsync_NotExistingApplicationFieldCode_ShouldThrowNotFoundException()
		{
			var exception = Assert.ThrowsAsync<HttpRequestException>(() => Sut.DeleteAsync(DefaultWorkspace.ArtifactID, int.MaxValue));

			exception.Message.Should().StartWith("StatusCode: 404, ReasonPhrase: 'Not Found'");
		}

		[Test]
		public void Delete_ExistingApplicationFieldCode_ShouldBeSuccessful()
		{
			var dto = PrepareTestData();

			var applicationFieldCode = _applicationFieldCodeCreateStrategy.Create(DefaultWorkspace.ArtifactID, dto);

			Assert.DoesNotThrow(() => Sut.Delete(DefaultWorkspace.ArtifactID, applicationFieldCode.ArtifactID));
		}

		[Test]
		public async Task DeleteAsync_ExistingApplicationFieldCode_ShouldBeSuccessful()
		{
			var dto = PrepareTestData();

			var applicationFieldCode = await _applicationFieldCodeCreateStrategy.CreateAsync(DefaultWorkspace.ArtifactID, dto).ConfigureAwait(false);

			Assert.DoesNotThrowAsync(() => Sut.DeleteAsync(DefaultWorkspace.ArtifactID, applicationFieldCode.ArtifactID));
		}

		private ApplicationFieldCode PrepareTestData()
		{
			return new ApplicationFieldCode
			{
				Application = ApplicationType.MicrosoftExcel,
				FieldCode = "Author",
				Option = ApplicationFieldCodeOption.DocumentDefault
			};
		}
	}
}
