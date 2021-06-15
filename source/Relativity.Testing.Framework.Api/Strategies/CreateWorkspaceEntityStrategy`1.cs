using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Session;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents base strategy of entity creation of workspace level.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal abstract class CreateWorkspaceEntityStrategy<T> : ICreateWorkspaceEntityStrategy<T>
		where T : Artifact
	{
		/// <summary>
		/// Creates the specified entity.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		public T Create(int workspaceId, T entity)
		{
			if (entity is null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			T createdEntity = DoCreate(workspaceId, entity);

			TestSession.Current?.Add(workspaceId, createdEntity);

			return createdEntity;
		}

		/// <summary>
		/// Does create the specified entity.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		protected abstract T DoCreate(int workspaceId, T entity);
	}
}
