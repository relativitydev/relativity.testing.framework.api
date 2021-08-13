using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(MatterUpdateStrategyPreOsier))]
	internal class MatterUpdateStrategyPreOsierFixture : MatterUpdateStrategyBaseFixture<MatterUpdateStrategyPreOsier>
	{
		private readonly string _updateUrl = $"relativity.matters/workspace/-1/matters/{_MATTER_ID}";

		[SetUp]
		public void SetUp()
		{
			DoSetUp();
			Sut = new MatterUpdateStrategyPreOsier(MockRestService.Object, MockMatterStatusGetChoiceIdByNameStrategy.Object, MockMatterGetByNameAndClientIdStrategy.Object);
		}

		[Test]
		public void Update_WithNull_ThrowsArgumentNullException()
			=> TestIfUpdateWithNullThrowsArgumentNullException();

		[Test]
		public void Update_WithValidEntity_ShouldCallRestServiceWithExpectedUrl()
			=> TestIfUpdateWithValidEntityCallsRestServiceWithExpectedUrl(_updateUrl);

		[Test]
		public void Update_WithValidEntityAndRestrictedUpdate_ShouldCallRestServiceWithRequestWithFilledLastModifiedOnField()
			=> TestIfUpdateWithValidEntityAndRestrictedUpdateCallsRestServiceWithRequestWithFilledLastModifiedOnField(_updateUrl);

		[Test]
		public void Update_WithValidEntity_ShouldCallMatterStatusGetChoiceIdByNameStrategy()
			=> TestIfUpdateWithValidEntityCallsMatterStatusGetChoiceIdByNameStrategy();

		[Test]
		public void Update_WithValidEntityWithMatterArtifactIdFilled_ShouldNotCallMatterGetByNameAndClientIdStrategy()
			=> TestIfUpdateWithValidEntityWithMatterArtifactIdFilledDoesNotCallMatterGetByNameAndClientIdStrategy();

		[Test]
		public void Update_WithoutMatterArtifactIdFilled_ShouldCallMatterGetByNameAndClientIdStrategy()
			=> TestIfUpdateWithoutMatterArtifactIdFilledCallsMatterGetByNameAndClientIdStrategy();

		[Test]
		public void Update_WithoutMatterArtifactIdAndNoMatchingMatterByNameAndClientId_ShouldThrowArgumentException()
			=> TestIfUpdateWithoutMatterArtifactIdAndNoMatchingMatterByNameAndClientIdThrowsArgumentException();

		[Test]
		public void Update_WithoutMatterArtifactIdAndNullClient_ShouldThrowArgumentException()
			=> TestIfUpdateWithoutMatterArtifactIdAndNullClientThrowArgumentException();

		[Test]
		public void Update_WithoutMatterArtifactIdAndClientWithInvalidId_ShouldThrowArgumentException()
			=> TestIfUpdateWithoutMatterArtifactIdAndClientWithInvalidIdThrowsArgumentException();

		[Test]
		public void Update_WithoutMatterArtifactIdAndEmptyName_ShouldThrowArgumentException()
			=> TestIfUpdateWithoutMatterArtifactIdAndEmptyNameThrowsArgumentException();

		[Test]
		public void Update_WithoutMatterArtifactIdAndWithWhitespaceName_ShouldThrowArgumentException()
			=> TestIfUpdateWithoutMatterArtifactIdAndWithWhitespaceNameThrowsArgumentException();

		[Test]
		public void Update_WithoutMatterArtifactIdAndWithNullName_ShouldThrowArgumentException()
			=> TestIfUpdateWithoutMatterArtifactIdAndWithNullNameThrowsArgumentException();
	}
}
