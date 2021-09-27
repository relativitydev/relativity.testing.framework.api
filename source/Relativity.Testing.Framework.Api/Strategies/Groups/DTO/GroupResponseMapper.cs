﻿using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal static class GroupResponseMapper
	{
		internal static Group DoMappingFromResponse(this GroupResponse value)
		{
			var mapped = new Group
			{
				Name = value.Name,
				ArtifactID = value.ArtifactID,
				Client = MapClientFromNamedArtifactWithGuids(value.Client.Value),
				Type = value.Type,
				Keywords = value.Keywords,
				Notes = value.Notes
			};
			return mapped;
		}

		private static Client MapClientFromNamedArtifactWithGuids(NamedArtifactWithGuids client)
		{
			return new Client
			{
				ArtifactID = client.ArtifactID,
				Name = client.Name
			};
		}
	}
}
