using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(BatchSetGetByIdStrategyV1))]
	public class BatchSetGetByIdStrategyFixture
	{
		private const string _INVALID_BATCH_SET_ID_EXCEPTION_MESSAGE = "Batch Set ID should be greater than zero.";
		private const string _INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE = "Workspace ID should be greater than zero or equal to -1 for admin context.";
		private const string _NONEXISTENT_BATCH_SET_ID_EXCEPTION_MESSAGE = "The batch set with ID: 1 does not exists";

		private BatchSetGetByIdStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IExistsBatchSetByIdStrategy> _mockExistsByIdStrategy;

		[OneTimeSetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockExistsByIdStrategy = new Mock<IExistsBatchSetByIdStrategy>();
			_sut = new BatchSetGetByIdStrategyV1(_mockRestService.Object, _mockExistsByIdStrategy.Object);
		}

		[Test]
		public void GetById_WithInvalidWorkspaceId_ThrowsException()
		{
			TestIfGetByIdThrowsExpectedException(-2, 1, _INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void GetById_WithInvalidBatchSetId_ThrowsException()
		{
			TestIfGetByIdThrowsExpectedException(1, -1, _INVALID_BATCH_SET_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void GetById_WithNonexistentBatchSetId_ThrowsException()
		{
			var workspaceId = 1;
			var nonExistentBatchSetId = 1;
			_mockExistsByIdStrategy.Setup(existsByIdStrategy => existsByIdStrategy.Exists(workspaceId, nonExistentBatchSetId, null)).Returns(false);

			TestIfGetByIdThrowsExpectedException(workspaceId, nonExistentBatchSetId, _NONEXISTENT_BATCH_SET_ID_EXCEPTION_MESSAGE);
		}

		private void TestIfGetByIdThrowsExpectedException(int workspaceId, int batchSetId, string expectedExceptionMessage)
		{
			var result = Assert.Throws<ArgumentException>(() =>
				_sut.Get(workspaceId, batchSetId, null));

			result.Message.Should().Contain(expectedExceptionMessage);
		}
	}
}
