using System;
using System.Linq;
using System.Linq.Expressions;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchesQueryStrategy : IBatchQueryStrategy
	{
		private readonly IObjectService _objectService;

		public BatchesQueryStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Batch[] Query(int workspaceId, Expression<Func<Batch, object>> wherePropertySelector, object whereValue)
		{
			return _objectService.Query<Batch>().
				For(workspaceId).
				Where(wherePropertySelector, whereValue).
				ToArray();
		}
	}
}
