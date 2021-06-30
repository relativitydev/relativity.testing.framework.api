using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(BatchSetCreateBatchesStrategyV1))]
	public class BatchSetCreateBatchesStrategyFixture
	{
		private const string _INVALID_BATCH_SET_ID_EXCEPTION_MESSAGE = "Batch Set ID should be greater than zero.";
		private const string _INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE = "Workspace ID should be greater than zero or equal to -1 for admin context.";
		private const string _NONEXISTENT_BATCH_SET_ID_EXCEPTION_MESSAGE = "The batch set with ID: 1 does not exists";

		private BatchSetCreateBatchesStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IExistsBatchSetByIdStrategy> _mockExistsByIdStrategy;

		[OneTimeSetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockExistsByIdStrategy = new Mock<IExistsBatchSetByIdStrategy>();
			_sut = new BatchSetCreateBatchesStrategyV1(_mockRestService.Object, _mockExistsByIdStrategy.Object);
		}

		[Test]
		public void CreateBatches_WithInvalidBatchSetId_ThrowsException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
							_sut.CreateBatches(1, -1));

			result.Message.Should().Contain(_INVALID_BATCH_SET_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void CreateBatches_WithInvalidWorkspaceId_ThrowsException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
							_sut.CreateBatches(-2, 1));

			result.Message.Should().Contain(_INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void CreateBatches_WithNonexistentBatchSetId_ThrowsException()
		{
			var workspaceId = 1;
			var nonExistentBatchSetId = 1;
			_mockExistsByIdStrategy.Setup(existsByIdStrategy => existsByIdStrategy.Exists(workspaceId, nonExistentBatchSetId, null)).Returns(false);

			var result = Assert.Throws<ArgumentException>(() =>
							_sut.CreateBatches(workspaceId, nonExistentBatchSetId));

			result.Message.Should().Contain(_NONEXISTENT_BATCH_SET_ID_EXCEPTION_MESSAGE);
		}
	}
}
