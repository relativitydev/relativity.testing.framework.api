using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Arrangement;

namespace Relativity.Testing.Framework.Api.Arrangement
{
	/// <summary>
	/// Represents the factory that can create generic entities of workspace level.
	/// </summary>
	public class WorkspaceLevelEntityCreator : IGenericEntityCreator
	{
		private readonly IRelativityFacade _facade;

		private readonly int _workspaceId;

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkspaceLevelEntityCreator"/> class.
		/// </summary>
		/// <param name="facade">The facade.</param>
		/// <param name="workspaceId">The workspace identifier.</param>
		public WorkspaceLevelEntityCreator(IRelativityFacade facade, int workspaceId)
		{
			_facade = facade;
			_workspaceId = workspaceId;
		}

		/// <summary>
		/// Creates the specified entity.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="entity">The entity.</param>
		/// <returns>The created entity.</returns>
		public TEntity Create<TEntity>(TEntity entity)
		{
			var strategy = _facade.Resolve<ICreateWorkspaceEntityStrategy<TEntity>>();

			return strategy.Create(_workspaceId, entity);
		}
	}
}
