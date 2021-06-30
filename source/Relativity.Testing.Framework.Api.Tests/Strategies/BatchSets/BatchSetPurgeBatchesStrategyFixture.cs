using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(BatchSetPurgeBatchesStrategyV1))]
	public class BatchSetPurgeBatchesStrategyFixture
	{
		private const string _INVALID_BATCH_SET_ID_EXCEPTION_MESSAGE = "Batch Set ID should be greater than zero.";
		private const string _INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE = "Workspace ID should be greater than zero or equal to -1 for admin context.";
		private const string _NONEXISTENT_BATCH_SET_ID_EXCEPTION_MESSAGE = "The batch set with ID: 1 does not exists";

		private BatchSetPurgeBatchesStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IExistsBatchSetByIdStrategy> _mockExistsByIdStrategy;

		[OneTimeSetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockExistsByIdStrategy = new Mock<IExistsBatchSetByIdStrategy>();
			_sut = new BatchSetPurgeBatchesStrategyV1(_mockRestService.Object, _mockExistsByIdStrategy.Object);
		}

		[Test]
		public void PurgeBatches_WithInvalidBatchSetId_ThrowsException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
							_sut.PurgeBatches(1, -1));

			result.Message.Should().Contain(_INVALID_BATCH_SET_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void PurgeBatches_WithInvalidWorkspaceId_ThrowsException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
							_sut.PurgeBatches(-2, 1));

			result.Message.Should().Contain(_INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void PurgeBatches_WithNonexistentBatchSetId_ThrowsException()
		{
			var workspaceId = 1;
			var nonExistentBatchSetId = 1;
			_mockExistsByIdStrategy.Setup(existsByIdStrategy => existsByIdStrategy.Exists(workspaceId, nonExistentBatchSetId, null)).Returns(false);

			var result = Assert.Throws<ArgumentException>(() =>
							_sut.PurgeBatches(workspaceId, nonExistentBatchSetId));

			result.Message.Should().Contain(_NONEXISTENT_BATCH_SET_ID_EXCEPTION_MESSAGE);
		}
	}
}
