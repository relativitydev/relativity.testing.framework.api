using Relativity.Testing.Framework.Arrangement;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Arrangement
{
	/// <summary>
	/// Represents the factory that can create generic entities of admin level.
	/// </summary>
	public class AdminLevelEntityCreator : IGenericEntityCreator
	{
		private readonly IRelativityFacade _facade;

		/// <summary>
		/// Initializes a new instance of the <see cref="AdminLevelEntityCreator"/> class.
		/// </summary>
		/// <param name="facade">The facade.</param>
		public AdminLevelEntityCreator(IRelativityFacade facade)
		{
			_facade = facade;
		}

		/// <summary>
		/// Creates the specified entity.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="entity">The entity.</param>
		/// <returns>The created entity.</returns>
		public TEntity Create<TEntity>(TEntity entity)
		{
			var strategy = _facade.Resolve<ICreateStrategy<TEntity>>();

			return strategy.Create(entity);
		}
	}
}
