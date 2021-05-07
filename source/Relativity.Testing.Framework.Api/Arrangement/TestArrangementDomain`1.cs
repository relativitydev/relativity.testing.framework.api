using Relativity.Testing.Framework.Arrangement;
using Relativity.Testing.Framework.Session;

namespace Relativity.Testing.Framework.Api.Arrangement
{
	/// <summary>
	/// Represents the test arrangement domain of an entity.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	public class TestArrangementDomain<TEntity> : TestArrangementDomain
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TestArrangementDomain{TEntity}"/> class.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="context">The context.</param>
		public TestArrangementDomain(TEntity entity, TestArrangementContext context)
			: base(context)
		{
			Entity = entity;
		}

		/// <summary>
		/// Gets the domain entity.
		/// </summary>
		public TEntity Entity { get; }

		/// <summary>
		/// Picks the domain entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>The same domain.</returns>
		public TestArrangementDomain<TEntity> Pick(out TEntity entity)
		{
			entity = Entity;

			return this;
		}

		/// <summary>
		/// Sets the value whether to delete the domain entity on cleanup of <see cref="TestSession"/>.
		/// </summary>
		/// <param name="delete">Whether to delete.</param>
		/// <returns>The same domain.</returns>
		public TestArrangementDomain<TEntity> CleanUp(bool delete)
		{
			TestSession.Current.SetCleanUp(Entity, delete);

			return this;
		}
	}
}
