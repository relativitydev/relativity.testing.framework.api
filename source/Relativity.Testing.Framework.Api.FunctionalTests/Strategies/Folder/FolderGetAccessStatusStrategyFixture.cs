using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IFolderGetAccessStatusStrategy))]
	internal class FolderGetAccessStatusStrategyFixture : ApiServiceTestFixture<IFolderGetAccessStatusStrategy>
	{
		private IFolderGetAccessStatusStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IFolderGetAccessStatusStrategy>();
		}

		[Test]
		public void Get_Missing_ReturnsStatusWithExistsSetToFalse()
		{
			int id = int.MaxValue;

			FolderAccessStatus status = _sut.Get(DefaultWorkspace.ArtifactID, id);

			status.Should().NotBeNull();
			status.Exists.Should().BeFalse();
		}

		[Test]
		public void Get_Existing_ReturnsStatusWithExistsSetToTrue()
		{
			int id = 0;
			Arrange(() =>
			{
				var toCreate = new Folder
				{
					Name = Randomizer.GetString("AT_")
				};
				id = Facade.Resolve<IFolderCreateStrategy>().Create(DefaultWorkspace.ArtifactID, toCreate).ArtifactID;
			});

			FolderAccessStatus status = _sut.Get(DefaultWorkspace.ArtifactID, id);

			status.Should().NotBeNull();
			status.Exists.Should().BeTrue();
		}
	}
}
