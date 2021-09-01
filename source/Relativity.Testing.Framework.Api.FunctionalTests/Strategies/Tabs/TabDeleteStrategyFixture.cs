using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteWorkspaceEntityByIdStrategy<Tab>))]
	[NonParallelizable] // We're seeing a lot of tab tests fail, so I'm hoping this will help alleviate it.
	internal class TabDeleteStrategyFixture : ApiServiceTestFixture<IDeleteWorkspaceEntityByIdStrategy<Tab>>
	{
		[Test]
		public void Delete_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.Delete(-1, int.MaxValue));
		}

		[Test]
		public void Delete_Existing()
		{
			Tab toDelete = null;

			ArrangeWorkingWorkspace(x => x
				.Create(out ObjectType objectType)
				.Create(new Tab { ObjectType = objectType }).Pick(out toDelete));

			Sut.Delete(DefaultWorkspace.ArtifactID, toDelete.ArtifactID);

			Facade.Resolve<IGetWorkspaceEntityByIdStrategy<Tab>>().Get(DefaultWorkspace.ArtifactID, toDelete.ArtifactID).
				Should().BeNull();
		}
	}
}
