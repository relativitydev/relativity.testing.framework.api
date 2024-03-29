﻿using System;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ApplicationFieldCodeCreateStrategyV1))]
	public class ApplicationFieldCodeCreateStrategyV1Fixture
	{
		private const int _WORKSPACE_ID = 100000;

		private ApplicationFieldCodeCreateStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IApplicationFieldCodeGetStrategy> _applicationFieldCodeGetStrategy;
		private Mock<IWorkspaceIdValidator> _workspaceIdValidator;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_applicationFieldCodeGetStrategy = new Mock<IApplicationFieldCodeGetStrategy>();
			_workspaceIdValidator = new Mock<IWorkspaceIdValidator>();

			_sut = new ApplicationFieldCodeCreateStrategyV1(_mockRestService.Object, _applicationFieldCodeGetStrategy.Object, _workspaceIdValidator.Object);
		}

		[Test]
		public void Create_WithNull_ShouldThrowArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.Create(_WORKSPACE_ID, null));
		}

		[Test]
		public void Create_WithAnyWorkspaceId_ShouldCallValidator()
		{
			_sut.Create(_WORKSPACE_ID, new ApplicationFieldCode());
			_workspaceIdValidator.Verify(x => x.Validate(_WORKSPACE_ID), Times.Once);
		}
	}
}
