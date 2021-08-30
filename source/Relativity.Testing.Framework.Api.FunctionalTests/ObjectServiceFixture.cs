using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Attributes;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests
{
	[TestOf(typeof(ObjectService))]
	internal class ObjectServiceFixture : ApiServiceTestFixture<IObjectService>
	{
		private const string AUTHENTICATIONPROVIDERTYPEGUID = "B98BAC57-E802-4825-9512-9C081BEFEA5A";
		private const string AUTHENTICATIONPROVIDERTYPEDESCRIPTIONFIELD = "6a95768d-1dda-44c1-92fa-a6c43fcd89d2";

		private Workspace _workspace;
		private IDocumentService _documentService;
		private IObjectService _objectService;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_workspace = Facade.Resolve<IWorkspaceService>().CreateWithDocs(new Workspace(), 10);
			_documentService = Facade.Resolve<IDocumentService>();
			_objectService = FacadeHost.Facade.Resolve<IObjectService>();
		}

		[Test]
		public void Mass_Create()
		{
			var entitiesToCreate = new List<Lists> { new Lists().FillRequiredProperties(), new Lists().FillRequiredProperties() };

			var result = Sut.Create<Lists>(-1, entitiesToCreate);

			entitiesToCreate.Count.Should().Be(result.Count);

			foreach (var entityId in result)
			{
				var entity = Sut.Query<Lists>().
					Where(x => x.ArtifactID, entityId).
					First();
				entitiesToCreate.Should().Contain(x => x.Name == entity.Name);
				entity.ArtifactID.Should().BePositive();
			}
		}

		[Test]
		public void Mass_Delete()
		{
			var documents = _documentService.GetAll(_workspace.ArtifactID);
			var documnetsToDelete = documents.Take(2);

			Sut.Delete(_workspace.ArtifactID, documnetsToDelete.Select(x => x.ArtifactID));

			var result = _documentService.GetAll(_workspace.ArtifactID);

			result.Should().NotContain(documnetsToDelete);
		}

		[Test]
		public void Delete()
		{
			var documentToDelete = _documentService.GetAll(_workspace.ArtifactID).First();

			Sut.Delete(_workspace.ArtifactID, documentToDelete.ArtifactID);

			var result = _documentService.GetAll(_workspace.ArtifactID);

			result.Should().NotContain(documentToDelete);
		}

		[Test]
		public void GetAll()
		{
			GroupObject[] items = Sut.GetAll<GroupObject>();

			items.Length.Should().BePositive();

			foreach (GroupObject item in items)
			{
				item.ArtifactID.Should().BePositive();
				item.Name.Should().NotBeNullOrEmpty();
			}
		}

		[Test]
		public void GetAll_WithPropertyFilter()
		{
			string expectedItemName = "System Administrators";

			GroupObject[] items = Sut.GetAll<GroupObject>(x => x.Name, expectedItemName);

			items.Length.Should().Be(1);

			items[0].ArtifactID.Should().BePositive();
			items[0].Name.Should().Be(expectedItemName);
		}

		[Test]
		public void Query()
		{
			GroupObject[] items = Sut.Query<GroupObject>().ToArray();

			items.Length.Should().BePositive();

			foreach (GroupObject item in items)
			{
				item.ArtifactID.Should().BePositive();
				item.Name.Should().NotBeNullOrEmpty();
			}
		}

		[Test]
		public void Query_Workspace_Fields()
		{
			int workspaceID = DefaultWorkspace.ArtifactID;
			Field[] items = Sut.Query<Field>()
				.For(workspaceID).ToArray();

			foreach (Field item in items)
			{
				item.ArtifactID.Should().BePositive();
				item.Name.Should().NotBeNullOrEmpty();
			}
		}

		[Test]
		public void Update()
		{
			var documentToUpdate = _documentService.GetAll(_workspace.ArtifactID).First();

			documentToUpdate.ExtractedText = Randomizer.GetString();

			Sut.Update(_workspace.ArtifactID, documentToUpdate);

			var result = _documentService.GetAll(_workspace.ArtifactID);

			result.Should().Contain(x => x.ExtractedText == documentToUpdate.ExtractedText && x.ArtifactID == documentToUpdate.ArtifactID);
		}

		[VersionRange(">=12.0")]
		[Test]
		public void Mass_Update()
		{
			// Code Smell - Is there a different way we should be setting the value here?
			var documentsToUpdate = _documentService.GetAll(_workspace.ArtifactID)
				.Take(2);

			var extractedTextToUpdate = Randomizer.GetString();
			var fieldValues = new List<FieldRefValuePair> { new FieldRefValuePair { Field = new FieldRef { Name = "Extracted Text" }, Value = extractedTextToUpdate } };

			Sut.Update(_workspace.ArtifactID, new MassUpdateByObjectIdentifiersRequest { Objects = documentsToUpdate, FieldValues = fieldValues });

			var result = _documentService.GetAll(_workspace.ArtifactID);

			foreach (var documentToUpdate in documentsToUpdate)
			{
				result.Should().Contain(x => x.ArtifactID == documentToUpdate.ArtifactID && x.ExtractedText == extractedTextToUpdate);
			}
		}

		[Test]
		public void Query_ByGuid()
		{
			AuthenticationProviderType result = _objectService.GetAll<AuthenticationProviderType>().FirstOrDefault();

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
		}

		[Test]
		public void Query_GuidAttributeTakesPrecedence()
		{
			AuthenticationProviderTypeButWithAnotherName[] result = _objectService.GetAll<AuthenticationProviderTypeButWithAnotherName>();
			result.Should().NotBeNullOrEmpty();
		}

		[Test]
		public void Query_FieldsByGuid()
		{
			AuthenticationProviderTypeButUsingGuidsForFields result = _objectService.GetAll<AuthenticationProviderTypeButUsingGuidsForFields>().FirstOrDefault();

			result.AFieldCalledDescription_ButWereLookingItUpByGuid.Should().NotBeNullOrEmpty();
		}

#pragma warning disable CA1812
		[ObjectTypeName("Group")]
		internal class GroupObject
		{
			public int ArtifactID { get; set; }

			public string Name { get; set; }
		}

		[ObjectTypeGuid(AUTHENTICATIONPROVIDERTYPEGUID)]
		internal class AuthenticationProviderType
		{
			public int ArtifactID { get; set; }

			public string Name { get; set; }
		}

		[ObjectTypeGuid(AUTHENTICATIONPROVIDERTYPEGUID)]
		[ObjectTypeName("SomethingElse")]
		internal class AuthenticationProviderTypeButWithAnotherName
		{
			public int ArtifactID { get; set; }

			public string Name { get; set; }
		}

		[ObjectTypeGuid(AUTHENTICATIONPROVIDERTYPEGUID)]
		internal class AuthenticationProviderTypeButUsingGuidsForFields
		{
			public int ArtifactID { get; set; }

			public string Name { get; set; }

			[FieldGuid(AUTHENTICATIONPROVIDERTYPEDESCRIPTIONFIELD)]
			public string AFieldCalledDescription_ButWereLookingItUpByGuid { get; set; }
		}

		[ObjectTypeName("Dashboard")]
		internal class Lists : NamedArtifact
		{
			public Lists FillRequiredProperties()
			{
				if (string.IsNullOrWhiteSpace(Name))
					Name = Randomizer.GetString("AT_");

				return this;
			}
		}
#pragma warning restore CA1812
	}
}
