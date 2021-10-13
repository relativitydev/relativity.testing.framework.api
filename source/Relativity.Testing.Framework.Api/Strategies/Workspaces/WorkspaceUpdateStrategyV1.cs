using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class WorkspaceUpdateStrategyV1 : IUpdateStrategy<Workspace>
	{
		private readonly IRestService _restService;

		public WorkspaceUpdateStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public void Update(Workspace entity)
		{
			if (entity is null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			_restService.Put($"relativity-environment/V1/workspace/{entity.ArtifactID}", BuildRequest(entity));
		}

		private object BuildRequest(Workspace entity)
		{
			return new
			{
				WorkspaceRequest = new
				{
					Name = entity.Name,
					Matter = new Securable<Artifact>(new Artifact(entity.Matter.ArtifactID)),
					DefaultCacheLocation = new Securable<Artifact>(entity.DefaultCacheLocation),
					DefaultFileRepository = new Securable<Artifact>(entity.DefaultFileRepository),
					DownloadHandlerUrl = entity.DownloadHandlerUrl,
					EnableDataGrid = false,
					entity.Keywords,
					entity.Notes,
					ResourcePool = new Securable<Artifact>(entity.ResourcePool),
					SqlFullTextLanguage = entity.SqlFullTextLanguage,
					SqlServer = new Securable<Artifact>(entity.SqlServer),
					Status = new Artifact(675),
					WorkspaceAdminGroup = entity.WorkspaceAdminGroup == null ? null : new Securable<Artifact>(entity.WorkspaceAdminGroup)
				} // Template has to be left as null due to API requirements (https://platform.relativity.com/RelativityOne/Content/BD_Environment/Workspace_Manager_service.htm#_Update_a_workspace).
			};
		}
	}
}
