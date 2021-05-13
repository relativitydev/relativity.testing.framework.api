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
							entityToCreate.ParentObjectType.ArtifactID,
							entityToCreate.ParentObjectType.ArtifactTypeID
						}
					},
					entityToCreate.Name,
					CopyInstancesOnParentCopy = entityToCreate.CopyInstanceOnParentCopy,
					CopyInstancesOnCaseCreation = entityToCreate.CopyInstanceOnWorkspaceCreation,
					entityToCreate.EnableSnapshotAuditingOnDelete,
					PersistentListsEnabled = entityToCreate.PersistentLists,
					PivotEnabled = entityToCreate.Pivot,
					SamplingEnabled = entityToCreate.Sampling,
					entityToCreate.UseRelativityForms,
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
				entityToCreate.ParentObjectType = _getWorkspaceEntityByNameStrategy.Get(workspaceId, workspaceId == -1 ? "System" : "Document");
			}
			else if (entityToCreate.ParentObjectType.ArtifactID == 0 && entityToCreate.ParentObjectType.Name != null)
			{
				entityToCreate.ParentObjectType = _getWorkspaceEntityByNameStrategy.Get(workspaceId, entityToCreate.ParentObjectType.Name);
			}
			else if (entityToCreate.ParentObjectType.ArtifactID == 0)
			{
				throw new ArgumentException("Entity should have indentifier ParentObjectType.", nameof(entity));
			}

			if (string.IsNullOrEmpty(entityToCreate.Name))
			{
				entityToCreate.Name = Randomizer.GetString("RTF ");
			}

			if (entityToCreate.RelativityApplications != null && entityToCreate.RelativityApplications.Any())
			{
				entityToCreate.RelativityApplications = entityToCreate.RelativityApplications
					.Select(x => new NamedArtifact { ArtifactID = x.ArtifactID })
					.ToArray();
			}

			return entityToCreate;
		}
	}
}
