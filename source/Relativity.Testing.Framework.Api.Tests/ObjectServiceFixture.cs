using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Attributes;
using Relativity.Testing.Framework.Mapping;

namespace Relativity.Testing.Framework.Api.Tests
{
	[TestFixture]
	[TestOf(typeof(IObjectService))]
	public class ObjectServiceFixture
	{
		private const string NAME = "The Field Name";
		private const string GUID = "6a95768d-1dda-44c1-92fa-a6c43fcd89d2";
		private const string GUID2 = "6a95768d-1dda-44c1-92fa-a6c43fcd89d3";
		private const int ARTIFACTID = 1016247;

		private readonly QuerySlimObject _querySlim = new QuerySlimObject
		{
			ArtifactID = 1016261,
			Values = new object[]
			{
					1016261L,
					1016262L,
					"Password",
					"Authenticate using an e-mail address and a password.",
					"Just some more text."
			}
		};

		private readonly QueryResultField[] _fields = new[]
		{
			new QueryResultField
			{
				ArtifactID = 1016246,
				FieldCategory = "Generic",
				FieldType = "WholeNumber",
				Name = "Field With No Attributes"
			},
			new QueryResultField
			{
				ArtifactID = 1016299,
				FieldCategory = "Generic",
				FieldType = "WholeNumber",
				Name = NAME
			},
			new QueryResultField
			{
				ArtifactID = ARTIFACTID,
				FieldCategory = "Generic",
				FieldType = "FixedLengthText",
				Name = "Name"
			},
			new QueryResultField
			{
				ArtifactID = 1016253,
				FieldCategory = "Generic",
				FieldType = "LongText",
				Name = "Description",
				Guids = new List<Guid>
				{
					Guid.Parse(GUID)
				}
			},
			new QueryResultField
			{
				ArtifactID = 1016253,
				FieldCategory = "Generic",
				FieldType = "LongText",
				Name = "Description2",
				Guids = new List<Guid>
				{
					Guid.Parse(GUID2)
				}
			}
		};

		private Mock<IRestService> _mockRestService;
		private IObjectMappingService _objectMappingService;
		private ObjectService _objectService;
		private SomeObject _result;

		[OneTimeSetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_objectMappingService = new ObjectMappingService();
			_objectService = new ObjectService(_mockRestService.Object, _objectMappingService);

			// The Mapping/QuerySlim stuff is muddled between the two repositories.
			// It really should be moved all the way into one, and then more granular tests can be written.
			_result = Activator.CreateInstance<SomeObject>();
			_objectService.MapObject(_querySlim, _result, _fields);
		}

		[Test]
		public void MapObject_MapsReturnedFieldsWhenNoAttributesArePresent()
		{
			_result.FieldWithNoAttributes.Should().Be(1016261);
		}

		[Test]
		public void MapObject_MapsReturnedFieldsWhenNameAttributeIsPresent()
		{
			_result.FieldWithNameAttribute.Should().Be(1016262);
		}

		[Test]
		public void MapObject_MapsReturnedFieldsWhenArtifactIdAttributeIsPresent()
		{
			_result.FieldWithArtifactIdAttribute.Should().Be("Password");
		}

		[Test]
		public void MapObject_MapsReturnedFieldsWhenGuidAttributeIsPresent()
		{
			_result.FieldWithGuidAttribute.Should().Be("Authenticate using an e-mail address and a password.");
		}

		[Test]
		public void MapObject_UsesGuidFirstWhenMultipleAttributesArePresent()
		{
			_result.FieldWithAllAttributes.Should().Be("Just some more text.");
		}

		internal class SomeObject
		{
			public int FieldWithNoAttributes { get; set; }

			[FieldName(NAME)]
			public int FieldWithNameAttribute { get; set; }

			[FieldArtifactId(ARTIFACTID)]
			public string FieldWithArtifactIdAttribute { get; set; }

			[FieldGuid(GUID)]
			public string FieldWithGuidAttribute { get; set; }

			[FieldGuid(GUID2)]
			[FieldArtifactId(ARTIFACTID)]
			[FieldName(NAME)]
			public string FieldWithAllAttributes { get; set; }
		}
	}
}
