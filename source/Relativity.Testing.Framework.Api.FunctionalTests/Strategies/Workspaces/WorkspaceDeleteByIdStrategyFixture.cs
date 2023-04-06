using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteByIdStrategy<Workspace>))]
	internal class WorkspaceDeleteByIdStrategyFixture : ApiServiceTestFixture<IDeleteByIdStrategy<Workspace>>
	{
		private IGetByIdStrategy<Workspace> _getById;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_getById = Facade.Resolve<IGetByIdStrategy<Workspace>>();
		}

		[Test]
		public void Delete_Existing()
		{
			Workspace toDelete = null;

			Arrange(x => x.Create(out toDelete));

			Sut.Delete(toDelete.ArtifactID);

			_getById.Get(toDelete.ArtifactID).Should().BeNull();
		}

		[Test]
		public void Delete_Missing()
		{
			int id = 9_999_999;

			var exception = Assert.Throws<ObjectNotFoundException>(() =>
				Sut.Delete(id));

			exception.Message.Should().StartWith($"Failed to find Workspace entity by {id} ID.");
		}
	}
}
