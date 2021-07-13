using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IImagingSetCreateStrategy))]
	internal class ImagingSetCreateStrategyFixture : ImagingSetStrategyAbstractFixture<IImagingSetCreateStrategy>
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Create Imaging Set does not support version of Relativity lower than 12.1.";
		private readonly ImagingSetRequest _defaultRequest = new ImagingSetRequest
		{
			DataSourceID = 1,
			ImagingProfileID = 2,
			Name = "Test Imaging Set"
		};

		[Test]
		[VersionRange(">=12.1")]
		public async Task CreateAsync_ValidParameters_ReturnsExpectedImagingSet()
		{
			var imagingSetCreateRequest = ArrangeImagingSetRequest();
			var expectedImagingSet = GetExpectedImageSetFromImagingSetRequest(imagingSetCreateRequest);

			var createdImagingSet = await Sut.CreateAsync(DefaultWorkspace.ArtifactID, imagingSetCreateRequest).ConfigureAwait(false);
			TestIfImagingSetIsEquivalentToExpected(createdImagingSet, expectedImagingSet);
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Create_ValidParameters__ReturnsExpectedImagingSet()
		{
			var imagingSetCreateRequest = ArrangeImagingSetRequest();
			var expectedImagingSet = GetExpectedImageSetFromImagingSetRequest(imagingSetCreateRequest);

			var createdImagingSet = Sut.Create(DefaultWorkspace.ArtifactID, imagingSetCreateRequest);

			TestIfImagingSetIsEquivalentToExpected(createdImagingSet, expectedImagingSet);
		}

		private void TestIfImagingSetIsEquivalentToExpected(ImagingSet createdImagingSet, ImagingSet expectedImagingSet)
		{
			createdImagingSet.Should().BeEquivalentTo(
				expectedImagingSet,
				o => o.Excluding(x => x.ArtifactID)
					.Excluding(x => x.Status)
					.Including(x => x.ImagingProfile.ArtifactID));
		}

		[Test]
		[VersionRange("<12.1")]
		public void Create_ForNotSupportedVersion_ThrowsArgumentException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
				Sut.Create(DefaultWorkspace.ArtifactID, _defaultRequest));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		[Test]
		[VersionRange("<12.1")]
		public void CreateAsync_ForNotSupportedVersion_ThrowsArgumentException()
		{
			var result = Assert.ThrowsAsync<ArgumentException>(async () =>
				await Sut.CreateAsync(DefaultWorkspace.ArtifactID, _defaultRequest).ConfigureAwait(false));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
