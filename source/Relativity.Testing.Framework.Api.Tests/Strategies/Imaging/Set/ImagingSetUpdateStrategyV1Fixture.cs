using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingSetUpdateStrategyV1))]
	public class ImagingSetUpdateStrategyV1Fixture
	{
		private const int _WORKSPACE_ID = 100000;
		private const int _IMAGING_SET_ID = 100001;
		private readonly ImagingSetRequest _validImagingSetRequest = new ImagingSetRequest
		{
			DataSourceID = 1,
			ImagingProfileID = 2,
			Name = "Test Name"
		};

		private readonly string _updateUrl = $"relativity-imaging/v1/workspaces/{_WORKSPACE_ID}/imaging-sets/{_IMAGING_SET_ID}";

		private ImagingSetUpdateStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IImagingSetValidatorV1> _imagingSetValidator;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_imagingSetValidator = new Mock<IImagingSetValidatorV1>();

			_sut = new ImagingSetUpdateStrategyV1(_mockRestService.Object, _imagingSetValidator.Object);
		}

		[Test]
		public void Update_WithAnyParameters_ShouldCallImagingSetValidator()
		{
			_sut.Update(_WORKSPACE_ID, _IMAGING_SET_ID, _validImagingSetRequest);
			_imagingSetValidator.Verify(validator => validator.ValidateImagingSetUpdateRequest(_WORKSPACE_ID, _IMAGING_SET_ID, _validImagingSetRequest), Times.Once);
		}

		[Test]
		public void Create_ShouldCallIRestService()
		{
			_sut.Update(_WORKSPACE_ID, _IMAGING_SET_ID, _validImagingSetRequest);
			_mockRestService.Verify(restService => restService.Post<int>(_updateUrl, It.IsAny<ImagingSetRequestDtoV1>(), 2, null), Times.Once);
		}
	}
}
