using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteWorkspaceEntityByIdStrategy<Script>))]
	internal class ScriptDeleteFixture : ApiServiceTestFixture<IDeleteWorkspaceEntityByIdStrategy<Script>>
	{
		public ScriptDeleteFixture()
		{
		}

		public ScriptDeleteFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Delete_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.Delete(DefaultWorkspace.ArtifactID, int.MaxValue));
		}

		[Test]
		public void Delete_Existing()
		{
			Script toDelete = null;

			ArrangeWorkingWorkspace(x => x
				.Create(new Script
				{
					Name = "My script Name",
					Description = "About my script",
					Category = "My category",
					ScriptBody = $"<script><action returns=\"table\"><![CDATA[ SELECT TOP(10) * FROM[eddsdbo].[Artifact]]]></action></script>"
				}).Pick(out toDelete));

			Sut.Delete(DefaultWorkspace.ArtifactID, toDelete.ArtifactID);

			Facade.Resolve<IGetWorkspaceEntityByIdStrategy<Script>>().Get(DefaultWorkspace.ArtifactID, toDelete.ArtifactID).
				Should().BeNull();
		}
	}
}
