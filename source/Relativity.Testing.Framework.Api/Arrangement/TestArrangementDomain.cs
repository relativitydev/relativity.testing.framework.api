using System;
using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Arrangement;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Session;

namespace Relativity.Testing.Framework.Api.Arrangement
{
	/// <summary>
	/// Represents the domain (root) of test arrangement functionality.
	/// </summary>
	public class TestArrangementDomain
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TestArrangementDomain"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public TestArrangementDomain(TestArrangementContext context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
		}

		/// <summary>
		/// Gets the current test arrangement context.
		/// </summary>
		public TestArrangementContext Context { get; }

		/// <summary>
		/// Creates the entity of specified type.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <returns>The <see cref="TestArrangementDomain{TEntity}"/> for created entity.</returns>
		public TestArrangementDomain<TEntity> Create<TEntity>()
			where TEntity : new()
		{
			TEntity entity = new TEntity();

			return Create(entity);
		}

		/// <summary>
		/// Creates the entity of specified type and sets it to <paramref name="entity"/> out parameter.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="entity">The created enity to set to.</param>
		/// <returns>The <see cref="TestArrangementDomain{TEntity}"/> for created entity.</returns>
		public TestArrangementDomain<TEntity> Create<TEntity>(out TEntity entity)
			where TEntity : new()
		{
			var entityDomain = Create<TEntity>();

			entity = entityDomain.Entity;

			return entityDomain;
		}

		/// <summary>
		/// Creates the specified entity.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The <see cref="TestArrangementDomain{TEntity}"/> for created entity.</returns>
		public TestArrangementDomain<TEntity> Create<TEntity>(TEntity entity)
		{
			TEntity resultEntity = Context.EntityCreator.Create(entity);

			return For(resultEntity);
		}

		/// <summary>
		/// Creates multiple entities of the specified type.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="count">The count of entities.</param>
		/// <returns>The <see cref="TestArrangementDomain{TEntity}"/> for created entities enumerable.</returns>
		public TestArrangementDomainOfEnumerable<TEntity> Create<TEntity>(int count)
			where TEntity : new()
		{
			return Create(count, new TEntity());
		}

		/// <summary>
		/// Creates multiple entities of the specified type.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="count">The count of entities.</param>
		/// <param name="entities">The enumerable to assign the created entities.</param>
		/// <returns>The <see cref="TestArrangementDomain{TEntity}" /> for created entities enumerable.</returns>
		public TestArrangementDomainOfEnumerable<TEntity> Create<TEntity>(int count, out IEnumerable<TEntity> entities)
			where TEntity : new()
		{
			return Create<TEntity>(count).PickAll(out entities);
		}

		/// <summary>
		/// Creates multiple entities of the specified type.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="count">The count of entities.</param>
		/// <param name="entities">The list to assign the created entities.</param>
		/// <returns>The <see cref="TestArrangementDomain{TEntity}" /> for created entities enumerable.</returns>
		public TestArrangementDomainOfEnumerable<TEntity> Create<TEntity>(int count, out List<TEntity> entities)
			where TEntity : new()
		{
			return Create<TEntity>(count).PickAll(out entities);
		}

		/// <summary>
		/// Creates multiple entities of the specified type.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="count">The count of entities.</param>
		/// <param name="entities">The array to assign the created entities.</param>
		/// <returns>The <see cref="TestArrangementDomain{TEntity}" /> for created entities enumerable.</returns>
		public TestArrangementDomainOfEnumerable<TEntity> Create<TEntity>(int count, out TEntity[] entities)
			where TEntity : new()
		{
			return Create<TEntity>(count).PickAll(out entities);
		}

		/// <summary>
		/// Creates multiple entities of the specified type using entity as template.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="count">The count of entities.</param>
		/// <param name="entity">The entity template.</param>
		/// <returns>The <see cref="TestArrangementDomain{TEntity}" /> for created entities enumerable.</returns>
		public TestArrangementDomainOfEnumerable<TEntity> Create<TEntity>(int count, TEntity entity)
		{
			if (count <= 0)
			{
				throw new ArgumentException($"'{nameof(count)}' should be positive value.", nameof(count));
			}

			var entities = Enumerable.Repeat(entity, count).Select(x => (TEntity)x.Copy());

			return Create(entities);
		}

		/// <summary>
		/// Creates the specified entities.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="entities">The entities to create.</param>
		/// <returns>The <see cref="TestArrangementDomain{TEntity}" /> for created entities enumerable.</returns>
		public TestArrangementDomainOfEnumerable<TEntity> Create<TEntity>(IEnumerable<TEntity> entities)
		{
			if (entities is null)
			{
				throw new ArgumentNullException(nameof(entities));
			}

			if (!entities.Any())
			{
				throw new ArgumentException($"'{nameof(entities)}' should not be an empty collection.", nameof(entities));
			}

			TEntity[] createdEntities = entities.
				Select(x => Context.EntityCreator.Create(x)).
				ToArray();

			return new TestArrangementDomainOfEnumerable<TEntity>(createdEntities, Context);
		}

		/// <summary>
		/// Creates the domain for working workspace.
		/// Get the working workspace using the <see cref="TestSession.Working{TEntity}"/> method.
		/// </summary>
		/// <returns>The <see cref="TestArrangementDomain{TEntity}" /> for working workspace.</returns>
		public TestArrangementDomain<Workspace> ForWorkingWorkspace()
		{
			return ForWorking<Workspace>();
		}

		/// <summary>
		/// Creates the domain for working entity of specified type.
		/// Get the working entity using the <see cref="TestSession.Working{TEntity}"/> method.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <returns>The <see cref="TestArrangementDomain{TEntity}" /> for working entity.</returns>
		public TestArrangementDomain<TEntity> ForWorking<TEntity>()
		{
			TEntity entity = Context.Session.Working<TEntity>();

			return For(entity);
		}

		/// <summary>
		/// Creates the domain for the specified entity.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="entity">The entity.</param>
		/// <returns>The <see cref="TestArrangementDomain{TEntity}" /> for entity.</returns>
		public TestArrangementDomain<TEntity> For<TEntity>(TEntity entity)
		{
			return new TestArrangementDomain<TEntity>(entity, Context);
		}
	}
}
