using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class FieldService : IFieldService
	{
		private readonly IRelativityFacade _facade;
		private readonly IDeleteWorkspaceEntityByIdStrategy<Field> _deleteWorkspaceEntityByIdStrategy;

		public FieldService(
			IRelativityFacade facade,
			IDeleteWorkspaceEntityByIdStrategy<Field> deleteWorkspaceEntityByIdStrategy)
		{
			_facade = facade;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
		}

		public TFieldModel Create<TFieldModel>(int workspaceId, TFieldModel entity)
			where TFieldModel : Field
		{
			return _facade.Resolve<ICreateWorkspaceEntityStrategy<TFieldModel>>().
				Create(workspaceId, entity);
		}

		public void Delete(int workspaceId, int entityId)
			=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceId, entityId);

		public Field Get(int workspaceId, int entityId)
		{
			return _facade.Resolve<IGetWorkspaceEntityByIdStrategy<Field>>().
				Get(workspaceId, entityId);
		}

		public TFieldModel Get<TFieldModel>(int workspaceId, int entityId)
			where TFieldModel : Field
		{
			return _facade.Resolve<IGetWorkspaceEntityByIdStrategy<TFieldModel>>().
				Get(workspaceId, entityId);
		}

		public Field Get(int workspaceId, string entityName)
		{
			return _facade.Resolve<IGetWorkspaceEntityByNameStrategy<Field>>().
				Get(workspaceId, entityName);
		}

		public TFieldModel Get<TFieldModel>(int workspaceId, string entityName)
			where TFieldModel : Field
		{
			return _facade.Resolve<IGetWorkspaceEntityByNameStrategy<TFieldModel>>().
				Get(workspaceId, entityName);
		}

		public void Update<TFieldModel>(int workspaceId, TFieldModel entity)
			where TFieldModel : Field
		{
			_facade.Resolve<IUpdateWorkspaceEntityStrategy<TFieldModel>>().
				Update(workspaceId, entity);
		}

		public TFieldModel Require<TFieldModel>(int workspaceId, TFieldModel entity)
			where TFieldModel : Field
		{
			return _facade.Resolve<IRequireWorkspaceEntityStrategy<TFieldModel>>().
				Require(workspaceId, entity);
		}
	}
}
