using System;
using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ObjectTypeCreateStrategy : CreateWorkspaceEntityStrategy<ObjectType>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<ObjectType> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<ObjectType> _getWorkspaceEntityByNameStrategy;

		public ObjectTypeCreateStrategy(
			IRestService restService,
			IGetWorkspaceEntityByIdStrategy<ObjectType> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<ObjectType> getWorkspaceEntityByNameStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
		}

		protected override ObjectType DoCreate(int workspaceId, ObjectType entity)
		{
			var entityToCreate = RequireObjectTypeFields(workspaceId, entity);

			var dto = new
			{
				ObjectTypeRequest = new
				{
					ParentObjectType = new
					{
						Secured = false,
						Value = new
						{
							entityToCreate.ParentObjectType.Value.ArtifactID,
							entityToCreate.ParentObjectType.Value.ArtifactTypeID
						}
					},
					entityToCreate.Name,
					entityToCreate.CopyInstancesOnParentCopy,
					entityToCreate.CopyInstancesOnCaseCreation,
					entityToCreate.EnableSnapshotAuditingOnDelete,
					entityToCreate.PersistentListsEnabled,
					entityToCreate.PivotEnabled,
					entityToCreate.SamplingEnabled,
					entityToCreate.RelativityApplications
				}
			};

			var artifactId = _restService.Post<int>($"relativity.objectTypes/workspace/{workspaceId}/objectTypes/", dto);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, artifactId);
		}

		private ObjectType RequireObjectTypeFields(int workspaceId, ObjectType entity)
		{
			var entityToCreate = entity.Copy();

			if (entityToCreate.ParentObjectType == null)
			{
				entityToCreate.ParentObjectType = new ObjectType.WrappedObjectType { Value = _getWorkspaceEntityByNameStrategy.Get(workspaceId, workspaceId == -1 ? "System" : "Document") };
			}
			else if (entityToCreate.ParentObjectType.Value == null)
			{
				entityToCreate.ParentObjectType.Value = _getWorkspaceEntityByNameStrategy.Get(workspaceId, workspaceId == -1 ? "System" : "Document");
			}
			else if (entityToCreate.ParentObjectType.Value.ArtifactID == 0 && entityToCreate.ParentObjectType.Value.Name != null)
			{
				entityToCreate.ParentObjectType = new ObjectType.WrappedObjectType { Value = _getWorkspaceEntityByNameStrategy.Get(workspaceId, entityToCreate.ParentObjectType.Value.Name) };
			}
			else if (entityToCreate.ParentObjectType.Value.ArtifactID == 0)
			{
				throw new ArgumentException("Entity should have indentifier ParentObjectType.", nameof(entity));
			}

			if (string.IsNullOrEmpty(entityToCreate.Name))
			{
				entityToCreate.Name = Randomizer.GetString("RTF ");
			}

			if (entityToCreate.RelativityApplications != null && entityToCreate.RelativityApplications.ViewableItems.Any())
			{
				entityToCreate.RelativityApplications.ViewableItems = entityToCreate.RelativityApplications.ViewableItems
					.Select(x => new NamedArtifact { ArtifactID = x.ArtifactID })
					.ToList();
			}

			return entityToCreate;
		}
	}
}
