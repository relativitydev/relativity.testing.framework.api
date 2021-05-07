using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FieldJsonResolver : DefaultContractResolver
	{
		public Type MappingType { get; set; }

		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			if (type == typeof(ObjectType))
			{
				List<MemberInfo> members = GetSerializableMembers(type);
				if (members == null)
				{
					throw new JsonSerializationException("Null collection of serializable members returned.");
				}

				JsonPropertyCollection properties = new JsonPropertyCollection(type);

				foreach (MemberInfo member in members)
				{
					JsonProperty property = CreatePropertyForObjectType(member, memberSerialization);

					if (property != null)
					{
						properties.AddProperty(property);
					}
				}

				IList<JsonProperty> orderedProperties = properties.OrderBy(p => p.Order ?? -1).ToList();
				return orderedProperties;
			}
			else
			{
				return base.CreateProperties(type, memberSerialization);
			}
		}

		protected JsonProperty CreatePropertyForObjectType(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty property = base.CreateProperty(member, memberSerialization);

			if (property.DeclaringType == typeof(ObjectType) && (property.PropertyName != "ArtifactID" && property.PropertyName != "ArtifactTypeID" && property.PropertyName != "Name"))
			{
				property.ShouldSerialize =
					instance =>
					{
						return false;
					};
			}

			return property;
		}

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty property = base.CreateProperty(member, memberSerialization);
			if (property.DeclaringType == typeof(Artifact))
			{
				property.ShouldSerialize =
					instance =>
					{
						var artifact = (Artifact)instance;

						return artifact.ArtifactID != 0;
					};
			}

			if (property.PropertyName == "ArtifactTypeName" || property.PropertyName == "FieldType" || property.PropertyName == nameof(Field.IsIdentifier))
			{
				property.ShouldSerialize =
					instance =>
					{
						return false;
					};
			}

			return property;
		}

		protected override string ResolvePropertyName(string propertyName)
		{
			var options = new ObjectFieldMappingOptions();

			return MappingType != null && ObjectFieldMapping.ContainsProperty(MappingType, propertyName, options)
				? ObjectFieldMapping.GetFieldName(MappingType, propertyName, options)
				: propertyName;
		}

		public static FieldJsonResolver For<TMappingType>()
		{
			return new FieldJsonResolver
			{
				MappingType = typeof(TMappingType)
			};
		}
	}
}
