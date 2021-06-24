using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(FieldGetByIdStrategy<Field>))]
	internal class FieldGetByIdStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<Field>>
	{
		private ObjectType _associativeObjectType;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_associativeObjectType = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<ObjectType>>().
				Get(DefaultWorkspace.ArtifactID, "Layout");
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.
				Get(DefaultWorkspace.ArtifactID, int.MaxValue);

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

			var result = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<MultipleObjectField>>()
				.Get(DefaultWorkspace.ArtifactID, existingField.ArtifactID);

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

			var result = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<SingleObjectField>>()
				.Get(DefaultWorkspace.ArtifactID, existingField.ArtifactID);

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

		private void TestGet<TFieldModel>()
			where TFieldModel : Field, new()
		{
			TFieldModel existingField = null;

			Arrange(() =>
			{
				existingField = Facade.Resolve<ICreateWorkspaceEntityStrategy<TFieldModel>>().
					Create(DefaultWorkspace.ArtifactID, new TFieldModel());
			});

			var result = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<TFieldModel>>()
				.Get(DefaultWorkspace.ArtifactID, existingField.ArtifactID);

			result.ObjectType.Name.Should().Be(existingField.ObjectType.Name);
			result.Should().BeEquivalentTo(existingField, o => o.Excluding(x => x.ObjectType));
		}
	}
}
