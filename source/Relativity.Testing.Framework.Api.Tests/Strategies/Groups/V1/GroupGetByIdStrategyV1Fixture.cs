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
	[TestOf(typeof(GroupGetByIdStrategyV1))]
	internal class GroupGetByIdStrategyV1Fixture
	{
		private const int _VALID_GROUP_ARTIFACT_ID = 1;
		private readonly string _getWithMetaAndActionsUrl = $"Relativity-Identity/v1/groups/{_VALID_GROUP_ARTIFACT_ID}?includeMetadata=True&includeActions=True";
		private readonly string _getWithoutMetaAndActionsUrl = $"Relativity-Identity/v1/groups/{_VALID_GROUP_ARTIFACT_ID}?includeMetadata=False&includeActions=False";
		private readonly string _getWithoutMetaWithActionsUrl = $"Relativity-Identity/v1/groups/{_VALID_GROUP_ARTIFACT_ID}?includeMetadata=False&includeActions=True";
		private readonly string _getWithMetaWithoutActionsUrl = $"Relativity-Identity/v1/groups/{_VALID_GROUP_ARTIFACT_ID}?includeMetadata=True&includeActions=False";

		private readonly List<Guid> _groupGuids = new List<Guid>
		{
			Guid.NewGuid(),
			Guid.NewGuid()
		};

		private readonly Meta _groupMetadata = new Meta
		{
			Unsupported = new List<string> { "Test", "Unsupported" },
			ReadOnly = new List<string> { "Test", "Readonly" }
		};

		private readonly List<HttpAction> _groupActions = new List<HttpAction>
		{
			new HttpAction
			{
				Name = "Delete",
				IsAvailable = true
			},
			new HttpAction
			{
				Name = "Update",
				IsAvailable = true
			}
		};

		private Mock<IRestService> _mockRestService;
		private Mock<IArtifactIdValidator> _mockArtifactIdValidator;
		private GroupGetByIdStrategyV1 _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			SetupMockArtifactIdValiator();
			_sut = new GroupGetByIdStrategyV1(_mockRestService.Object, _mockArtifactIdValidator.Object);
		}

		[Test]
		public void Get_WithValidGroupArtifactIDWithMetaAndActions_ShouldCallRestServiceWithExpectedUrl()
		{
			Group expectedGroup = GetExpectedGroup(true, true);
			SetupMockRestService(_getWithMetaAndActionsUrl, expectedGroup);

			_sut.Get(_VALID_GROUP_ARTIFACT_ID, true, true);

			_mockRestService.Verify(restService => restService.Get<GroupResponse>(_getWithMetaAndActionsUrl, null), Times.Once);
		}

		[Test]
		public void Get_WithValidGroupArtifactIDWithMetaAndActions_ShouldReturnExpectedGroup()
		{
			Group expectedGroup = GetExpectedGroup(true, true);
			SetupMockRestService(_getWithMetaAndActionsUrl, expectedGroup);

			Group group = _sut.Get(_VALID_GROUP_ARTIFACT_ID, true, true);

			group.Should().BeEquivalentTo(expectedGroup);
		}

		[Test]
		public void Get_WithValidGroupArtifactIDWithoutMetaAndActions_ShouldCallRestServiceWithExpectedUrl()
		{
			Group expectedGroup = GetExpectedGroup(false, false);
			SetupMockRestService(_getWithoutMetaAndActionsUrl, expectedGroup);

			_sut.Get(_VALID_GROUP_ARTIFACT_ID, false, false);

			_mockRestService.Verify(restService => restService.Get<GroupResponse>(_getWithoutMetaAndActionsUrl, null), Times.Once);
		}

		[Test]
		public void Get_WithValidGroupArtifactIDWithoutMetaAndActions_ShouldReturnExpectedGroup()
		{
			Group expectedGroup = GetExpectedGroup(false, false);
			SetupMockRestService(_getWithoutMetaAndActionsUrl, expectedGroup);

			Group group = _sut.Get(_VALID_GROUP_ARTIFACT_ID, false, false);

			group.Should().BeEquivalentTo(expectedGroup);
		}

		[Test]
		public void Get_WithValidGroupArtifactIDWithMetaWithoutActions_ShouldCallRestServiceWithExpectedUrl()
		{
			Group expectedGroup = GetExpectedGroup(true, false);
			SetupMockRestService(_getWithMetaWithoutActionsUrl, expectedGroup);

			_sut.Get(_VALID_GROUP_ARTIFACT_ID, true, false);

			_mockRestService.Verify(restService => restService.Get<GroupResponse>(_getWithMetaWithoutActionsUrl, null), Times.Once);
		}

		[Test]
		public void Get_WithValidGroupArtifactIDWithMetaWithoutActions_ShouldReturnExpectedGroup()
		{
			Group expectedGroup = GetExpectedGroup(true, false);
			SetupMockRestService(_getWithMetaWithoutActionsUrl, expectedGroup);

			Group group = _sut.Get(_VALID_GROUP_ARTIFACT_ID, true, false);

			group.Should().BeEquivalentTo(expectedGroup);
		}

		[Test]
		public void Get_WithValidGroupArtifactIDWithoutMetaWithActions_ShouldCallRestServiceWithExpectedUrl()
		{
			Group expectedGroup = GetExpectedGroup(false, true);
			SetupMockRestService(_getWithoutMetaWithActionsUrl, expectedGroup);

			_sut.Get(_VALID_GROUP_ARTIFACT_ID, false, true);

			_mockRestService.Verify(restService => restService.Get<GroupResponse>(_getWithoutMetaWithActionsUrl, null), Times.Once);
		}

		[Test]
		public void Get_WithValidGroupArtifactIDWithoutMetaWithActions_ShouldReturnExpectedGroup()
		{
			Group expectedGroup = GetExpectedGroup(false, true);
			SetupMockRestService(_getWithoutMetaWithActionsUrl, expectedGroup);

			Group group = _sut.Get(_VALID_GROUP_ARTIFACT_ID, false, true);

			group.Should().BeEquivalentTo(expectedGroup);
		}

		[Test]
		public void Create_WithAnyParameters_ShouldCallArtifactIdValidator()
		{
			Group expectedGroup = GetExpectedGroup(false, false);
			SetupMockRestService(_getWithoutMetaAndActionsUrl, expectedGroup);

			_sut.Get(_VALID_GROUP_ARTIFACT_ID);

			_mockArtifactIdValidator.Verify(validator => validator.Validate(_VALID_GROUP_ARTIFACT_ID, "Group"), Times.Once);
		}

		private void SetupMockRestService(string getUrl, Group groupToReturn)
		{
			var mockGroupResponse = new GroupResponse
			{
				ArtifactID = groupToReturn.ArtifactID,
				Name = groupToReturn.Name,
				Client = new Securable<NamedArtifactWithGuids>(new NamedArtifactWithGuids
				{
					ArtifactID = groupToReturn.Client.ArtifactID,
					Name = groupToReturn.Client.Name
				}),
				Keywords = groupToReturn.Keywords,
				Notes = groupToReturn.Notes,
				Actions = groupToReturn.Actions,
				Meta = groupToReturn.Meta,
				Type = groupToReturn.Type,
				Guids = groupToReturn.Guids
			};
			_mockRestService
				.Setup(restService => restService.Get<GroupResponse>(getUrl, null))
				.Returns(mockGroupResponse);
		}

		private void SetupMockArtifactIdValiator()
		{
			_mockArtifactIdValidator = new Mock<IArtifactIdValidator>();
			_mockArtifactIdValidator
				.Setup(validator => validator.Validate(It.Is<int>(id => id < 1), "Group"))
				.Throws<ArgumentException>();
		}

		private Group GetExpectedGroup(bool withMeta, bool withActions)
		{
			return new Group
			{
				ArtifactID = _VALID_GROUP_ARTIFACT_ID,
				Name = "Test Group Name",
				Client = new Client
				{
					Name = "Test Client Name",
					ArtifactID = 2,
				},
				Type = GroupType.Personal,
				Keywords = "Test Keywords",
				Notes = "Test Notes",
				Guids = _groupGuids,
				Meta = withMeta ? _groupMetadata : null,
				Actions = withActions ? _groupActions : null
			};
		}
	}
}
