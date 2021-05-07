using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<OcrProfile>))]
	internal class OcrProfileGetByIdStrategyFixrure : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<OcrProfile>>
	{
		public OcrProfileGetByIdStrategyFixrure()
		{
		}

		public OcrProfileGetByIdStrategyFixrure(string relativityInstanceAlias)
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
			OcrProfile existingOcrProfile = null;

			ArrangeWorkingWorkspace(x => x
				.Create(new OcrProfile())
				.Pick(out existingOcrProfile));

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingOcrProfile.ArtifactID);

			result.Should().BeEquivalentTo(existingOcrProfile);
		}
	}
}
