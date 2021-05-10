using System;
using System.Linq;
using System.Text;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ChoiceResolveByObjectFieldAndNameStrategy : IChoiceResolveByObjectFieldAndNameStrategy
	{
		private readonly IObjectService _objectService;

		public ChoiceResolveByObjectFieldAndNameStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public ArtifactReference ResolveReference(int workspaceId, string objectTypeName, string fieldName, string choiceName)
		{
			if (objectTypeName is null)
			{
				throw new ArgumentNullException(nameof(objectTypeName));
			}

			if (fieldName is null)
			{
				throw new ArgumentNullException(nameof(fieldName));
			}

			if (choiceName is null)
			{
				throw new ArgumentNullException(nameof(choiceName));
			}

			ArtifactReference reference = _objectService.Query<Choice>().
				For(workspaceId).
				Where(x => x.ObjectType, objectTypeName).
				Where(x => x.Field, fieldName).
				Where(x => x.Name, choiceName).
				FetchOnlyArtifactID().
				Select(x => new ArtifactReference(x.ArtifactID)).
				FirstOrDefault();

			return reference ?? throw new ObjectNotFoundException(new StringBuilder().
				AppendLine($"The choice object is not found by:").
				AppendLine($" - Workspace ID: {workspaceId}").
				AppendLine($" - Object type name: {objectTypeName}").
				AppendLine($" - Field name: {fieldName}").
				Append($" - Choice name: {choiceName}").
				ToString());
		}

		public ArtifactReference ResolveReference(string objectTypeName, string fieldName, string choiceName)
		{
			return ResolveReference(-1, objectTypeName, fieldName, choiceName);
		}
	}
}
