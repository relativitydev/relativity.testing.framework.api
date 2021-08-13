using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	internal abstract class MatterUpdateStrategyBaseFixture<TStrategy>
		where TStrategy : IMatterUpdateStrategy
	{
		protected const int _MATTER_ID = 3;

		private const string _NO_CLIENT_ID_AND_NAME_EXCEPTION_MESSAGE = "This entity should have an artifact ID or name and client id as an identifier";
		private const string _NO_MATCHING_ENTITY_BY_NAME_AND_CLIENT_ID = "Can't find matter entity by Name and ClientID.";
		private const string _MATTER_NAME = "Test Matter Name";
		private const string _STATUS = "Test Status";
		private const int _CLIENT_ID = 1;
		private const int _STATUS_ID = 2;

		private readonly Client _clientWithArtifactId = new Client
		{
			ArtifactID = _CLIENT_ID,
			Name = "Test Client Name"
		};

		protected TStrategy Sut { get; set; }

		protected Mock<IRestService> MockRestService { get; private set; }

		protected Mock<IMatterStatusGetChoiceIdByNameStrategy> MockMatterStatusGetChoiceIdByNameStrategy { get; private set; }

		protected Mock<IMatterGetByNameAndClientIdStrategy> MockMatterGetByNameAndClientIdStrategy { get; private set; }

		protected void DoSetUp()
		{
			MockRestService = new Mock<IRestService>();
			MockMatterStatusGetChoiceIdByNameStrategy = new Mock<IMatterStatusGetChoiceIdByNameStrategy>();
			MockMatterStatusGetChoiceIdByNameStrategy.
				Setup(getStatusStrategy => getStatusStrategy.GetId(_STATUS)).Returns(_STATUS_ID);
			MockMatterGetByNameAndClientIdStrategy = new Mock<IMatterGetByNameAndClientIdStrategy>();
		}

		protected void TestIfUpdateWithNullThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(null));
		}

		protected void TestIfUpdateWithValidEntityCallsRestServiceWithExpectedUrl(string updateUrl)
		{
			Matter matter = GetTestMatter(true, _clientWithArtifactId);
			MatterUpdateRequest expectedUpdateRequest = GetExpectedMatterUpdateRequest(matter);

			Sut.Update(matter);

			VerifyRestServicePutWasCalledWithExpectedRequest(expectedUpdateRequest, updateUrl);
		}

		protected void TestIfUpdateWithValidEntityAndRestrictedUpdateCallsRestServiceWithRequestWithFilledLastModifiedOnField(string updateUrl)
		{
			Matter matter = GetTestMatter(true, _clientWithArtifactId);
			MatterUpdateRequest expectedUpdateRequest = GetExpectedMatterUpdateRequest(matter, true);

			Sut.Update(matter, true);

			VerifyRestServicePutWasCalledWithExpectedRequest(expectedUpdateRequest, updateUrl);
		}

		protected void TestIfUpdateWithValidEntityCallsMatterStatusGetChoiceIdByNameStrategy()
		{
			Matter matter = GetTestMatter(true, _clientWithArtifactId);

			Sut.Update(matter);

			MockMatterStatusGetChoiceIdByNameStrategy.Verify(getChoiceIdByNameStrategy => getChoiceIdByNameStrategy.GetId(_STATUS), Times.Once);
		}

		protected void TestIfUpdateWithValidEntityWithMatterArtifactIdFilledDoesNotCallMatterGetByNameAndClientIdStrategy()
		{
			Matter matter = GetTestMatter(true, _clientWithArtifactId);

			Sut.Update(matter);

			MockMatterGetByNameAndClientIdStrategy.Verify(getByNameAndClientStrategy => getByNameAndClientStrategy.Get(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
		}

		protected void TestIfUpdateWithoutMatterArtifactIdFilledCallsMatterGetByNameAndClientIdStrategy()
		{
			Matter matter = GetTestMatter(false, _clientWithArtifactId);
			SetupGetByNameAndClientIdStrategyToReturnMatter(matter);

			Sut.Update(matter);

			MockMatterGetByNameAndClientIdStrategy.Verify(getByNameAndClientStrategy => getByNameAndClientStrategy.Get(_MATTER_NAME, _CLIENT_ID), Times.Once);
		}

		protected void TestIfUpdateWithoutMatterArtifactIdAndNoMatchingMatterByNameAndClientIdThrowsArgumentException()
		{
			Matter matter = GetTestMatter(false, _clientWithArtifactId);
			SetupGetByNameAndClientIdStrategyToReturnMatter(null);

			TestIfUpdateThrowsArgumentExceptionWithMessage(matter, _NO_MATCHING_ENTITY_BY_NAME_AND_CLIENT_ID);
		}

		protected void TestIfUpdateWithoutMatterArtifactIdAndNullClientThrowArgumentException()
		{
			Matter matter = GetTestMatter(false, null);

			TestIfUpdateThrowsArgumentExceptionWithMessage(matter);
		}

		protected void TestIfUpdateWithoutMatterArtifactIdAndClientWithInvalidIdThrowsArgumentException()
		{
			Client clientWithInvalidId = GetTestClient(-1);
			Matter matter = GetTestMatter(false, clientWithInvalidId);

			TestIfUpdateThrowsArgumentExceptionWithMessage(matter);
		}

		protected void TestIfUpdateWithoutMatterArtifactIdAndEmptyNameThrowsArgumentException()
		{
			Matter matter = GetTestMatter(false, _clientWithArtifactId, string.Empty);

			TestIfUpdateThrowsArgumentExceptionWithMessage(matter);
		}

		protected void TestIfUpdateWithoutMatterArtifactIdAndWithWhitespaceNameThrowsArgumentException()
		{
			Matter matter = GetTestMatter(false, _clientWithArtifactId, "\t");

			TestIfUpdateThrowsArgumentExceptionWithMessage(matter);
		}

		protected void TestIfUpdateWithoutMatterArtifactIdAndWithNullNameThrowsArgumentException()
		{
			Matter matter = GetTestMatter(false, _clientWithArtifactId, null);

			TestIfUpdateThrowsArgumentExceptionWithMessage(matter);
		}

		private static MatterUpdateRequest GetExpectedMatterUpdateRequest(Matter matter, bool restrictedUpdate = false)
		{
			var expectedUpdateRequest = new MatterUpdateRequest(matter, _STATUS_ID, restrictedUpdate);
			expectedUpdateRequest.MatterRequest.Client.Value.ArtifactID = _CLIENT_ID;
			expectedUpdateRequest.MatterRequest.Status.Value.ArtifactID = _STATUS_ID;
			return expectedUpdateRequest;
		}

		private static Matter GetTestMatter(bool withArtifactID = true, Client client = null, string name = _MATTER_NAME)
		{
			return new Matter
			{
				ArtifactID = withArtifactID ? _MATTER_ID : 0,
				Name = name,
				Number = "3",
				Status = _STATUS,
				Client = client,
				Keywords = "Test Keywords",
				Notes = "Test Notes"
			};
		}

		private static Client GetTestClient(int clientID = _CLIENT_ID)
		{
			return new Client
			{
				ArtifactID = clientID
			};
		}

		private static bool CompareMatterUpdateRequest(MatterUpdateRequest request, MatterUpdateRequest expectedRequest)
		{
			return request.MatterRequest.Name.Equals(expectedRequest.MatterRequest.Name) &&
				request.MatterRequest.Number.Equals(expectedRequest.MatterRequest.Number) &&
				request.MatterRequest.Keywords.Equals(expectedRequest.MatterRequest.Keywords) &&
				request.MatterRequest.Notes.Equals(expectedRequest.MatterRequest.Notes) &&
				request.MatterRequest.Status.Value.ArtifactID == expectedRequest.MatterRequest.Status.Value.ArtifactID &&
				request.MatterRequest.Client.Value.ArtifactID == expectedRequest.MatterRequest.Client.Value.ArtifactID &&
				request.LastModifiedOn == expectedRequest.LastModifiedOn;
		}

		private void TestIfUpdateThrowsArgumentExceptionWithMessage(
			Matter matter, string message = _NO_CLIENT_ID_AND_NAME_EXCEPTION_MESSAGE)
		{
			ArgumentException exception = Assert.Throws<ArgumentException>(() => Sut.Update(matter));

			exception.Message.Should().Contain(message);
		}

		private void SetupGetByNameAndClientIdStrategyToReturnMatter(Matter matterToReturn)
		{
			MockMatterGetByNameAndClientIdStrategy.Setup(
				getByNameAndClientIdStrategy => getByNameAndClientIdStrategy.Get(_MATTER_NAME, _CLIENT_ID))
				.Returns(matterToReturn);
		}

		private void VerifyRestServicePutWasCalledWithExpectedRequest(MatterUpdateRequest expectedUpdateRequest, string updateUrl)
		{
			MockRestService.Verify(
				restService => restService.Put(
					updateUrl,
					It.Is<MatterUpdateRequest>(request => CompareMatterUpdateRequest(request, expectedUpdateRequest)),
					null),
				Times.Once);
		}
	}
}
