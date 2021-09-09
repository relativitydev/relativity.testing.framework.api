using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingSetCreateStrategyV1))]
	public class ImagingSetCreateStrategyV1Fixture
	{
		private const int _WORKSPACE_ID = 100000;
		private readonly ImagingSetRequest _validImagingSetRequest = new ImagingSetRequest
		{
			DataSourceID = 1,
			ImagingProfileID = 2,
			Name = "Test Name"
		};

		private readonly string _createUrl = $"relativity-imaging/v1/workspaces/{_WORKSPACE_ID}/imaging-sets";

		private ImagingSetCreateStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IImagingSetGetStrategy> _imagingSetGetStrategy;
		private Mock<IImagingSetValidatorV1> _imagingSetValidator;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_imagingSetGetStrategy = new Mock<IImagingSetGetStrategy>();
			_imagingSetValidator = new Mock<IImagingSetValidatorV1>();

			_sut = new ImagingSetCreateStrategyV1(_mockRestService.Object, _imagingSetValidator.Object, _imagingSetGetStrategy.Object);
		}

		[Test]
		public void Create_WithAnyParameters_ShouldCallImagingSetValidator()
		{
			_sut.Create(_WORKSPACE_ID, _validImagingSetRequest);
			_imagingSetValidator.Verify(validator => validator.ValidateImagingSetCreateRequest(_WORKSPACE_ID, _validImagingSetRequest), Times.Once);
		}

		[Test]
		public void Create_ShouldCallIRestService()
		{
			_sut.Create(_WORKSPACE_ID, _validImagingSetRequest);
			_mockRestService.Verify(restService => restService.Post<int>(_createUrl, It.IsAny<ImagingSetRequestDtoV1>(), 2, null), Times.Once);
		}
	}
}
