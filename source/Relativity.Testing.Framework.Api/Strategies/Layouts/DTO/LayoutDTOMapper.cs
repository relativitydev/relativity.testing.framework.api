using Relativity.Testing.Framework.Api.Strategies.Layouts.DTO;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies.Layouts
{
	internal static class LayoutDTOMapper
	{
		internal static Layout DoMappingFromDTO(this LayoutDTOV1 value)
		{
			var mapped = new Layout
			{
				Name = value.ObjectIdentifier.Name,
				ArtifactID = value.ObjectIdentifier.ArtifactID,
				ObjectType = value.ObjectType.Value,
				Order = value.Order,
				OverwriteProtection = value.OverwriteProtection,
				AllowCopyFromPrevious = value.AllowCopyFromPrevious,
				RelativityApplications = value.RelativityApplications.ViewableItems,
				Owner = value.Owner.Value
			};
			return mapped;
		}
	}
}
