using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal static class MatterDTOMapperV1
	{
		internal static Matter DoMappingFromDTO(
			this MatterDTOV1 value,
			ArtifactIdNamePair client,
			string statusName)
		{
			var mapped = new Matter
			{
				Name = value.Name,
				ArtifactID = value.ArtifactID,
				Number = value.Number,
				Client = MapClientFromArtifactIdNamePair(client),
				Status = statusName,
				Keywords = value.Keywords,
				Notes = value.Notes,
				Actions = value.Actions,
				Meta = value.Meta,
				CreatedBy = value.CreatedBy,
				CreatedOn = value.CreatedOn,
				LastModifiedBy = value.LastModifieddBy,
				LastModifiedOn = value.LastModifiedOn
			};
			return mapped;
		}

		private static Client MapClientFromArtifactIdNamePair(ArtifactIdNamePair client)
		{
			return new Client
			{
				ArtifactID = client.ArtifactID,
				Name = client.Name
			};
		}
	}
}
