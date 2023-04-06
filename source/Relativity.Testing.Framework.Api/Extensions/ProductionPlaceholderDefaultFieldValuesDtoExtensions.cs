using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Extensions
{
	internal static class ProductionPlaceholderDefaultFieldValuesDtoExtensions
	{
		public static DefaultFieldValue<NamedArtifact> MapToDefaultFieldValue(this ProductionPlaceholderDefaultFieldValuesDto dto)
		{
			return new DefaultFieldValue<NamedArtifact>
			{
				ArtifactID = dto.Type.ID,
				Guid = dto.Type.Guid,
				DefaultValue = new NamedArtifact
				{
					ArtifactID = dto.Type.DefaultValue.ID,
					Name = dto.Type.DefaultValue.Name
				}
			};
		}
	}
}
