using System;
using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ChoiceGetAllByObjectFieldStrategy : IChoiceGetAllByObjectFieldStrategy
	{
		private readonly IObjectService _objectService;

		public ChoiceGetAllByObjectFieldStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public IEnumerable<Choice> GetAll(int workspaceId, string objectTypeName, string fieldName)
		{
			if (objectTypeName is null)
			{
				throw new ArgumentNullException(nameof(objectTypeName));
			}

			if (fieldName is null)
			{
				throw new ArgumentNullException(nameof(fieldName));
			}

			return _objectService.Query<Choice>().
				For(workspaceId).
				Where(x => x.ObjectType, objectTypeName).
				Where(x => x.Field, fieldName).
				ToArray();
		}
	}
}
