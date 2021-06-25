using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ILayoutAddFieldsStrategy))]
	[VersionRange(">=12.0")]
	internal class LayoutAddFieldsStrategy : ApiServiceTestFixture<ILayoutAddFieldsStrategy>
	{
		private ILayoutGetCategoriesStrategy _layoutGetCategoriesStrategy;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_layoutGetCategoriesStrategy = Facade.Resolve<ILayoutGetCategoriesStrategy>();
		}

		[Test]
		public void AddFields_AddSingleField()
		{
			ObjectType objectType = null;
			Layout layout = null;
			DateField field = null;

			ArrangeWorkingWorkspace(x => x
				.Create(new ObjectType()).Pick(out objectType)
				.Create(new Layout { ObjectType = objectType }).Pick(out layout)
				.Create(new DateField { ObjectType = objectType }).Pick(out field));

			CategoryField categoryField = field.ToCategoryField();

			Sut.AddFields(DefaultWorkspace.ArtifactID, layout, new List<CategoryField> { categoryField });

			List<Category> categories = _layoutGetCategoriesStrategy.GetCategories(DefaultWorkspace.ArtifactID, layout);

			categories.Should().NotBeNullOrEmpty();
			categories.First().Elements.Last().Elements.Last().DisplayName.Should().Be(field.Name);
		}

		[Test]
		public void AddFields_AddMultipleFields()
		{
			ObjectType objectType = null;
			Layout layout = null;
			SingleChoiceField field1 = null;
			SingleChoiceField field2 = null;

			ArrangeWorkingWorkspace(x => x
				.Create(new ObjectType()).Pick(out objectType)
				.Create(new Layout { ObjectType = objectType }).Pick(out layout)
				.Create(new SingleChoiceField { ObjectType = objectType }).Pick(out field1)
				.Create(new SingleChoiceField { ObjectType = objectType }).Pick(out field2));

			CategoryField categoryField1 = field1.ToCategoryField();
			CategoryField categoryField2 = field2.ToCategoryField();

			Sut.AddFields(DefaultWorkspace.ArtifactID, layout, new List<CategoryField> { categoryField1, categoryField2 });

			List<Category> categories = _layoutGetCategoriesStrategy.GetCategories(DefaultWorkspace.ArtifactID, layout);

			categories.Should().NotBeNullOrEmpty();
			categories.First().Elements.Last().Elements.Count.Should().Be(4);
		}
	}
}
