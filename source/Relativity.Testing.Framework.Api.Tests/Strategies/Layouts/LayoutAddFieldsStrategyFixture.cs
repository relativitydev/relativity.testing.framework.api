using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ILayoutAddFieldsStrategy))]
	public class LayoutAddFieldsStrategyFixture
	{
		private readonly Layout _mockLayout = new Layout { ArtifactID = 12345 };
		private readonly List<Category> _mockCategories = new List<Category>
		{
			new Category
			{
				GroupId = 1,
				Elements = new List<CategoryElement>
				{
					new CategoryElement
					{
						CategoryID = 1,
						Order = 1,
						Elements = new List<Element>
						{
							new Element
							{
								Row = 1,
								Column = 1
							}
						}
					}
				}
			}
		};

		private Mock<IRestService> _mockRestService;
		private Mock<IGetWorkspaceEntityByNameStrategy<Layout>> _layoutGetByNameStrategy;
		private Mock<ILayoutGetCategoriesStrategy> _layoutGetCategoriesStrategy;
		private ILayoutAddFieldsStrategy _layoutAddFieldsStrategy;

		[OneTimeSetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_layoutGetByNameStrategy = new Mock<IGetWorkspaceEntityByNameStrategy<Layout>>();
			_layoutGetCategoriesStrategy = new Mock<ILayoutGetCategoriesStrategy>();
			_layoutAddFieldsStrategy = new LayoutAddFieldsStrategy(_mockRestService.Object, _layoutGetByNameStrategy.Object, _layoutGetCategoriesStrategy.Object);
		}

		[Test]
		public void RemoveExistingFields_RemovesFieldsThatMatch()
		{
			List<Category> categories = new List<Category>
			{
				new Category
				{
					Elements = new List<CategoryElement>
					{
						new CategoryElement
						{
							Elements = new List<Element>
							{
								new Element
								{
									FieldId = 1
								}
							}
						}
					}
				}
			};

			List<CategoryField> fields = new List<CategoryField>
			{
				new CategoryField
				{
					FieldArtifactID = 1
				}
			};

			LayoutAddFieldsStrategy.RemoveExistingFields(categories, fields);

			fields.Should().BeEmpty();
		}

		[Test]
		public static void RemoveExistingFields_DoesNotRemoveFieldsThatDoNotMatch()
		{
			List<Category> categories = new List<Category>
			{
				new Category
				{
					Elements = new List<CategoryElement>
					{
						new CategoryElement
						{
							Elements = new List<Element>
							{
								new Element
								{
									FieldId = 1
								}
							}
						}
					}
				}
			};

			List<CategoryField> fields = new List<CategoryField>
			{
				new CategoryField
				{
					FieldArtifactID = 2
				}
			};

			LayoutAddFieldsStrategy.RemoveExistingFields(categories, fields);

			fields.Should().NotBeNullOrEmpty();
		}

		[Test]
		public static void RemoveExistingFields_DoesNotBreakWhenCategoriesIsEmpty()
		{
			List<Category> categories = new List<Category>
			{
				new Category
				{
					Elements = new List<CategoryElement>
					{
						new CategoryElement
						{
							Elements = new List<Element>
							{
							}
						}
					}
				}
			};

			List<CategoryField> fields = new List<CategoryField>
			{
				new CategoryField
				{
					FieldArtifactID = 2
				}
			};

			Assert.DoesNotThrow(() => LayoutAddFieldsStrategy.RemoveExistingFields(categories, fields));
		}

		[Test]
		public static void RemoveExistingFields_DoesNotBreakWhenFieldsIsEmpty()
		{
			List<Category> categories = new List<Category>
			{
				new Category
				{
					Elements = new List<CategoryElement>
					{
						new CategoryElement
						{
							Elements = new List<Element>
							{
								new Element
								{
									FieldId = 1
								}
							}
						}
					}
				}
			};

			List<CategoryField> fields = new List<CategoryField>
			{
			};

			Assert.DoesNotThrow(() => LayoutAddFieldsStrategy.RemoveExistingFields(categories, fields));
		}

		[Test]
		public static void FillInLayoutProperties_FillsInLayoutProperties()
		{
			int layoutArtifactId = 12345;
			int categoryArtifactID = 11111;
			Category category = new Category
			{
				Elements = new List<CategoryElement>
				{
					new CategoryElement
					{
						CategoryID = categoryArtifactID,
						Elements = new List<Element>
						{
							new Element
							{
								FieldId = 1,
								Row = 1
							}
						}
					}
				}
			};

			List<CategoryField> fields = new List<CategoryField>
			{
				new CategoryField
				{
					FieldArtifactID = 1
				},
				new CategoryField
				{
					FieldArtifactID = 2
				}
			};

			LayoutAddFieldsStrategy.FillInLayoutProperties(layoutArtifactId, category, fields);
			fields.First().Row.Should().Be(2);
			fields.First().LayoutArtifactID.Should().Be(layoutArtifactId);
			fields.First().CategoryID.Should().Be(categoryArtifactID);
			fields.Last().Row.Should().Be(3);
		}

		[Test]
		public void AddFields_ThrowsWhenWorkspaceIdIsZero()
		{
			var exception = Assert.Throws<ArgumentException>(() => _layoutAddFieldsStrategy.AddFields(0, new Layout(), new List<CategoryField> { }));
			exception.Message.Should().Contain("WorkspaceId must be -1 or a valid workspace artifact id.");
		}

		[Test]
		public void AddFields_ThrowsWhenLayoutIsNull()
		{
			Assert.Throws<ArgumentNullException>(() => _layoutAddFieldsStrategy.AddFields(12345, null, new List<CategoryField> { }));
		}

		[Test]
		public void AddFields_ThrowsWhenLayoutHasNoNameOrArtifactId()
		{
			Layout layout = new Layout
			{
				Name = null,
				ArtifactID = 0
			};

			var exception = Assert.Throws<ArgumentException>(() => _layoutAddFieldsStrategy.AddFields(12345, layout, new List<CategoryField> { }));
			exception.Message.Should().Contain($"{typeof(Layout)} model must have a valid ArtifactId or Name set.");
		}

		[Test]
		public void AddFields_ThrowsWhenNoCategoryFieldsArePresent()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => _layoutAddFieldsStrategy.AddFields(12345, _mockLayout, new List<CategoryField> { }));
			exception.Message.Should().Contain($"{typeof(List<CategoryField>)} No fields present after removing existing fields from the layout.");
		}

		[Test]
		public void AddFields_LooksUpLayoutByNameIfArtifactIdIsNotProvided()
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

			List<CategoryField> categoryFields = new List<CategoryField>
			{
				new CategoryField
				{
					FieldArtifactID	= 1,
					DisplayName = "ASuperCoolField",
					FieldDisplayType = FieldDisplayType.Text,
					FieldTypeID = FieldType.FixedLengthText,
					IsRequired = true
				}
			};

			_layoutGetCategoriesStrategy.Setup(e => e.GetCategories(It.IsAny<int>(), It.IsAny<Layout>())).Returns(_mockCategories);

			_mockRestService.Setup(e => e.Post<SaveFieldsAndCustomTextResponse>(
				"Relativity.Services.Layout.Interfaces.ILayoutModule/LayoutBuilderService/SaveFieldsAndCustomText",
				It.IsAny<SaveFieldsAndCustomTextRequest>(),
				2)).Returns(new SaveFieldsAndCustomTextResponse
				{
					Success = true
				});

			_layoutAddFieldsStrategy.AddFields(12345, layout, categoryFields);

			_layoutGetByNameStrategy.Verify(mock => mock.Get(12345, layout.Name), Times.Once());
		}
	}
}
