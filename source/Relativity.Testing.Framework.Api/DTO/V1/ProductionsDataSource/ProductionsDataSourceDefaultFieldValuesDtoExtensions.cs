using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.DTO
{
	internal static class ProductionsDataSourceDefaultFieldValuesDtoExtensions
	{
		public static ProductionDataSourceDefaultValues MapToDefaultFieldValue(this ProductionsDataSourceDefaultFieldValuesDto dto)
		{
			return new ProductionDataSourceDefaultValues
			{
				BurnRedactions = new DefaultFieldValue<bool>
				{
					ArtifactID = dto.BurnRedactions.ID,
					DefaultValue = dto.BurnRedactions.DefaultValue,
					Guid = dto.BurnRedactions.Guid
				},
				UseImagePlaceholder = new DefaultFieldValue<NamedArtifact>
				{
					ArtifactID = dto.UseImagePlaceholder.ID,
					Guid = dto.UseImagePlaceholder.Guid,
					DefaultValue = new NamedArtifact
					{
						ArtifactID = dto.UseImagePlaceholder.DefaultValue.ID,
						Name = dto.UseImagePlaceholder.DefaultValue.Name
					}
				}
			};
		}
	}
}
