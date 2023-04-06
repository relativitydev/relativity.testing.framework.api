using System;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingProfileUpdateStrategyV1))]
	public class ImagingProfileUpdateStrategyV1Fixture
	{
		private const int WorkspaceId = 100000;

		private ImagingProfileUpdateStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IWorkspaceIdValidator> _workspaceIdValidator;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_workspaceIdValidator = new Mock<IWorkspaceIdValidator>();

			_sut = new ImagingProfileUpdateStrategyV1(_mockRestService.Object, _workspaceIdValidator.Object);
		}

		[Test]
		public void Update_WithNull_ShouldThrowArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.Update(WorkspaceId, null));
		}

		[Test]
		public void Update_WithAnyWorkspaceId_ShouldCallValidator()
		{
			_sut.Update(WorkspaceId, new ImagingProfile());
			_workspaceIdValidator.Verify(x => x.Validate(It.IsAny<int>()), Times.Once);
		}
	}
}
