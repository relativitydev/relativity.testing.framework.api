using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(FieldCreateStrategy<Field>))]
	internal class FieldUpdateStrategyFixture : ApiServiceTestFixture<IUpdateWorkspaceEntityStrategy<FixedLengthTextField>>
	{
		private ObjectType _associativeObjectType;
		private Field _fieldForPropagation;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_associativeObjectType = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<ObjectType>>().
				Get(DefaultWorkspace.ArtifactID, "Layout");

			_fieldForPropagation = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<Field>>().
				Get(DefaultWorkspace.ArtifactID, "MD5 Hash");
		}

		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Update_DateField()
		{
			TestUpdate<DateField>();
		}

		[Test]
		public void Update_DecimalField()
		{
			TestUpdate<DecimalField>();
		}

		[Test]
		public void Update_LongTextField()
		{
			TestUpdate<LongTextField>();
		}

		[Test]
		public void Update_MultipleChoiceField()
		{
			TestUpdate<MultipleChoiceField>();
		}

		[Test]
		public void Update_MultipleObjectField()
		{
			MultipleObjectField existingField = null;

			Arrange(() =>
			{
				existingField = Facade.Resolve<ICreateWorkspaceEntityStrategy<MultipleObjectField>>().
					Create(DefaultWorkspace.ArtifactID, new MultipleObjectField
					{
						AssociativeObjectType = _associativeObjectType,
						PropagateTo = new FieldPropagate
						{
							ViewableItems = new List<Artifact> { new Artifact { ArtifactID = _fieldForPropagation.ArtifactID } }
						}
					});
			});

			var toUpdate = existingField.Copy();
			toUpdate.Name = Randomizer.GetString();

			var result = Facade.Resolve<IUpdateWorkspaceEntityStrategy<MultipleObjectField>>().
				Update(DefaultWorkspace.ArtifactID, toUpdate);

			result.ObjectType.Name.Should().Be(toUpdate.ObjectType.Name);
			result.Should().BeEquivalentTo(toUpdate, o => o.Excluding(x => x.ObjectType));
		}

		[Test]
		public void Update_SingleChoiceField()
		{
			TestUpdate<SingleChoiceField>();
		}

		[Test]
		public void Update_SingleObjectField()
		{
			SingleObjectField existingField = null;

			Arrange(() =>
			{
				existingField = Facade.Resolve<ICreateWorkspaceEntityStrategy<SingleObjectField>>().
					Create(DefaultWorkspace.ArtifactID, new SingleObjectField
					{
						AssociativeObjectType = _associativeObjectType,
						PropagateTo = new FieldPropagate
						{
							ViewableItems = new List<Artifact> { new Artifact { ArtifactID = _fieldForPropagation.ArtifactID } }
						}
					});
			});

			var toUpdate = existingField.Copy();
			toUpdate.Name = Randomizer.GetString();

			var result = Facade.Resolve<IUpdateWorkspaceEntityStrategy<SingleObjectField>>().
				Update(DefaultWorkspace.ArtifactID, toUpdate);

			result.ObjectType.Name.Should().Be(toUpdate.ObjectType.Name);
			result.Should().BeEquivalentTo(toUpdate, o => o.Excluding(x => x.ObjectType));
		}

		[Test]
		public void Update_UserField()
		{
			TestUpdate<UserField>();
		}

		[Test]
		public void Update_WholeNumberField()
		{
			TestUpdate<WholeNumberField>();
		}

		[Test]
		public void Update_YesNoField()
		{
			TestUpdate<YesNoField>();
		}

		[Test]
		public void Update_FixedLengthTextField()
		{
			TestUpdate<FixedLengthTextField>();
		}

		[Test]
		public void Update_CurrencyField()
		{
			TestUpdate<CurrencyField>();
		}

		[Test]
		public void Update_Relational_FixedLengthTextField()
		{
			const string fileName = "single_image.jpg";

			var base64String = Convert.ToBase64String(File.ReadAllBytes($@"{AppDomain.CurrentDomain.BaseDirectory}\files\{fileName}"));

			FixedLengthTextField existingField = new FixedLengthTextField();

			Arrange(() =>
			{
				existingField = Facade.Resolve<ICreateWorkspaceEntityStrategy<FixedLengthTextField>>().
					Create(DefaultWorkspace.ArtifactID, new FixedLengthTextField
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
					});
			});

			var toUpdate = existingField.Copy();
			toUpdate.Name = Randomizer.GetString();

			var result = Facade.Resolve<IUpdateWorkspaceEntityStrategy<FixedLengthTextField>>().
				Update(DefaultWorkspace.ArtifactID, toUpdate);

			result.ObjectType.Name.Should().Be(toUpdate.ObjectType.Name);
			result.Should().BeEquivalentTo(toUpdate, o => o.
				Excluding(x => x.ObjectType).
				Excluding(x => x.PropagateTo));
		}

		private void TestUpdate<TFieldModel>()
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

			var toUpdate = existingField.Copy();
			toUpdate.Name = Randomizer.GetString();

			var result = Facade.Resolve<IUpdateWorkspaceEntityStrategy<TFieldModel>>().
				Update(DefaultWorkspace.ArtifactID, toUpdate);

			result.ObjectType.Name.Should().Be(toUpdate.ObjectType.Name);
			result.Should().BeEquivalentTo(toUpdate, o => o.Excluding(x => x.ObjectType));
		}
	}
}
