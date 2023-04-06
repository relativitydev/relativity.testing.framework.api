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
	[TestOf(typeof(BatchSetValidatorV1))]
	public class BatchSetValidatorFixture
	{
		private const string _INVALID_BATCH_SET_ID_EXCEPTION_MESSAGE = "Batch Set ID should be greater than zero.";
		private const string _INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE = "Workspace ID should be greater than zero or equal to -1 for admin context.";
		private const string _NONEXISTENT_BATCH_SET_ID_EXCEPTION_MESSAGE = "The batch set with ID: 1 does not exists";
		private const int _VALID_WORKSPACE_ID = 1;
		private const int _VALID_BATCH_SET_ID = 1;
		private const int _INVALID_ID = -2;

		private Mock<IExistsBatchSetByIdStrategy> _mockExistsBatchSetByIdStrategy;
		private BatchSetValidatorV1 _sut;

		[OneTimeSetUp]
		public void SetUp()
		{
			var mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();
			mockWorkspaceIdValidator
				.Setup(validator => validator.Validate(It.Is<int>(id => id < -1 || id == 0)))
				.Throws(new ArgumentException("Workspace ID should be greater than zero or equal to -1 for admin context."));
			var mockArtifactIdValidator = new Mock<IArtifactIdValidator>();
			mockArtifactIdValidator
				.Setup(validator => validator.Validate(It.Is<int>(id => id < -1 || id == 0), "Batch Set"))
				.Throws(new ArgumentException("Batch Set ID should be greater than zero."));
			_mockExistsBatchSetByIdStrategy = new Mock<IExistsBatchSetByIdStrategy>();

			_sut = new BatchSetValidatorV1(
				mockWorkspaceIdValidator.Object, mockArtifactIdValidator.Object, _mockExistsBatchSetByIdStrategy.Object);
		}

		[Test]
		public void ValidateWorkspaceId_WithInvalidWorkspaceId_ThrowsExpectedException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
							_sut.ValidateWorkspaceId(_INVALID_ID));

			result.Message.Should().Contain(_INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void ValidateWorkspaceId_WithAdminContextWorkspaceId_DoesNotThrowException()
		{
			Assert.DoesNotThrow(() => _sut.ValidateWorkspaceId(-1));
		}

		[Test]
		public void ValidateWorkspaceId_WithValidPositiveWorkspaceId_DoesNotThrowException()
		{
			Assert.DoesNotThrow(() => _sut.ValidateWorkspaceId(_VALID_WORKSPACE_ID));
		}

		[Test]
		public void ValidateWorkspaceIdAndExistingBatchSetId_WithInvalidWorkspaceId_ThrowsExpectedException()
		{
			TestIfValidateWorkspaceIdAndExistingBatchSetIdThrowsExpectedException(_INVALID_ID, _VALID_BATCH_SET_ID, _INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void ValidateWorkspaceIdAndExistingBatchSetId_WithInvalidBatchSetId_ThrowsExpectedException()
		{
			TestIfValidateWorkspaceIdAndExistingBatchSetIdThrowsExpectedException(_VALID_WORKSPACE_ID, _INVALID_ID, _INVALID_BATCH_SET_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void ValidateWorkspaceIdAndExistingBatchSetId_WithNonexistentBatchSetId_ThrowsExpectedException()
		{
			var workspaceId = 1;
			var nonExistentBatchSetId = 1;
			_mockExistsBatchSetByIdStrategy
				.Setup(validator => validator.Exists(workspaceId, nonExistentBatchSetId, null))
				.Returns(false);

			TestIfValidateWorkspaceIdAndExistingBatchSetIdThrowsExpectedException(workspaceId, nonExistentBatchSetId, _NONEXISTENT_BATCH_SET_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void ValidateWorkspaceIdAndExistingBatchSetId_WithValidIds_DoesNotThrowException()
		{
			var workspaceId = 1;
			var existingBatchSetId = 1;
			_mockExistsBatchSetByIdStrategy
				.Setup(validator => validator.Exists(workspaceId, existingBatchSetId, null))
				.Returns(true);

			Assert.DoesNotThrow(() => _sut.ValidateWorkspaceIdAndExistingBatchSetId(workspaceId, existingBatchSetId, null));
		}

		private void TestIfValidateWorkspaceIdAndExistingBatchSetIdThrowsExpectedException(int workspaceId, int batchSetId, string expectedMessage)
		{
			var result = Assert.Throws<ArgumentException>(() =>
				_sut.ValidateWorkspaceIdAndExistingBatchSetId(workspaceId, batchSetId, null));

			result.Message.Should().Contain(expectedMessage);
		}

		[Test]
		public void ValidateUpdateArguments_WithNullBatchSet_ThrowsExpectedException()
		{
			Assert.Throws<ArgumentNullException>(() =>
				_sut.ValidateUpdateArguments(_VALID_WORKSPACE_ID, null));
		}

		[Test]
		public void ValidateUpdateArguments_WithInvalidWorkspaceId_ThrowsExpectedException()
		{
			var batchSet = new BatchSet
			{
				ArtifactID = -1,
				Name = Randomizer.GetString(),
				BatchSize = 1500,
				BatchPrefix = Randomizer.GetString("BS", 3),
				DataSource = new NamedArtifact
				{
					ArtifactID = 1
				}
			};
			TestIfValidateUpdateArgumentsThrowsExpectedException(_INVALID_ID, batchSet, _INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void ValidateUpdateArguments_WithInvalidBatchSetId_ThrowsExpectedException()
		{
			var batchSet = new BatchSet
			{
				ArtifactID = _INVALID_ID,
				Name = Randomizer.GetString(),
				BatchSize = 1500,
				BatchPrefix = Randomizer.GetString("BS", 3),
				DataSource = new NamedArtifact
				{
					ArtifactID = 1
				}
			};
			TestIfValidateUpdateArgumentsThrowsExpectedException(_VALID_WORKSPACE_ID, batchSet, _INVALID_BATCH_SET_ID_EXCEPTION_MESSAGE);
		}

		private void TestIfValidateUpdateArgumentsThrowsExpectedException(int workspaceId, BatchSet batchSet, string expectedMessage)
		{
			var result = Assert.Throws<ArgumentException>(() =>
				_sut.ValidateUpdateArguments(workspaceId, batchSet, null));

			result.Message.Should().Contain(expectedMessage);
		}
	}
}
