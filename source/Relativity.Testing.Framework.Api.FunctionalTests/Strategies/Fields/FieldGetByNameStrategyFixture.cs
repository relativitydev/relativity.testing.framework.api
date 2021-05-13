using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(FieldGetByNameStrategy<Field>))]
	internal class FieldGetByNameStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByNameStrategy<Field>>
	{
		private ObjectType _associativeObjectType;

		public FieldGetByNameStrategyFixture()
		{
		}

		public FieldGetByNameStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_associativeObjectType = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<ObjectType>>().
				Get(DefaultWorkspace.ArtifactID, "Layout");
		}

		[Test]
		public void Get_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() => Sut
				.Get(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.
				Get(DefaultWorkspace.ArtifactID, Guid.NewGuid().ToString());

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing_DateField()
		{
			TestGet<DateField>();
		}

		[Test]
		public void Get_Existing_DecimalField()
		{
			TestGet<DecimalField>();
		}

		[Test]
		public void Get_Existing_LongTextField()
		{
			TestGet<LongTextField>();
		}

		[Test]
		public void Get_Existing_MultipleChoiceField()
		{
			TestGet<MultipleChoiceField>();
		}

		[Test]
		public void Get_Existing_MultipleObjectField()
		{
			MultipleObjectField existingField = null;

			Arrange(() =>
			{
				existingField = Facade.Resolve<ICreateWorkspaceEntityStrategy<MultipleObjectField>>().
					Create(DefaultWorkspace.ArtifactID, new MultipleObjectField { AssociativeObjectType = _associativeObjectType });
			});

			var result = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<MultipleObjectField>>()
				.Get(DefaultWorkspace.ArtifactID, existingField.Name);

			result.ObjectType.Name.Should().Be(existingField.ObjectType.Name);
			result.Should().BeEquivalentTo(existingField, o => o.Excluding(x => x.ObjectType));
		}

		[Test]
		public void Get_Existing_SingleChoiceField()
		{
			TestGet<SingleChoiceField>();
		}

		[Test]
		public void Get_Existing_SingleObjectField()
		{
			SingleObjectField existingField = null;

			Arrange(() =>
			{
				existingField = Facade.Resolve<ICreateWorkspaceEntityStrategy<SingleObjectField>>().
					Create(DefaultWorkspace.ArtifactID, new SingleObjectField { AssociativeObjectType = _associativeObjectType });
			});

			var result = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<SingleObjectField>>()
				.Get(DefaultWorkspace.ArtifactID, existingField.Name);

			result.ObjectType.Name.Should().Be(existingField.ObjectType.Name);
			result.Should().BeEquivalentTo(existingField, o => o.Excluding(x => x.ObjectType));
		}

		[Test]
		public void Get_Existing_UserField()
		{
			TestGet<UserField>();
		}

		[Test]
		public void Get_Existing_WholeNumberField()
		{
			TestGet<WholeNumberField>();
		}

		[Test]
		public void Get_Existing_YesNoField()
		{
			TestGet<YesNoField>();
		}

		[Test]
		public void Get_Existing_FixedLengthTextField()
		{
			TestGet<FixedLengthTextField>();
		}

		[Test]
		public void Get_Existing_CurrencyField()
		{
			TestGet<CurrencyField>();
		}

		[Test]
		public void Get_Existing()
		{
			const string fieldName = "Artifact ID";

			var result = Sut
				.Get(DefaultWorkspace.ArtifactID, fieldName);

			result.ArtifactID.Should().BePositive();
			result.Name.Should().Be(fieldName);
		}

		private void TestGet<TFieldModel>()
			where TFieldModel : Field, new()
		{
			TFieldModel existingField = null;

			Arrange(() =>
			{
				existingField = Facade.Resolve<ICreateWorkspaceEntityStrategy<TFieldModel>>().
					Create(DefaultWorkspace.ArtifactID, new TFieldModel());
			});

			var result = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<TFieldModel>>()
				.Get(DefaultWorkspace.ArtifactID, existingField.Name);

			result.ObjectType.Name.Should().Be(existingField.ObjectType.Name);
			result.Should().BeEquivalentTo(existingField, o => o.Excluding(x => x.ObjectType));
		}
	}
}
