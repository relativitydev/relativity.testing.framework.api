using System;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Validators;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingProfileCreateNativeStrategyV1))]
	public class ImagingProfileCreateNativeStrategyV1Fixture
	{
		private const int WorkspaceId = 100000;

		private ImagingProfileCreateNativeStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IImagingProfileGetStrategy> _imagingProfileGetStrategy;
		private Mock<IWorkspaceIdValidator> _workspaceIdValidator;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_imagingProfileGetStrategy = new Mock<IImagingProfileGetStrategy>();
			_workspaceIdValidator = new Mock<IWorkspaceIdValidator>();

			_sut = new ImagingProfileCreateNativeStrategyV1(_mockRestService.Object, _imagingProfileGetStrategy.Object, _workspaceIdValidator.Object);
		}

		[Test]
		public void Create_WithNull_ShouldThrowArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.Create(WorkspaceId, null));
		}

		[Test]
		public void Create_WithAnyWorkspaceId_ShouldCallValidator()
		{
			_sut.Create(WorkspaceId, new CreateNativeImagingProfileDTO());
			_workspaceIdValidator.Verify(x => x.Validate(It.IsAny<int>()), Times.Once);
		}
	}
}
