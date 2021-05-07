using System;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MarkupSetUpdateStrategy : IUpdateWorkspaceEntityStrategy<MarkupSet>
	{
		private readonly IObjectService _objectService;

		public MarkupSetUpdateStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		void IUpdateWorkspaceEntityStrategy<MarkupSet>.Update(int workspaceId, MarkupSet entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			_objectService.Update(workspaceId, entity);
		}
	}
}
