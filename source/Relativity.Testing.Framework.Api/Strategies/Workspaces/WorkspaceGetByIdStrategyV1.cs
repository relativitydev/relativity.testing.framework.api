using System;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.DTO;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class WorkspaceGetByIdStrategyV1 : IGetByIdStrategy<Workspace>
	{
		private readonly IRestService _restService;

		public WorkspaceGetByIdStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public Workspace Get(int id)
		{
			var response = _restService.Get<WorkspaceDTO>($"relativity-environment/V1/workspace/{id}");

			return ConvertFromDTO(response);
		}

		private Workspace ConvertFromJson(JObject response)
		{
			throw new NotImplementedException();
		}

		private Workspace ConvertFromDTO(WorkspaceDTO dto)
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
				Notes = dto.Notes
			};

			if (dto.Template != null)
			{
				result.TemplateWorkspace = dto.Template.Value;
			}

			if (dto.ProductionRestrictions != null)
			{
				result.ProductionRestrictions = dto.ProductionRestrictions.Value;
			}

			if (dto.DataGridFileRepository != null)
			{
				result.DataGridFileRepository = dto.DataGridFileRepository.Value;
			}

			if (dto.WorkspaceAdminGroup != null)
			{
				result.WorkspaceAdminGroup = dto.WorkspaceAdminGroup.Value;
			}

			return result;
		}
	}
}
