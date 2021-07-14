﻿using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IApplicationFieldCodeUpdateStrategy))]
	internal class ApplicationFieldCodeUpdateStrategyFixture : ApiServiceTestFixture<IApplicationFieldCodeUpdateStrategy>
	{
		private IApplicationFieldCodeCreateStrategy _applicationFieldCodeCreateStrategy;
		private int _applicationFieldCodeArtifactId;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_applicationFieldCodeCreateStrategy = Facade.Resolve<IApplicationFieldCodeCreateStrategy>();
		}

		protected override void OnTearDownTest()
		{
			base.OnTearDownTest();

			if (_applicationFieldCodeArtifactId != 0)
			{
				Facade.Resolve<IApplicationFieldCodeDeleteStrategy>().Delete(DefaultWorkspace.ArtifactID, _applicationFieldCodeArtifactId);
			}
		}

		[Test]
		public void Update_ExistingApplicationFieldCode_ShouldBeSuccessful()
		{
			var dto = PrepareTestData();

			var applicationFieldCode = _applicationFieldCodeCreateStrategy.Create(DefaultWorkspace.ArtifactID, dto);
			applicationFieldCode.Option = ApplicationFieldCodeOption.DocumentDefault;

			_applicationFieldCodeArtifactId = applicationFieldCode.ArtifactID;

			var updatedApplicationFieldCode = Sut.Update(DefaultWorkspace.ArtifactID, applicationFieldCode);

			updatedApplicationFieldCode.Should().BeEquivalentTo(applicationFieldCode, option => option.Excluding(x => x.Name));
		}

		[Test]
		public async Task UpdateAsync_ExistingApplicationFieldCode_ShouldBeSuccessful()
		{
			var dto = PrepareTestData();

			var applicationFieldCode = await _applicationFieldCodeCreateStrategy.CreateAsync(DefaultWorkspace.ArtifactID, dto).ConfigureAwait(false);
			applicationFieldCode.Option = ApplicationFieldCodeOption.DocumentDefault;

			_applicationFieldCodeArtifactId = applicationFieldCode.ArtifactID;

			var updatedApplicationFieldCode = await Sut.UpdateAsync(DefaultWorkspace.ArtifactID, applicationFieldCode).ConfigureAwait(false);

			updatedApplicationFieldCode.Should().BeEquivalentTo(applicationFieldCode, option => option.Excluding(x => x.Name));
		}

		private ApplicationFieldCode PrepareTestData()
		{
			return new ApplicationFieldCode
			{
				Application = ApplicationType.MicrosoftExcel,
				FieldCode = "Pages",
				Option = ApplicationFieldCodeOption.ShowFieldCode
			};
		}
	}
}
