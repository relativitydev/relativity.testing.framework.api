using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingSetHideStrategyV1))]
	public class ImagingSetHideV1StrategyFixture
	{
		private const int _WORKSPACE_ID = 100000;
		private const int _IMAGING_SET_ID = 100000;

		private readonly string _hideUrl = $"relativity-imaging/v1/workspaces/{_WORKSPACE_ID}/imaging-sets/{_IMAGING_SET_ID}/hide-images";

		private ImagingSetHideStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IImagingSetValidatorV1> _imagingSetValidator;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_imagingSetValidator = new Mock<IImagingSetValidatorV1>();

			_sut = new ImagingSetHideStrategyV1(_mockRestService.Object, _imagingSetValidator.Object);
		}

		[Test]
		public void Hide_WithAnyParameters_ShouldCallImagingSetValidator()
		{
			_sut.Hide(_WORKSPACE_ID, _IMAGING_SET_ID);
			_imagingSetValidator.Verify(validator => validator.ValidateIds(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Once);
		}

		[Test]
		public void Hide_ShouldCallIRestService()
		{
			_sut.Hide(_WORKSPACE_ID, _IMAGING_SET_ID);
			_mockRestService.Verify(restService => restService.Post(_hideUrl, null, null), Times.Once);
		}
	}
}
