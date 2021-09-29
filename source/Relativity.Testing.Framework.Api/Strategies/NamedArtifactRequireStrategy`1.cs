using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the base strategy class of entity requirement for entities inherited from [NamedArtifact](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.NamedArtifact.html).
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal abstract class NamedArtifactRequireStrategy<T> : IRequireStrategy<T>
		where T : NamedArtifact, new()
	{
		private readonly ICreateStrategy<T> _createStrategy;

		private readonly IGetByNameStrategy<T> _getByNameStrategy;

		private readonly IGetByIdStrategy<T> _getByIdStrategy;

		protected NamedArtifactRequireStrategy(
			ICreateStrategy<T> createStrategy,
			IGetByNameStrategy<T> getByNameStrategy,
			IGetByIdStrategy<T> getByIdStrategy)
		{
			_createStrategy = createStrategy;
			_getByNameStrategy = getByNameStrategy;
			_getByIdStrategy = getByIdStrategy;
		}

		/// <summary>
		/// Requires the specified entity.
		/// <list type="number">
		/// <item>If [Artifact.ArtifactID](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Artifact.html#Relativity_Testing_Framework_Models_Artifact_ArtifactID) property of <paramref name="entity"/> has positive value, gets entity by ID using <see cref="IGetByIdStrategy{T}"/> and returns it.</item>
		/// <item>If [NamedArtifact.Name](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.NamedArtifact.html#Relativity_Testing_Framework_Models_NamedArtifact_Name) property of <paramref name="entity"/> has a value, gets entity by name using <see cref="IGetByNameStrategy{T}"/> and returns it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <param name="entity">The entity to require.</param>
		/// <returns>The entity required.</returns>
		public T Require(T entity)
		{
			if (entity?.ArtifactID > 0)
			{
				return GetById(entity.ArtifactID);
			}

			if (!string.IsNullOrEmpty(entity?.Name) && TryGetByName(entity.Name, out T gotEntity))
			{
				return gotEntity;
			}

			return Create(entity);
		}

		private T GetById(int id)
		{
			return _getByIdStrategy.Get(id)
				?? throw new ObjectNotFoundException($"Failed to find {typeof(T).Name} entity by {id} ID.");
		}

		private bool TryGetByName(string name, out T entity)
		{
			entity = _getByNameStrategy.Get(name);
			return entity != null;
		}

		private T Create(T entity)
		{
			return _createStrategy.Create(entity ?? new T());
		}
	}
}
