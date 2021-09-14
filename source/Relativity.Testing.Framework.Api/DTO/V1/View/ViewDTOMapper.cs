using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.DTO
{
	internal static class ViewDTOMapper
	{
		public static ViewDTO ConvertToDTO(View entity)
		{
			ViewDTO resultDTO = new ViewDTO
			{
				ArtifactID = entity.ArtifactID,
				ArtifactTypeID = entity.ArtifactTypeId,
				Order = entity.Order,
				VisibleInDropdown = entity.VisibleInDropdown,
				QueryHint = entity.QueryHint,
				RelativityApplications = entity.RelativityApplications,
				Name = entity.Name,
				Fields = entity.Fields,
				Sorts = entity.Sorts,
				Dashboard = entity.Dashboard,
				GroupDefinitionFieldArtifactID = entity.GroupDefinitionFieldArtifactId,
				SearchCriteria = entity.SearchCriteria
			};
			if (entity.Owner != null)
			{
				resultDTO.Owner = new Securable<NamedArtifact>
				{
					Value = new NamedArtifact
					{
						Name = entity.Owner.Name,
						ArtifactID = entity.Owner.ArtifactID
					}
				};
			}

			return resultDTO;
		}
	}
}
