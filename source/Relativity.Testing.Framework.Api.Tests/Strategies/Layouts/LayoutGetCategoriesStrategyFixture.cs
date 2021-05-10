using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ILayoutGetCategoriesStrategy))]
	public class LayoutGetCategoriesStrategyFixture
	{
		private Mock<IRestService> _mockRestService;
		private Mock<IGetWorkspaceEntityByNameStrategy<Layout>> _layoutGetByNameStrategy;
		private ILayoutGetCategoriesStrategy _layoutGetCategoriesStrategy;

		[OneTimeSetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_layoutGetByNameStrategy = new Mock<IGetWorkspaceEntityByNameStrategy<Layout>>();
			_layoutGetCategoriesStrategy = new LayoutGetCategoriesStrategy(_mockRestService.Object, _layoutGetByNameStrategy.Object);
		}

		[Test]
		public void GetCategories_ThrowsWhenWorkspaceIdIsZero()
		{
			var exception = Assert.Throws<ArgumentException>(() => _layoutGetCategoriesStrategy.GetCategories(0, new Layout()));
			exception.Message.Should().Contain("WorkspaceId must be -1 or a valid workspace artifact id.");
		}

		[Test]
		public void GetCategories_ThrowsWhenLayoutIsNull()
		{
			Assert.Throws<ArgumentNullException>(() => _layoutGetCategoriesStrategy.GetCategories(12345, null));
		}

		[Test]
		public void GetCategories_ThrowsWhenLayoutHasNoNameOrArtifactId()
		{
			Layout layout = new Layout
			{
				Name = null,
				ArtifactID = 0
			};

			var exception = Assert.Throws<ArgumentException>(() => _layoutGetCategoriesStrategy.GetCategories(12345, layout));
			exception.Message.Should().Contain($"{typeof(Layout)} model must have a valid ArtifactId or Name set.");
		}

		[Test]
		public void GetCategories_LooksUpLayoutByNameIfArtifactIdIsNotProvided()
		{
			Layout layout = new Layout
			{
				Name = "SuperCoolLayout",
				ArtifactID = 0
			};

			_layoutGetByNameStrategy.Setup(e => e.Get(12345, layout.Name)).Returns(new Layout
			{
				Name = layout.Name,
				ArtifactID = 1
			});

			_mockRestService.Setup(e => e.Post<ReadSingleAsyncResult>(
					"Relativity.Services.Layout.Interfaces.ILayoutModule/LayoutRenderService/ReadSingleAsync",
					It.IsAny<ReadSingleAsyncRequest>(),
					2)).Returns(new ReadSingleAsyncResult { Groups = new List<Category>() });

			_layoutGetCategoriesStrategy.GetCategories(12345, layout);

			_layoutGetByNameStrategy.Verify(mock => mock.Get(12345, layout.Name), Times.Once());
		}
	}
}
