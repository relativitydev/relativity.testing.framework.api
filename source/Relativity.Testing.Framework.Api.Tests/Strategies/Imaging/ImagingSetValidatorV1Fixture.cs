using System;
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
	[TestOf(typeof(ImagingSetValidatorV1))]
	public class ImagingSetValidatorV1Fixture
	{
		private const string _INVALID_NAME_EXCEPTION_MESSAGE = "Name must be filled and not empty";
		private const int _WORKSPACE_ID = 100000;
		private const int _IMAGING_SET_ID = 100001;
		private readonly ImagingSetRequest _defaultImagingSetRequest = new ImagingSetRequest
		{
			DataSourceID = 1,
			ImagingProfileID = 2,
			Name = "Test Name"
		};

		private ImagingSetValidatorV1 _sut;
		private Mock<IWorkspaceIdValidator> _workspaceIdValidator;
		private Mock<IArtifactIdValidator> _artifactIdValidator;

		[SetUp]
		public void SetUp()
		{
			_workspaceIdValidator = new Mock<IWorkspaceIdValidator>();
			_artifactIdValidator = new Mock<IArtifactIdValidator>();

			_sut = new ImagingSetValidatorV1(_workspaceIdValidator.Object, _artifactIdValidator.Object);
		}

		[Test]
		public void ValidateIds_WithAnyWorkspaceId_ShouldCallWorkspaceIdValidator()
		{
			_sut.ValidateIds(_WORKSPACE_ID, _IMAGING_SET_ID);
			_workspaceIdValidator.Verify(x => x.Validate(It.IsAny<int>()), Times.Once);
		}

		[Test]
		public void ValidateIds_WithAnyImagingSetId_ShouldCallArtifactIdValidator()
		{
			_sut.ValidateIds(_WORKSPACE_ID, _IMAGING_SET_ID);
			_artifactIdValidator.Verify(validator => validator.Validate(It.IsAny<int>(), "Imaging Set"), Times.Once);
		}

		[Test]
		public void ValidateImagingRequest_WithAnyWorkspaceId_ShouldCallWorkspaceIdValidator()
		{
			_sut.ValidateImagingRequest(_WORKSPACE_ID, _defaultImagingSetRequest);
			_workspaceIdValidator.Verify(x => x.Validate(It.IsAny<int>()), Times.Once);
		}

		[Test]
		public void ValidateImagingRequest_WithAnyImagingSetRequest_ShouldCallArtifactIdValidatorForDataSource()
		{
			_sut.ValidateImagingRequest(_WORKSPACE_ID, _defaultImagingSetRequest);
			_artifactIdValidator.Verify(validator => validator.Validate(It.IsAny<int>(), "Data Source"), Times.Once);
			_artifactIdValidator.Verify(validator => validator.Validate(It.IsAny<int>(), "Imaging Profile"), Times.Once);
		}

		[Test]
		public void ValidateImagingRequest_WithAnyImagingSetRequest_ShouldCallArtifactIdValidatorImagingProfile()
		{
			_sut.ValidateImagingRequest(_WORKSPACE_ID, _defaultImagingSetRequest);
			_artifactIdValidator.Verify(validator => validator.Validate(It.IsAny<int>(), "Imaging Profile"), Times.Once);
		}

		[Test]
		public void ValidateImagingRequest_WithNullName_ShouldThrowArgumnentException()
		{
			var request = new ImagingSetRequest
			{
				DataSourceID = 1,
				ImagingProfileID = 2,
			};

			TestIfValidateImagingRequestThrowsArgumentExceptionForInvalidName(request);
		}

		[Test]
		public void ValidateImagingRequest_WithEmptyName_ShouldThrowArgumnentException()
		{
			var request = new ImagingSetRequest
			{
				DataSourceID = 1,
				ImagingProfileID = 2,
				Name = string.Empty
			};

			TestIfValidateImagingRequestThrowsArgumentExceptionForInvalidName(request);
		}

		[Test]
		public void ValidateImagingRequest_WithWhitespaceName_ShouldThrowArgumnentException()
		{
			var request = new ImagingSetRequest
			{
				DataSourceID = 1,
				ImagingProfileID = 2,
				Name = "\n"
			};

			TestIfValidateImagingRequestThrowsArgumentExceptionForInvalidName(request);
		}

		private void TestIfValidateImagingRequestThrowsArgumentExceptionForInvalidName(ImagingSetRequest request)
		{
			var result = Assert.Throws<ArgumentException>(() => _sut.ValidateImagingRequest(_WORKSPACE_ID, request));
			result.Message.Should().Contain(_INVALID_NAME_EXCEPTION_MESSAGE);
		}

		[Test]
		public void ValidateImagingRequest_WithNullRequest_ShouldThrowArgumnentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.ValidateImagingRequest(_WORKSPACE_ID, null));
		}
	}
}
