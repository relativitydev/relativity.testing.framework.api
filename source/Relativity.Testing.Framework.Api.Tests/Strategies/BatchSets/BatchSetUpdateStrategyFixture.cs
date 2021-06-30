using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(BatchSetUpdateStrategyV1))]
	public class BatchSetUpdateStrategyFixture
	{
		private const string _INVALID_DATA_SOURCE_EXCEPTION_MESSAGE = "Data Source must not be null and have Artifact ID greater than zero";
		private const string _INVALID_BATCH_SET_NAME_EXCEPTION_MESSAGE = "Batch Set Name cannot be null, empty nor whitespace.";
		private const string _INVALID_BATCH_SET_ID_EXCEPTION_MESSAGE = "Batch Set ID should be greater than zero.";
		private const string _INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE = "Workspace ID should be greater than zero or equal to -1 for admin context.";

		private BatchSetUpdateStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;

		[OneTimeSetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_sut = new BatchSetUpdateStrategyV1(_mockRestService.Object);
		}

		[Test]
		public void Update_WithNullEntity_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() =>
				_sut.Update(1, null));
		}

		[Test]
		public void Update_WithInvalidWorkspaceId_ThrowsException()
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

			var result = Assert.Throws<ArgumentException>(() =>
							_sut.Update(-2, batchSet));

			result.Message.Should().Contain(_INVALID_WORKSPACE_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Update_WithInvalidBatchSetId_ThrowsException()
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
			TestIfUpdateThrowsExpectedException(batchSet, _INVALID_BATCH_SET_ID_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Update_WithInavlidDataSourceId_ThrowsException()
		{
			var batchSet = new BatchSet
			{
				ArtifactID = 1,
				Name = Randomizer.GetString(),
				BatchSize = 1500,
				BatchPrefix = Randomizer.GetString("BS", 3),
				DataSource = new NamedArtifact
				{
					ArtifactID = -1
				}
			};
			TestIfUpdateThrowsExpectedException(batchSet, _INVALID_DATA_SOURCE_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Update_WithNullDataSource_ThrowsException()
		{
			var batchSet = new BatchSet
			{
				ArtifactID = 1,
				Name = Randomizer.GetString(),
				BatchSize = 1500,
				BatchPrefix = Randomizer.GetString("BS", 3),
				DataSource = null
			};

			TestIfUpdateThrowsExpectedException(batchSet, _INVALID_DATA_SOURCE_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Update_WithNullName_ThrowsException()
		{
			var batchSet = new BatchSet
			{
				ArtifactID = 1,
				Name = null,
				BatchSize = 1500,
				BatchPrefix = Randomizer.GetString("BS", 3),
				DataSource = new NamedArtifact
				{
					ArtifactID = 1
				}
			};

			TestIfUpdateThrowsExpectedException(batchSet, _INVALID_BATCH_SET_NAME_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Update_WithEmptyName_ThrowsException()
		{
			var batchSet = new BatchSet
			{
				ArtifactID = 1,
				Name = string.Empty,
				BatchSize = 1500,
				BatchPrefix = Randomizer.GetString("BS", 3),
				DataSource = new NamedArtifact
				{
					ArtifactID = 1
				}
			};

			TestIfUpdateThrowsExpectedException(batchSet, _INVALID_BATCH_SET_NAME_EXCEPTION_MESSAGE);
		}

		[Test]
		public void Update_WithWhitespaceName_ThrowsException()
		{
			var batchSet = new BatchSet
			{
				ArtifactID = 1,
				Name = "	",
				BatchSize = 1500,
				BatchPrefix = Randomizer.GetString("BS", 3),
				DataSource = new NamedArtifact
				{
					ArtifactID = 1
				}
			};

			TestIfUpdateThrowsExpectedException(batchSet, _INVALID_BATCH_SET_NAME_EXCEPTION_MESSAGE);
		}

		private void TestIfUpdateThrowsExpectedException(BatchSet batchSet, string expectedExceptionMessage)
		{
			var result = Assert.Throws<ArgumentException>(() =>
				_sut.Update(1, batchSet));

			result.Message.Should().Contain(expectedExceptionMessage);
		}
	}
}
