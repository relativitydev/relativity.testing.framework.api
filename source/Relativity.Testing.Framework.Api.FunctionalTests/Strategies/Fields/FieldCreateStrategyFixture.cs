using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(FieldCreateStrategy<Field>))]
	internal class FieldCreateStrategyFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<FixedLengthTextField>>
	{
		private ObjectType _objectType;
		private ObjectType _associativeObjectType;
		private Field _fieldForPropagation;

		public FieldCreateStrategyFixture()
		{
		}

		public FieldCreateStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_objectType = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<ObjectType>>().
				Get(DefaultWorkspace.ArtifactID, "Document");

			_associativeObjectType = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<ObjectType>>().
				Get(DefaultWorkspace.ArtifactID, "Layout");

			_fieldForPropagation = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<Field>>().
				Get(DefaultWorkspace.ArtifactID, "MD5 Hash");
		}

		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Create(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Create_DateField()
		{
			TestCreate<DateField>();
		}

		[Test]
		public void Create_DecimalField()
		{
			TestCreate<DecimalField>();
		}

		[Test]
		public void Create_LongTextField()
		{
			TestCreate<LongTextField>();
		}

		[Test]
		public void Create_MultipleChoiceField()
		{
			TestCreate<MultipleChoiceField>();
		}

		[Test]
		public void Create_MultipleObjectField()
		{
			var field = new MultipleObjectField
			{
				Name = Randomizer.GetString("Field {0}"),
				ObjectType = _objectType,
				AssociativeObjectType = _associativeObjectType,
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

			var result = Facade.Resolve<ICreateWorkspaceEntityStrategy<MultipleObjectField>>()
				.Create(DefaultWorkspace.ArtifactID, field);

			result.ArtifactID.Should().BePositive();
			result.AssociativeObjectType.Name.Should().Be(field.AssociativeObjectType.Name);
			result.ObjectType.Name.Should().Be(field.ObjectType.Name);

			result.Should().BeEquivalentTo(field, o => o.Excluding(x => x.ArtifactID).
				Excluding(x => x.ObjectType).
				Excluding(x => x.AssociativeObjectType));
		}

		[Test]
		public void Create_SingleChoiceField()
		{
			TestCreate<SingleChoiceField>();
		}

		[Test]
		public void Create_SingleObjectField()
		{
			var field = new SingleObjectField
			{
				Name = Randomizer.GetString("Field {0}"),
				ObjectType = _objectType,
				AssociativeObjectType = _associativeObjectType,
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

			var result = Facade.Resolve<ICreateWorkspaceEntityStrategy<SingleObjectField>>()
				.Create(DefaultWorkspace.ArtifactID, field);

			result.ArtifactID.Should().BePositive();
			result.AssociativeObjectType.Name.Should().Be(field.AssociativeObjectType.Name);
			result.ObjectType.Name.Should().Be(field.ObjectType.Name);

			result.Should().BeEquivalentTo(field, o => o.Excluding(x => x.ArtifactID).
				Excluding(x => x.ObjectType).
				Excluding(x => x.AssociativeObjectType));
		}

		[Test]
		public void Create_UserField()
		{
			TestCreate<UserField>();
		}

		[Test]
		public void Create_WholeNumberField()
		{
			TestCreate<WholeNumberField>();
		}

		[Test]
		public void Create_YesNoField()
		{
			TestCreate<YesNoField>();
		}

		[Test]
		public void Create_FixedLengthTextField()
		{
			TestCreate<FixedLengthTextField>();
		}

		[Test]
		public void Create_CurencyField()
		{
			TestCreate<CurrencyField>();
		}

		[Test]
		public void Create_Relational_FixedLengthTextField()
		{
			const string fileName = "single_image.jpg";

			var base64String = Convert.ToBase64String(File.ReadAllBytes($@"{AppDomain.CurrentDomain.BaseDirectory}\files\{fileName}"));

			FixedLengthTextField field = new FixedLengthTextField
			{
				IsRelational = true,
				FriendlyName = Randomizer.GetString("Friendly Name {0}"),
				ImportBehavior = FieldImportBehavior.LeaveBlankValuesUnchanged,
				PaneIcon = new FieldPaneIcon
				{
					FileName = fileName,
					ImageBase64 = base64String
				},
				Order = 1
			};

			var result = Facade.Resolve<ICreateWorkspaceEntityStrategy<FixedLengthTextField>>()
				.Create(DefaultWorkspace.ArtifactID, field);

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNull();
			result.ObjectType.Name.Should().Be("Document");
			result.PaneIcon.FileName.Should().Contain(fileName);
			result.Should().BeEquivalentTo(field, o => o.
				Excluding(x => x.ArtifactID).
				Excluding(x => x.Name).
				Excluding(x => x.ObjectType).
				Excluding(x => x.PropagateTo).
				Excluding(x => x.PaneIcon.FileName));
		}

		private void TestCreate<TFieldModel>()
			where TFieldModel : Field, new()
		{
			TFieldModel field = new TFieldModel
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

			var result = Facade.Resolve<ICreateWorkspaceEntityStrategy<TFieldModel>>()
				.Create(DefaultWorkspace.ArtifactID, field);

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNull();
			result.ObjectType.Name.Should().Be("Document");
			result.Should().BeEquivalentTo(field, o => o.Excluding(x => x.ArtifactID).
				Excluding(x => x.Name).
				Excluding(x => x.ObjectType));
		}
	}
}
