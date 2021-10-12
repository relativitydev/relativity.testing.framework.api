using System;
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
	[TestOf(typeof(IFolderQueryStrategy))]
	internal class FolderQueryStrategyFixture
	{
		private const string _QUERY_URL = "Relativity.Services.Folder.IFolderModule/Folder%20Manager/QueryAsync";
		private const int _VALID_WORKSPACE_ARTIFACT_ID = 1;

		private Mock<IRestService> _mockRestService;
		private Mock<IWorkspaceIdValidator> _mockWorkspaceIdValidator;
		private IFolderQueryStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();
			_sut = new FolderQueryStrategy(_mockRestService.Object, _mockWorkspaceIdValidator.Object);
		}

		[Test]
		public void Query_WithNullQuery_ThrowsArgumentNullException()
		{
			Query query = null;

			Assert.Throws<ArgumentNullException>(() => _sut.Query(_VALID_WORKSPACE_ARTIFACT_ID, query, 1));
		}

		[Test]
		public void Query_WithAnyParams_CallsWorkspaceIdValidator()
		{
			var query = new Query();

			_sut.Query(_VALID_WORKSPACE_ARTIFACT_ID, query, 1);

			_mockWorkspaceIdValidator.Verify(validator => validator.Validate(_VALID_WORKSPACE_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Query_WithValidParameters_CallsRestServiceWithExpectedParameters()
		{
			var query = new Query();

			_sut.Query(_VALID_WORKSPACE_ARTIFACT_ID, query);

			_mockRestService.Verify(restService => restService.Post<QueryResult<NamedArtifact>>(_QUERY_URL, It.Is<FolderQueryRequest>(request => request.WorkspaceArtifactID == _VALID_WORKSPACE_ARTIFACT_ID && request.Query != null), 2, null), Times.Once);
		}

		[Test]
		public void Query_WithValidParameters_ReturnQueryResultFromRestService()
		{
			var expectedResponse = new QueryResult<NamedArtifact>
			{
				TotalCount = 1,
				Success = true,
				Results = new List<QuerySingleResult<NamedArtifact>>
				{
					new QuerySingleResult<NamedArtifact>
					{
						Success = true,
						Artifact = new NamedArtifact
						{
							Name = "Folder Name",
							ArtifactID = 2
						}
					}
				}
			};
			_mockRestService.Setup(restService => restService.Post<QueryResult<NamedArtifact>>(_QUERY_URL, It.Is<FolderQueryRequest>(request => request.WorkspaceArtifactID == _VALID_WORKSPACE_ARTIFACT_ID && request.Query != null), 2, null)).Returns(expectedResponse);

			var query = new Query();

			QueryResult<NamedArtifact> response = _sut.Query(_VALID_WORKSPACE_ARTIFACT_ID, query);

			response.Should().NotBeNull();
			response.Should().BeEquivalentTo(expectedResponse);
		}
	}
}
