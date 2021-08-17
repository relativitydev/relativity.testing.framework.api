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
	[TestOf(typeof(TabGetOrderStrategyV1))]
	public class TabGetOrderStrategyV1Fixture
	{
		private const int _WORKSPACE_ID = 12345;
		private readonly string _getUrl = $"relativity-data-visualization/v1/workspaces/{_WORKSPACE_ID}/tabs/view-order-list";
		private TabGetOrderStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IWorkspaceIdValidator> _mockWorkspaceIdValidator;
		private TabResponseV1 _mockTabResponse;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			SetupMockRestService();
			_mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();
			_sut = new TabGetOrderStrategyV1(_mockRestService.Object, _mockWorkspaceIdValidator.Object);
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
			_mockRestService.Verify(restService => restService.Get<List<TabResponseV1>>(_getUrl, null), Times.Once);
		}

		[Test]
		public void Get_ShouldReturnTabWithMappedFields()
		{
			List<Tab> result = _sut.Get(_WORKSPACE_ID);

			result.Should().NotBeNull();
			result.Should().NotBeEmpty();
			result[0].Name.Should().Be(_mockTabResponse.ObjectIdentifier.Name);
			result[0].ArtifactID.Should().Be(_mockTabResponse.ObjectIdentifier.ArtifactID);
			result[0].Parent.ArtifactID.Should().Be(_mockTabResponse.Parent.Value.ArtifactID);
			result[0].Order.Should().Be(_mockTabResponse.Order);
		}

		private void SetupMockRestService()
		{
			_mockTabResponse = new TabResponseV1
			{
				ObjectIdentifier = new NamedArtifact
				{
					Name = "Workspaces",
					ArtifactID = 1
				},
				Parent = new Securable<NamedArtifact>
				{
					Secured = false,
					Value = new NamedArtifact
					{
						ArtifactID = 2
					}
				},
				Order = -1000
			};
			var mockOrderResponse = new List<TabResponseV1>
			{
				_mockTabResponse
			};
			_mockRestService
				.Setup(restService => restService.Get<List<TabResponseV1>>(_getUrl, null))
				.Returns(mockOrderResponse);
		}
	}
}
