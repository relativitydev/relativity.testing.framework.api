using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IFolderCreateStrategy))]
	internal class FolderCreateStrategyFixture : ApiServiceTestFixture<IFolderCreateStrategy>
	{
		private IFolderCreateStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IFolderCreateStrategy>();
		}

		[Test]
		public void Create_WithParent_ReturnsExpectedFolder()
		{
			Folder parentFolder = null;
			Arrange(() =>
			{
				parentFolder = new Folder
				{
					Name = Randomizer.GetString("AT_")
				};
				parentFolder = _sut.Create(DefaultWorkspace.ArtifactID, parentFolder);
			});
			var toCreate = new Folder
			{
				Name = Randomizer.GetString("AT_"),
				ParentFolder = parentFolder
			};

			Folder result = _sut.Create(DefaultWorkspace.ArtifactID, toCreate);

			result.Should().NotBeNull();
			result.Name.Should().Be(toCreate.Name);
			result.ParentFolder.Should().BeEquivalentTo(toCreate.ParentFolder);
			result.Permissions.Should().NotBeNull();
		}

		[Test]
		public void Create_WithoutParent_ReturnsExpectedFolder()
		{
			var toCreate = new Folder
			{
				Name = Randomizer.GetString("AT_"),
			};

			Folder result = _sut.Create(DefaultWorkspace.ArtifactID, toCreate);

			result.Should().NotBeNull();
			result.Name.Should().Be(toCreate.Name);
			result.ParentFolder.Should().NotBeNull();
			result.ParentFolder.ArtifactID.Should().BeGreaterThan(0);
			result.ParentFolder.Name.Should().NotBeNullOrWhiteSpace();
			result.Permissions.Should().NotBeNull();
		}
	}
}
