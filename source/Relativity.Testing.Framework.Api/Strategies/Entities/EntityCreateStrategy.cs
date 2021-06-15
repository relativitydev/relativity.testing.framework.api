using System;
using System.Collections.Generic;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class EntityCreateStrategy : CreateWorkspaceEntityStrategy<Entity>
	{
		private readonly IObjectService _objectService;
		private readonly IChoiceResolveByObjectFieldAndNameStrategy _choiceResolveByObjectFieldAndNameStrategy;

		public EntityCreateStrategy(
			IObjectService objectService,
			IChoiceResolveByObjectFieldAndNameStrategy choiceResolveByObjectFieldAndNameStrategy)
		{
			_objectService = objectService;
			_choiceResolveByObjectFieldAndNameStrategy = choiceResolveByObjectFieldAndNameStrategy;
		}

		protected override Entity DoCreate(int workspaceId, Entity entity)
		{
			entity.FillRequiredProperties();

			ResolveEntityType(workspaceId, entity);

			ResolveClassification(workspaceId, entity);

			return _objectService.Create(workspaceId, entity);
		}

		private void ResolveEntityType(int workspaceId, Entity entity)
		{
			NamedArtifact type;

			if (entity.Type.ArtifactID != 0)
			{
				type = entity.Type;
			}
			else if (entity.Type.Name != null)
			{
				var artifactId = _choiceResolveByObjectFieldAndNameStrategy.ResolveReference(workspaceId, "Entity", "Type", entity.Type.Name).ArtifactID;
				type = new NamedArtifact { ArtifactID = artifactId };
			}
			else
			{
				throw new ArgumentException("Unable to get Type for a Custodian");
			}

			entity.Type = type;
		}

		private void ResolveClassification(int workspaceId, Entity entity)
		{
			if (entity.Classification != null)
			{
				List<NamedArtifact> classifications = new List<NamedArtifact>();

				foreach (var classification in entity.Classification)
				{
					if (classification.ArtifactID != 0)
					{
						classifications.Add(classification);
					}
					else if (classification.Name != null)
					{
						var artifactId = _choiceResolveByObjectFieldAndNameStrategy.ResolveReference(workspaceId, "Entity", "Classification", classification.Name).ArtifactID;
						classifications.Add(new NamedArtifact { ArtifactID = artifactId });
					}
					else
					{
						throw new ArgumentException("Unable to get Custodian Classification");
					}
				}

				entity.Classification = classifications;
			}
		}
	}
}
