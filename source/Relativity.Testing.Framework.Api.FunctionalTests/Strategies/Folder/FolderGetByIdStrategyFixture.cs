using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IFolderGetByIdStrategy))]
	internal class FolderGetByIdStrategyFixture : ApiServiceTestFixture<IFolderGetByIdStrategy>
	{
		private IFolderGetByIdStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IFolderGetByIdStrategy>();
		}

		[Test]
		public void Get_Missing_ReturnsNull()
		{
			int id = 9_999_999;

			Folder result = _sut.Get(DefaultWorkspace.ArtifactID, id);

			result.Should().BeNull();
		}
	}
}
