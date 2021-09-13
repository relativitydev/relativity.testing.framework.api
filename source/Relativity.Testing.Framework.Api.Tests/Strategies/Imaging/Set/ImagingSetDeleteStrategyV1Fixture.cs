using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingSetDeleteStrategyV1))]
	public class ImagingSetDeleteStrategyV1Fixture
	{
		private const int _WORKSPACE_ID = 100000;
		private const int _IMAGING_SET_ID = 100001;

		private readonly string _deleteUrl = $"relativity-imaging/v1/workspaces/{_WORKSPACE_ID}/imaging-sets/{_IMAGING_SET_ID}";

		private ImagingSetDeleteStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IImagingSetValidatorV1> _imagingSetValidator;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_imagingSetValidator = new Mock<IImagingSetValidatorV1>();

			_sut = new ImagingSetDeleteStrategyV1(_mockRestService.Object, _imagingSetValidator.Object);
		}

		[Test]
		public void Delete_WithAnyParameters_ShouldCallImagingSetValidator()
		{
			_sut.Delete(_WORKSPACE_ID, _IMAGING_SET_ID);
			_imagingSetValidator.Verify(validator => validator.ValidateIds(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Once);
		}

		[Test]
		public void Get_ShouldCallIRestService()
		{
			_sut.Delete(_WORKSPACE_ID, _IMAGING_SET_ID);
			_mockRestService.Verify(restService => restService.Delete(_deleteUrl, null, null), Times.Once);
		}
	}
}
