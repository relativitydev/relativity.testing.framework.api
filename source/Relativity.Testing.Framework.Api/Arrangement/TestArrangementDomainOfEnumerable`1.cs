using System;
using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Arrangement;
using Relativity.Testing.Framework.Session;

namespace Relativity.Testing.Framework.Api.Arrangement
{
	/// <summary>
	/// Represents the test arrangement domain of an entity enumerable.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	public class TestArrangementDomainOfEnumerable<TEntity> : TestArrangementDomain
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TestArrangementDomainOfEnumerable{TEntity}"/> class.
		/// </summary>
		/// <param name="entities">The entities.</param>
		/// <param name="context">The context.</param>
		/// <exception cref="ArgumentNullException"><paramref name="entities"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="entities"/> is an empty collection.</exception>
		public TestArrangementDomainOfEnumerable(IEnumerable<TEntity> entities, TestArrangementContext context)
			: base(context)
		{
			if (entities is null)
			{
				throw new ArgumentNullException(nameof(entities));
			}

			if (!entities.Any())
			{
				throw new ArgumentException($"'{nameof(entities)}' should not be an empty collection.", nameof(entities));
			}

			Entities = entities;
		}

		/// <summary>
		/// Gets the domain entities.
		/// </summary>
		public IEnumerable<TEntity> Entities { get; }

		/// <summary>
		/// Picks the first entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>The same domain.</returns>
		public TestArrangementDomainOfEnumerable<TEntity> PickFirst(out TEntity entity)
		{
			entity = Entities.First();

			return this;
		}

		/// <summary>
		/// Picks the last entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>The same domain.</returns>
		public TestArrangementDomainOfEnumerable<TEntity> PickLast(out TEntity entity)
		{
			entity = Entities.Last();

			return this;
		}

		/// <summary>
		/// Picks the middle entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>The same domain.</returns>
		public TestArrangementDomainOfEnumerable<TEntity> PickMiddle(out TEntity entity)
		{
			int entitiesCount = Entities.Count();

			entity = entitiesCount > 2 ? Entities.ElementAt(entitiesCount / 2) : Entities.First();

			return this;
		}

		/// <summary>
		/// Picks any random entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>The same domain.</returns>
		public TestArrangementDomainOfEnumerable<TEntity> PickAny(out TEntity entity)
		{
			int randomIndex = Randomizer.GetInt(Entities.Count());

			entity = Entities.ElementAt(randomIndex);

			return this;
		}

		/// <summary>
		/// Picks all entities.
		/// </summary>
		/// <param name="entities">The entities.</param>
		/// <returns>The same domain.</returns>
		public TestArrangementDomainOfEnumerable<TEntity> PickAll(out IEnumerable<TEntity> entities)
		{
			entities = Entities;

			return this;
		}

		/// <summary>
		/// Picks all entities.
		/// </summary>
		/// <param name="entities">The entities.</param>
		/// <returns>The same domain.</returns>
		public TestArrangementDomainOfEnumerable<TEntity> PickAll(out List<TEntity> entities)
		{
			entities = Entities.ToList();

			return this;
		}

		/// <summary>
		/// Picks all entities.
		/// </summary>
		/// <param name="entities">The entities.</param>
		/// <returns>The same domain.</returns>
		public TestArrangementDomainOfEnumerable<TEntity> PickAll(out TEntity[] entities)
		{
			entities = Entities.ToArray();

			return this;
		}

		/// <summary>
		/// Sets the value whether to delete the entities on cleanup of <see cref="TestSession"/>.
		/// </summary>
		/// <param name="delete">Whether to delete.</param>
		/// <returns>The same domain.</returns>
		public TestArrangementDomainOfEnumerable<TEntity> CleanUp(bool delete)
		{
			TestSession.Current.SetCleanUp(Entities.Cast<object>(), delete);

			return this;
		}
	}
}
