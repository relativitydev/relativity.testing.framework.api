using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(FieldRequireStrategy<Field>))]
	internal class FieldRequireStrategyFixture : ApiServiceTestFixture<IRequireWorkspaceEntityStrategy<FixedLengthTextField>>
	{
		private Field _fieldForPropagation;
		private ObjectType _objectType;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_fieldForPropagation = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<Field>>().
				Get(DefaultWorkspace.ArtifactID, "MD5 Hash");

			_objectType = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<ObjectType>>().
				Get(DefaultWorkspace.ArtifactID, "Document");
		}

		[Test]
		public void Require_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Require(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void RequireMissing_DateField()
		{
			TestRequireMissing<DateField>();
		}

		[Test]
		public void RequireMissing_DecimalField()
		{
			TestRequireMissing<DecimalField>();
		}

		[Test]
		public void RequireMissing_LongTextField()
		{
			TestRequireMissing<LongTextField>();
		}

		[Test]
		public void RequireMissing_MultipleChoiceField()
		{
			TestRequireMissing<MultipleChoiceField>();
		}

		[Test]
		public void RequireMissing_SingleChoiceField()
		{
			TestRequireMissing<SingleChoiceField>();
		}

		[Test]
		public void RequireMissing_UserField()
		{
			TestRequireMissing<UserField>();
		}

		[Test]
		public void RequireMissing_WholeNumberField()
		{
			TestRequireMissing<WholeNumberField>();
		}

		[Test]
		public void RequireMissing_YesNoField()
		{
			TestRequireMissing<YesNoField>();
		}

		[Test]
		public void RequireMissing_FixedLengthTextField()
		{
			TestRequireMissing<FixedLengthTextField>();
		}

		[Test]
		public void RequireMissing_CurencyField()
		{
			TestRequireMissing<CurrencyField>();
		}

		private void TestRequireMissing<TFieldModel>()
			where TFieldModel : Field, new()
		{
			TFieldModel newField = new TFieldModel
			{
				PropagateTo = new FieldPropagate
				{
					ViewableItems = new List<Artifact>
					{
						new Artifact
						{
							ArtifactID = _fieldForPropagation.ArtifactID
						}
					}
				}
			};

			var result = Facade.Resolve<IRequireWorkspaceEntityStrategy<TFieldModel>>()
				.Require(DefaultWorkspace.ArtifactID, newField);

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNull();
			result.ObjectType.Name.Should().Be(_objectType.Name);
			result.Should().BeEquivalentTo(newField, o => o.Excluding(x => x.ArtifactID).
				Excluding(x => x.Name).
				Excluding(x => x.ObjectType));
		}

		[Test]
		public void RequireExisting_DateField()
		{
			TestRequireExisting<DateField>();
		}

		[Test]
		public void RequireExisting_DecimalField()
		{
			TestRequireExisting<DecimalField>();
		}

		[Test]
		public void RequireExisting_LongTextField()
		{
			TestRequireExisting<LongTextField>();
		}

		[Test]
		public void RequireExisting_MultipleChoiceField()
		{
			TestRequireExisting<MultipleChoiceField>();
		}

		[Test]
		public void RequireExisting_SingleChoiceField()
		{
			TestRequireExisting<SingleChoiceField>();
		}

		[Test]
		public void RequireExisting_UserField()
		{
			TestRequireExisting<UserField>();
		}

		[Test]
		public void RequireExisting_WholeNumberField()
		{
			TestRequireExisting<WholeNumberField>();
		}

		[Test]
		public void RequireExisting_YesNoField()
		{
			TestRequireExisting<YesNoField>();
		}

		[Test]
		public void RequireExisting_FixedLengthTextField()
		{
			TestRequireExisting<FixedLengthTextField>();
		}

		[Test]
		public void RequireExisting_CurencyField()
		{
			TestRequireExisting<CurrencyField>();
		}

		private void TestRequireExisting<TFieldModel>()
			where TFieldModel : Field, new()
		{
			TFieldModel existingField = new TFieldModel();

			Arrange(() =>
			{
				existingField = Facade.Resolve<ICreateWorkspaceEntityStrategy<TFieldModel>>().
					Create(DefaultWorkspace.ArtifactID, new TFieldModel
					{
						PropagateTo = new FieldPropagate
						{
							ViewableItems = new List<Artifact> { new Artifact { ArtifactID = _fieldForPropagation.ArtifactID } }
						}
					});
			});

			var require = existingField.Copy();
			require.Name = Randomizer.GetString();

			var result = Facade.Resolve<IRequireWorkspaceEntityStrategy<TFieldModel>>().
				Require(DefaultWorkspace.ArtifactID, require);

			result.ObjectType.Name.Should().Be(require.ObjectType.Name);
			result.Should().BeEquivalentTo(require, o => o.Excluding(x => x.ObjectType));
		}
	}
}
