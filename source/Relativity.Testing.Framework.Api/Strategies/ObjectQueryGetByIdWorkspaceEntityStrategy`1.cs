using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the base strategy class of getting the entity by Artifact ID using <see cref="IObjectService.Query{TObject}"/> method.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal abstract class ObjectQueryGetByIdWorkspaceEntityStrategy<T> : IGetWorkspaceEntityByIdStrategy<T>
		where T : Artifact
	{
		private readonly IObjectService _objectService;

		protected ObjectQueryGetByIdWorkspaceEntityStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		/// <summary>
		/// Gets the entity by the specified ID using <see cref="IObjectService.Query{TObject}"/> method.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The entity ID.</param>
		/// <returns>
		/// The entity or <see langword="null" />.
		/// </returns>
		public T Get(int workspaceId, int entityId)
		{
			return _objectService.Query<T>().For(workspaceId).Where(x => x.ArtifactID, entityId).FirstOrDefault();
		}
	}
}
