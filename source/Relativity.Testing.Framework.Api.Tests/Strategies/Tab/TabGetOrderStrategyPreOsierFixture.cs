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
	[TestOf(typeof(TabGetOrderStrategyPreOsier))]
	public class TabGetOrderStrategyPreOsierFixture
	{
		private const int _WORKSPACE_ID = 12345;
		private readonly string _getUrl = $"Relativity.Tabs/workspace/{_WORKSPACE_ID}/tabs/vieworderlist";
		private TabGetOrderStrategyPreOsier _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IWorkspaceIdValidator> _mockWorkspaceIdValidator;
		private TabOrderResponsePreOsier _mockTabResponse;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			SetupMockRestService();
			_mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();
			_sut = new TabGetOrderStrategyPreOsier(_mockRestService.Object, _mockWorkspaceIdValidator.Object);
		}

		[Test]
		public void Get_WithAnyParameters_ShouldCallWorkspaceIdValidator()
		{
			_sut.Get(_WORKSPACE_ID);
			_mockWorkspaceIdValidator.Verify(validator => validator.Validate(_WORKSPACE_ID), Times.Once);
		}

		[Test]
		public void Get_ShouldCallIRestService()
		{
			_sut.Get(_WORKSPACE_ID);
			_mockRestService.Verify(restService => restService.Get<List<TabOrderResponsePreOsier>>(_getUrl, null), Times.Once);
		}

		[Test]
		public void Get_ShouldReturnTabWithMappedFields()
		{
			List<Tab> result = _sut.Get(_WORKSPACE_ID);

			result.Should().NotBeNull();
			result.Should().NotBeEmpty();
			result[0].Name.Should().Be(_mockTabResponse.Name);
			result[0].ArtifactID.Should().Be(_mockTabResponse.ArtifactID);
			result[0].Parent.ArtifactID.Should().Be(_mockTabResponse.ParentArtifactID);
			result[0].Order.Should().Be(_mockTabResponse.Order);
		}

		private void SetupMockRestService()
		{
			_mockTabResponse = new TabOrderResponsePreOsier
			{
				Name = "Workspaces",
				ArtifactID = 2,
				ParentArtifactID = 3,
				Order = -1000
			};
			var mockOrderResponse = new List<TabOrderResponsePreOsier>
			{
				_mockTabResponse
			};
			_mockRestService
				.Setup(restService => restService.Get<List<TabOrderResponsePreOsier>>(_getUrl, null))
				.Returns(mockOrderResponse);
		}
	}
}
