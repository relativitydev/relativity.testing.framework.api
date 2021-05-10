using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ObjectTypeUpdateStrategy : IUpdateWorkspaceEntityStrategy<ObjectType>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<ObjectType> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<ObjectType> _getWorkspaceEntityByNameStrategy;

		public ObjectTypeUpdateStrategy(
			IRestService restService,
			IGetWorkspaceEntityByIdStrategy<ObjectType> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<ObjectType> getWorkspaceEntityByNameStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
		}

		public void Update(int workspaceId, ObjectType entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			var dto = new
			{
				ObjectTypeRequest = new
				{
					entity.Name,
					CopyInstancesOnParentCopy = entity.CopyInstanceOnParentCopy,
					CopyInstancesOnCaseCreation = entity.CopyInstanceOnWorkspaceCreation,
					entity.EnableSnapshotAuditingOnDelete,
					PersistentListsEnabled = entity.PersistentLists,
					PivotEnabled = entity.Pivot,
					SamplingEnabled = entity.Sampling,
					entity.UseRelativityForms,
					entity.RelativityApplications
				}
			};

			_restService.Put($"relativity.objectTypes/workspace/{workspaceId}/objectTypes/{entity.ArtifactID}", dto);
		}
	}
}
