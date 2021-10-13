using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IFolderUpdateStrategy))]
	internal class FolderUpdateStrategyFixture : ApiServiceTestFixture<IFolderUpdateStrategy>
	{
		private IFolderUpdateStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IFolderUpdateStrategy>();
		}

		[Test]
		public void Update_Missing()
		{
			int artifactID = int.MaxValue;
			var folder = new Folder
			{
				ArtifactID = artifactID,
				Name = "New folder name"
			};

			HttpRequestException exception = Assert.Throws<HttpRequestException>(() =>
				_sut.Update(DefaultWorkspace.ArtifactID, folder));

			exception.Message.Should().Contain($"Folder Artifact ID {artifactID} is invalid.");
		}

		[Test]
		public void Update_Existing()
		{
			Folder toUpdate = null;
			Arrange(() =>
			{
				Folder toCreate = new Folder
				{
					Name = Randomizer.GetString("AT_")
				};
				toUpdate = Facade.Resolve<IFolderCreateStrategy>().Create(DefaultWorkspace.ArtifactID, toCreate);
			});

			toUpdate.Name = Randomizer.GetString("AT_Updated_");

			Folder updated = _sut.Update(DefaultWorkspace.ArtifactID, toUpdate);

			updated.Should().BeEquivalentTo(toUpdate, o => o.Excluding(folder => folder.SystemLastModifiedOn));
		}
	}
}
