﻿using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(GroupUpdateStrategyV1))]
	internal class GroupUpdateStrategyV1Fixture
	{
		private const string _CLIENT_NAME = "Test Client Name";
		private const int _CLIENT_ARTIFACT_ID = 2;
		private const int _EXISTING_GROUP_ARTIFACT_ID = 1;
		private const string _EXISTING_GROUP_NAME = "Test Group Name";
		private const string _UPDATED_KEYWORDS = "Updated Keywords";
		private const string _UPDATED_NOTES = "Updated Notes";
		private const string _UPDATED_NAME = "Updated Group Name";

		private readonly string _updateUrl = $"Relativity-Identity/v1/groups/{_EXISTING_GROUP_ARTIFACT_ID}";
		private readonly Client _client = new Client
		{
			Name = _CLIENT_NAME,
			ArtifactID = _CLIENT_ARTIFACT_ID
		};

		private Mock<IRestService> _mockRestService;
		private Mock<IGetByNameStrategy<Group>> _mockGetByNameStrategy;
		private GroupUpdateStrategyV1 _sut;

		[SetUp]
		public void SetUp()
		{
			_mockGetByNameStrategy = new Mock<IGetByNameStrategy<Group>>();
			_mockRestService = new Mock<IRestService>();

			_sut = new GroupUpdateStrategyV1(_mockRestService.Object, _mockGetByNameStrategy.Object);
		}

		[Test]
		public void Update_WithValidGroupWithArtifactID_ShouldCallRestServiceWithExpectedParameters()
		{
			var groupToUpdate = new Group
			{
				ArtifactID = _EXISTING_GROUP_ARTIFACT_ID,
				Name = _UPDATED_NAME,
				Client = _client,
				Keywords = _UPDATED_KEYWORDS,
				Notes = _UPDATED_NOTES
			};
			SetupMockRestService(groupToUpdate);

			_sut.Update(groupToUpdate);

			_mockRestService.Verify(
				restService => restService.Put<GroupResponse>(
					_updateUrl, It.Is<GroupDTO>(dto => CheckIfGroupRequestIsEquivalentToExpected(dto.GroupRequest, groupToUpdate)), null),
				Times.Once);
		}

		[Test]
		public void Update_WithValidGroupWithoutArtifactIDWithName_ShouldCallGetByNameStrategy()
		{
			var groupToUpdate = new Group
			{
				Name = _EXISTING_GROUP_NAME,
				Client = _client,
				Keywords = _UPDATED_KEYWORDS,
				Notes = _UPDATED_NOTES
			};
			SetupMockGetByNameStrategy();
			SetupMockRestService(groupToUpdate);

			_sut.Update(groupToUpdate);

			_mockGetByNameStrategy.Verify(getByNameStrategy => getByNameStrategy.Get(_EXISTING_GROUP_NAME), Times.Once);
		}

		[Test]
		public void Update_WithValidGroupWithoutArtifactIDWithName_ShouldCallRestServiceWithExpectedParamaters()
		{
			var groupToUpdate = new Group
			{
				Name = _EXISTING_GROUP_NAME,
				Client = _client,
				Keywords = _UPDATED_KEYWORDS,
				Notes = _UPDATED_NOTES
			};
			SetupMockGetByNameStrategy();
			SetupMockRestService(groupToUpdate);

			_sut.Update(groupToUpdate);

			_mockRestService.Verify(
				restService => restService.Put<GroupResponse>(
					_updateUrl, It.Is<GroupDTO>(dto => CheckIfGroupRequestIsEquivalentToExpected(dto.GroupRequest, groupToUpdate)), null),
				Times.Once);
		}

		[Test]
		public void Update_WithoutArtifactIDWithNonexistentName_ShouldThrowArgumentException()
		{
			string nonexistentName = "Nonexistent Group Name";
			var groupToUpdate = new Group
			{
				Name = nonexistentName,
				Client = _client,
				Keywords = _UPDATED_KEYWORDS,
				Notes = _UPDATED_NOTES
			};
			_mockGetByNameStrategy
				.Setup(getByNameStrategy => getByNameStrategy.Get(nonexistentName))
				.Returns<Group>(null);

			ArgumentException exception = Assert.Throws<ArgumentException>(() => _sut.Update(groupToUpdate));
			exception.Message.Should().Contain("Can't find the entity");
		}

		[Test]
		public void Update_WithoutArtifactIDAndName_ThrowsArgumentException()
		{
			var groupToUpdate = new Group
			{
				Client = _client,
				Keywords = _UPDATED_KEYWORDS,
				Notes = _UPDATED_NOTES
			};

			ArgumentException exception = Assert.Throws<ArgumentException>(() => _sut.Update(groupToUpdate));
			exception.Message.Should().Contain("This entity should have an artifact ID or name as an identifier");
		}

		[Test]
		public void Update_WithNullGroup_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.Update(null));
		}

		private bool CheckIfGroupRequestIsEquivalentToExpected(GroupRequest request, Group groupToUpdate)
		{
			return request.Name.Equals(groupToUpdate.Name) &&
				request.Client.Value.ArtifactID == _CLIENT_ARTIFACT_ID &&
				request.Notes.Equals(groupToUpdate.Notes) &&
				request.Keywords.Equals(groupToUpdate.Keywords);
		}

		private void SetupMockGetByNameStrategy()
		{
			Group groupToReturn = new Group
			{
				ArtifactID = _EXISTING_GROUP_ARTIFACT_ID,
			};
			_mockGetByNameStrategy
				.Setup(getByNameStrategy => getByNameStrategy.Get(_EXISTING_GROUP_NAME))
				.Returns(groupToReturn);
		}

		private void SetupMockRestService(Group groupToUpdate)
		{
			var groupResponse = new GroupResponse
			{
				Name = groupToUpdate.Name,
				ArtifactID = groupToUpdate.ArtifactID,
				Keywords = groupToUpdate.Keywords,
				Notes = groupToUpdate.Notes,
				Client = new Securable<NamedArtifactWithGuids>(new NamedArtifactWithGuids
				{
					ArtifactID = groupToUpdate.Client.ArtifactID,
					Name = groupToUpdate.Client.Name
				})
			};
			_mockRestService
				.Setup(restService => restService.Put<GroupResponse>(_updateUrl, It.IsAny<GroupDTO>(), null))
				.Returns(groupResponse);
		}
	}
}
