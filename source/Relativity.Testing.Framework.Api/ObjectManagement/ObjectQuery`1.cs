using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Api.Querying;

namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	/// <summary>
	/// Represents the object query.
	/// </summary>
	/// <typeparam name="TObject">The type of the object.</typeparam>
	public class ObjectQuery<TObject> : Query<TObject, ObjectQuery<TObject>>
	{
		internal ObjectQuery(ObjectQueryRequest request, IQueryExecutor<TObject> executor)
			: base(request, executor)
		{
		}

		private new ObjectQueryRequest Request => (ObjectQueryRequest)base.Request;

		/// <summary>
		/// Sets the workspace ID for which the query should be executed.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <returns>The query object.</returns>
		public ObjectQuery<TObject> For(int workspaceId)
		{
			Request.WorkspaceId = workspaceId;

			return this;
		}

		/// <summary>
		/// Sets the user credentials with which the query should be executed.
		/// </summary>
		/// <param name="userCredentials">User credentials.</param>
		/// <returns>The query object.</returns>
		public ObjectQuery<TObject> With(UserCredentials userCredentials)
		{
			Request.UserCredentials = userCredentials;

			return this;
		}
	}
}
