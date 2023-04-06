using Relativity.Testing.Framework.Api.DTO;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Extensions
{
	internal static class WorkspaceDTOExtensions
	{
		public static Workspace MapToWorkspace(this WorkspaceDTO dto)
		{
			var result = new Workspace
			{
				ArtifactID = dto.ArtifactID,
				Name = dto.Name,
				Client = dto.Client.Value,
				Matter = dto.Matter.Value,
				Status = dto.Status.Name,
				ResourcePool = dto.ResourcePool.Value,
				SqlServer = dto.SqlServer.Value,
				DefaultFileRepository = dto.DefaultFileRepository.Value,
				DefaultCacheLocation = dto.DefaultCacheLocation.Value,
				DownloadHandlerUrl = dto.DownloadHandlerUrl,
				SqlFullTextLanguage = (SqlFullTextLanguage)dto.SqlFullTextLanguage.ID,
				Keywords = dto.Keywords,
				Notes = dto.Notes,
				TemplateWorkspace = dto.Template?.Value,
				ProductionRestrictions = dto.ProductionRestrictions?.Value,
				DataGridFileRepository = dto.DataGridFileRepository?.Value,
				WorkspaceAdminGroup = dto.WorkspaceAdminGroup?.Value
			};

			return result;
		}
	}
}
