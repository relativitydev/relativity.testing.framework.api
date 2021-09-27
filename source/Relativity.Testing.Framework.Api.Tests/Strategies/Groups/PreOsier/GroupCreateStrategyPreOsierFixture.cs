using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(GroupCreateStrategyPreOsier))]
	internal class GroupCreateStrategyPreOsierFixture
	{
		private const string _CREATE_URL = "relativity.groups/workspace/-1/groups";
		private const string _CLIENT_NAME = "Test Client Name";
		private readonly Group _groupToCreate = new Group
		{
			Name = "Test Group Name",
			Client = new Client
			{
				Name = _CLIENT_NAME
			},
			Keywords = "Test Keywords",
			Notes = "Test Notes"
		};

		private readonly Client _client = new Client
		{
			Name = _CLIENT_NAME,
			ArtifactID = 1
		};

		private Group _expectedGroup;

		private Mock<IRestService> _mockRestService;
		private Mock<IRequireStrategy<Client>> _mockClientRequireService;
		private GroupCreateStrategyPreOsier _sut;

		[SetUp]
		public void SetUp()
		{
			_expectedGroup = new Group
			{
				Name = _groupToCreate.Name,
				Client = new Client
				{
					Name = _CLIENT_NAME,
					ArtifactID = _client.ArtifactID
				},
				Keywords = _groupToCreate.Keywords,
				Notes = _groupToCreate.Notes
			};
			SetupMockRestService();
			SetupMockClientRquireStrategy();
			_sut = new GroupCreateStrategyPreOsier(_mockRestService.Object, _mockClientRequireService.Object);
		}

		[Test]
		public void Create_WithValidGroup_ShouldCallRestServiceWithExpectedUrl()
		{
			_sut.Create(_groupToCreate);

			_mockRestService.Verify(
				restService => restService.Post<GroupResponse>(
					_CREATE_URL, It.Is<GroupDTO>(dto => CheckIfGroupRequestIsEquivalentToExpected(dto.GroupRequest)), 2, null),
				Times.Once);
		}

		private bool CheckIfGroupRequestIsEquivalentToExpected(GroupRequest request)
		{
			return request.Name.Equals(_groupToCreate.Name) &&
				request.Client.Value.ArtifactID == _client.ArtifactID &&
				request.Notes.Equals(_groupToCreate.Notes) &&
				request.Keywords.Equals(_groupToCreate.Keywords);
		}

		[Test]
		public void Create_WithValidGroup_ShouldReturnExpectedGroup()
		{
			Group result = _sut.Create(_groupToCreate);

			result.Should().BeEquivalentTo(_expectedGroup);
		}

		[Test]
		public void Create_WithNull_ShouldThrowNullExceptio()
		{
			Assert.Throws<ArgumentNullException>(() =>
				_sut.Create(null));
		}

		private void SetupMockRestService()
		{
			var mockGroupResponse = new GroupResponse
			{
				Name = _groupToCreate.Name,
				Client = new Securable<NamedArtifactWithGuids>(new NamedArtifactWithGuids
				{
					ArtifactID = _client.ArtifactID,
					Name = _client.Name
				}),
				Keywords = _groupToCreate.Keywords,
				Notes = _groupToCreate.Notes
			};
			_mockRestService = new Mock<IRestService>();
			_mockRestService
				.Setup(restService => restService.Post<GroupResponse>(_CREATE_URL, It.IsAny<GroupDTO>(), 2, null))
				.Returns(mockGroupResponse);
		}

		private void SetupMockClientRquireStrategy()
		{
			_mockClientRequireService = new Mock<IRequireStrategy<Client>>();
			_mockClientRequireService
				.Setup(requireStrategy => requireStrategy.Require(_groupToCreate.Client))
				.Returns(_client);
		}
	}
}
