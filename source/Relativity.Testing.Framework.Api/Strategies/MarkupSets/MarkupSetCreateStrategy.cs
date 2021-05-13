using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MarkupSetCreateStrategy : CreateWorkspaceEntityStrategy<MarkupSet>
	{
		private readonly IObjectService _objectService;

		public MarkupSetCreateStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		protected override MarkupSet DoCreate(int workspaceId, MarkupSet entity)
		{
			entity.FillRequiredProperties();

			return _objectService.Create(workspaceId, entity);
		}
	}
}
