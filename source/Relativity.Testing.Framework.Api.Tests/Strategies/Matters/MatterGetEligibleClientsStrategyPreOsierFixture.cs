﻿using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(MatterGetEligibleClientsStrategyPreOsier))]
	internal class MatterGetEligibleClientsStrategyPreOsierFixture
	{
		private const string _GET_ALL_URL = "Relativity.Services.Matter.IMatterModule/Matter%20Manager/GetClientsForMatterAsync";
		private readonly ArtifactIdNamePair[] _expectedClientsResponse = new[]
		{
			new ArtifactIdNamePair
			{
				ArtifactID = 1,
				Name = "Test Client Name"
			}
		};

		private Mock<IRestService> _mockRestService;
		private MatterGetEligibleClientsStrategyPreOsier _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();

			_sut = new MatterGetEligibleClientsStrategyPreOsier(_mockRestService.Object);
		}

		[Test]
		public void GetAll_ShouldCallRestServiceWithExpectedUrl()
		{
			_sut.GetAll();
			VerifyRestServicePostWasCalled();
		}

		[Test]
		public void GetAll_ShouldReturnRestServiceResponse()
		{
			SetupMockRestService();

			ArtifactIdNamePair[] result = _sut.GetAll();

			result.Should().BeEquivalentTo(_expectedClientsResponse);
		}

		private void VerifyRestServicePostWasCalled()
		{
			_mockRestService.Verify(restService => restService.Post<ArtifactIdNamePair[]>(_GET_ALL_URL, null, 2, null), Times.Once);
		}

		private void SetupMockRestService()
		{
			_mockRestService
				.Setup(restService => restService.Post<ArtifactIdNamePair[]>(_GET_ALL_URL, null, 2, null))
				.Returns(_expectedClientsResponse);
		}
	}
}
