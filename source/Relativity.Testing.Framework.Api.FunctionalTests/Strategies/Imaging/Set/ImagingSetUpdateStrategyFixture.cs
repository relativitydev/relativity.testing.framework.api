using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IImagingSetUpdateStrategy))]
	internal class ImagingSetUpdateStrategyFixture : ImagingStrategyAbstractFixture<IImagingSetUpdateStrategy>
	{
		[Test]
		[VersionRange(">=12.1")]
		public async Task UpdateAsync_ValidParameters_ReturnsExpectedImagingSetId()
		{
			var expectedImagingSet = CreateImagingSetWithUpdatedName();
			var updatedImagingSetId = await UpdateImagingSetAsync(expectedImagingSet).ConfigureAwait(false);

			updatedImagingSetId.Should().Be(expectedImagingSet.ArtifactID);
		}

		[Test]
		[VersionRange(">=12.1")]
		public async Task UpdateAsync_ValidParameters_UpdatesImagingSet()
		{
			var expectedImagingSet = CreateImagingSetWithUpdatedName();
			var updatedImagingSetId = await UpdateImagingSetAsync(expectedImagingSet).ConfigureAwait(false);

			var imagingSetAfterUpdate = await Facade.Resolve<IImagingSetGetStrategy>()
				.GetAsync(DefaultWorkspace.ArtifactID, updatedImagingSetId).ConfigureAwait(false);

			imagingSetAfterUpdate.Should().BeEquivalentTo(expectedImagingSet);
		}

		private async Task<int> UpdateImagingSetAsync(ImagingSet imagingSetToUpdate)
		{
			var imagingSetUpdateRequest = CreateImagingSetRequestFromImagingSet(imagingSetToUpdate);

			var updatedImagingSetId = await Sut.UpdateAsync(
				DefaultWorkspace.ArtifactID, imagingSetToUpdate.ArtifactID, imagingSetUpdateRequest)
				.ConfigureAwait(false);
			return updatedImagingSetId;
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Update_ValidParameters_ReturnsExpectedImagingSetId()
		{
			var expectedImagingSet = CreateImagingSetWithUpdatedName();
			var updatedImagingSetId = UpdateImagingSet(expectedImagingSet);

			updatedImagingSetId.Should().Be(expectedImagingSet.ArtifactID);
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Update_ValidParameters_UpdatesImagingSet()
		{
			var expectedImagingSet = CreateImagingSetWithUpdatedName();
			int updatedImagingSetId = UpdateImagingSet(expectedImagingSet);

			var imagingSetAfterUpdate = Facade.Resolve<IImagingSetGetStrategy>()
				.Get(DefaultWorkspace.ArtifactID, updatedImagingSetId);

			imagingSetAfterUpdate.Should().BeEquivalentTo(expectedImagingSet);
		}

		private int UpdateImagingSet(ImagingSet imagingSetToUpdate)
		{
			var imagingSetUpdateRequest = CreateImagingSetRequestFromImagingSet(imagingSetToUpdate);

			var updatedImagingSetId = Sut.Update(DefaultWorkspace.ArtifactID, imagingSetToUpdate.ArtifactID, imagingSetUpdateRequest);
			return updatedImagingSetId;
		}

		private ImagingSet CreateImagingSetWithUpdatedName()
		{
			var expectedImagingSet = CreateImagingSet();
			expectedImagingSet.Name = "Updated Imaging Set Name";
			return expectedImagingSet;
		}
	}
}
