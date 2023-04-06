using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(IFolderDeleteUnusedStrategy))]
	internal class FolderDeleteUnusedStrategyFixture
	{
		private const int _VALID_WORKSPACE_ARTIFACT_ID = 1;
		private const string _DELETE_URL = "Relativity.Services.Folder.IFolderModule/Folder%20Manager/DeleteUnusedFoldersAsync";

		private readonly QueryResult<Artifact> _expectedDeleteResponse = new QueryResult<Artifact>
		{
			TotalCount = 2,
			Success = true,
			Results = new List<QuerySingleResult<Artifact>>
			{
				new QuerySingleResult<Artifact>
				{
					Success = true,
					Artifact = new Artifact(2)
				},
				new QuerySingleResult<Artifact>
				{
					Success = true,
					Artifact = new Artifact(3)
				}
			}
		};

		private Mock<IRestService> _mockRestService;
		private Mock<IWorkspaceIdValidator> _mockWorkspaceIdValidator;
		private IFolderDeleteUnusedStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockRestService.Setup(restService => restService.Post<QueryResult<Artifact>>(_DELETE_URL, It.IsAny<object>(), 2, null))
				.Returns(_expectedDeleteResponse);
			_mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();

			_sut = new FolderDeleteUnusedStrategy(
				_mockRestService.Object,
				_mockWorkspaceIdValidator.Object);
		}

		[Test]
		public void Delete_WithAnyWorkspaceArtifactID_CallsWorkspaceIdValidator()
		{
			_sut.Delete(_VALID_WORKSPACE_ARTIFACT_ID);

			_mockWorkspaceIdValidator.Verify(validator => validator.Validate(_VALID_WORKSPACE_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Delete_WithValidWorkspaceArtifactID_CallsRestService()
		{
			_sut.Delete(_VALID_WORKSPACE_ARTIFACT_ID);

			_mockRestService.Verify(restService => restService.Post<QueryResult<Artifact>>(_DELETE_URL, It.IsAny<object>(), 2, null), Times.Once);
		}

		[Test]
		public void Delete_WithValidWorkspaceArtifactID_ReturnsExpectedResponse()
		{
			QueryResult<Artifact> response = _sut.Delete(_VALID_WORKSPACE_ARTIFACT_ID);

			response.Should().NotBeNull();
			response.Should().BeEquivalentTo(_expectedDeleteResponse);
		}
	}
}
