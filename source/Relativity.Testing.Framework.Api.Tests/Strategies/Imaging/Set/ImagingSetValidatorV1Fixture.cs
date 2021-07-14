using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
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

		private readonly ImagingSetRequest _nullNameRequest = new ImagingSetRequest
		{
			DataSourceID = 1,
			ImagingProfileID = 2,
		};

		private readonly ImagingSetRequest _emptyNameRequest = new ImagingSetRequest
		{
			DataSourceID = 1,
			ImagingProfileID = 2,
			Name = string.Empty
		};

		private readonly ImagingSetRequest _whitespaceNameRequest = new ImagingSetRequest
		{
			DataSourceID = 1,
			ImagingProfileID = 2,
			Name = "\n"
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
		public void ValidateImagingSetCreateRequest_WithAnyWorkspaceId_ShouldCallWorkspaceIdValidator()
		{
			_sut.ValidateImagingSetCreateRequest(_WORKSPACE_ID, _defaultImagingSetRequest);
			_workspaceIdValidator.Verify(x => x.Validate(It.IsAny<int>()), Times.Once);
		}

		[Test]
		public void ValidateImagingSetCreateRequest_WithAnyImagingSetRequest_ShouldCallArtifactIdValidatorForDataSource()
		{
			_sut.ValidateImagingSetCreateRequest(_WORKSPACE_ID, _defaultImagingSetRequest);
			_artifactIdValidator.Verify(validator => validator.Validate(It.IsAny<int>(), "Data Source"), Times.Once);
			_artifactIdValidator.Verify(validator => validator.Validate(It.IsAny<int>(), "Imaging Profile"), Times.Once);
		}

		[Test]
		public void ValidateImagingSetCreateRequest_WithAnyImagingSetRequest_ShouldCallArtifactIdValidatorImagingProfile()
		{
			_sut.ValidateImagingSetCreateRequest(_WORKSPACE_ID, _defaultImagingSetRequest);
			_artifactIdValidator.Verify(validator => validator.Validate(It.IsAny<int>(), "Imaging Profile"), Times.Once);
		}

		[Test]
		public void ValidateImagingSetCreateRequest_WithNullName_ShouldThrowArgumnentException()
		{
			TestIfValidateImagingSetCreateRequestThrowsArgumentExceptionForInvalidName(_nullNameRequest);
		}

		[Test]
		public void ValidateImagingSetCreateRequest_WithEmptyName_ShouldThrowArgumnentException()
		{
			TestIfValidateImagingSetCreateRequestThrowsArgumentExceptionForInvalidName(_emptyNameRequest);
		}

		[Test]
		public void ValidateImagingSetCreateRequest_WithWhitespaceName_ShouldThrowArgumnentException()
		{
			TestIfValidateImagingSetCreateRequestThrowsArgumentExceptionForInvalidName(_whitespaceNameRequest);
		}

		private void TestIfValidateImagingSetCreateRequestThrowsArgumentExceptionForInvalidName(ImagingSetRequest request)
		{
			var result = Assert.Throws<ArgumentException>(() => _sut.ValidateImagingSetCreateRequest(_WORKSPACE_ID, request));
			result.Message.Should().Contain(_INVALID_NAME_EXCEPTION_MESSAGE);
		}

		[Test]
		public void ValidateImagingSetCreateRequest_WithNullRequest_ShouldThrowArgumnentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.ValidateImagingSetCreateRequest(_WORKSPACE_ID, null));
		}

		[Test]
		public void ValidateImagingSetUpdateRequest_WithNullName_ShouldThrowArgumnentException()
		{
			TestIfValidateImagingSetUpdateRequestThrowsArgumentExceptionForInvalidName(_nullNameRequest);
		}

		[Test]
		public void ValidateImagingSetUpdateRequest_WithEmptyName_ShouldThrowArgumnentException()
		{
			TestIfValidateImagingSetUpdateRequestThrowsArgumentExceptionForInvalidName(_emptyNameRequest);
		}

		[Test]
		public void ValidateImagingSetUpdateRequest_WithWhitespaceName_ShouldThrowArgumnentException()
		{
			TestIfValidateImagingSetUpdateRequestThrowsArgumentExceptionForInvalidName(_whitespaceNameRequest);
		}

		private void TestIfValidateImagingSetUpdateRequestThrowsArgumentExceptionForInvalidName(ImagingSetRequest request)
		{
			var result = Assert.Throws<ArgumentException>(() => _sut.ValidateImagingSetUpdateRequest(_WORKSPACE_ID, _IMAGING_SET_ID, request));
			result.Message.Should().Contain(_INVALID_NAME_EXCEPTION_MESSAGE);
		}

		[Test]
		public void ValidateImagingSetUpdateRequest_WithNullRequest_ShouldThrowArgumnentNullException()
		{
			Assert.Throws<ArgumentNullException>(() =>
				_sut.ValidateImagingSetUpdateRequest(_WORKSPACE_ID, _IMAGING_SET_ID, null));
		}

		[Test]
		public void ValidateImagingSetUpdateRequest_WithAnyWorkspaceId_ShouldCallWorkspaceIdValidator()
		{
			_sut.ValidateImagingSetUpdateRequest(_WORKSPACE_ID, _IMAGING_SET_ID, _defaultImagingSetRequest);
			_workspaceIdValidator.Verify(x => x.Validate(_WORKSPACE_ID), Times.Once);
		}

		[Test]
		public void ValidateImagingSetUpdateRequest_WithAnyImagingSetId_ShouldCallArtifactIdValidatorForImagingSet()
		{
			TestIfValidateImagingSetRequestCallsArtifactIdValidatorForSpecifiedId(_IMAGING_SET_ID, "Imaging Set");
		}

		[Test]
		public void ValidateImagingSetUpdateRequest_WithAnyImagingSetRequest_ShouldCallArtifactIdValidatorForDataSource()
		{
			TestIfValidateImagingSetRequestCallsArtifactIdValidatorForSpecifiedId(
				_defaultImagingSetRequest.DataSourceID, "Data Source");
		}

		[Test]
		public void ValidateImagingSetUpdateRequest_WithAnyImagingSetRequest_ShouldCallArtifactIdValidatorForImagingProfile()
		{
			TestIfValidateImagingSetRequestCallsArtifactIdValidatorForSpecifiedId(
				_defaultImagingSetRequest.ImagingProfileID, "Imaging Profile");
		}

		private void TestIfValidateImagingSetRequestCallsArtifactIdValidatorForSpecifiedId(int id, string name)
		{
			_sut.ValidateImagingSetUpdateRequest(_WORKSPACE_ID, _IMAGING_SET_ID, _defaultImagingSetRequest);
			_artifactIdValidator.Verify(validator => validator.Validate(id, name), Times.Once);
		}
	}
}
