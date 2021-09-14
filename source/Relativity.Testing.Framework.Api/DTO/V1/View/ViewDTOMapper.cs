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
				Owner = new SecuredValueDTO
				{
					Value = new NamedArtifactWithGuids
					{
						Name = entity.Owner.Name,
						ArtifactID = entity.Owner.ArtifactID
					}
				},
				Name = entity.Name,
				Fields = entity.Fields,
				Sorts = entity.Sorts,
				Dashboard = entity.Dashboard,
				GroupDefinitionFieldArtifactID = entity.GroupDefinitionFieldArtifactId,
				SearchCriteria = entity.SearchCriteria
			};
			return resultDTO;
		}
	}
}
