using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.DTO
{
	internal static class ViewDTOMapper
	{
		internal static ViewDTO MapToDTO(this View entity)
		{
			ViewDTO resultDTO = new ViewDTO
			{
				ArtifactID = entity.ArtifactID,
				ArtifactTypeID = entity.ArtifactTypeId,
				Order = entity.Order,
				VisibleInDropdown = entity.VisibleInDropdown,
				QueryHint = entity.QueryHint,
				Name = entity.Name,
				Fields = entity.Fields,
				Sorts = entity.Sorts,
				Dashboard = entity.Dashboard,
				RelativityApplicationsList = entity.RelativityApplications,
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
