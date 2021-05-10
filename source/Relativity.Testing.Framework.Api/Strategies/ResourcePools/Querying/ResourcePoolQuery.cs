using Relativity.Testing.Framework.Api.Querying;

namespace Relativity.Testing.Framework.Api.Strategies
{
	public class ResourcePoolQuery<TObject> : Query<TObject, ResourcePoolQuery<TObject>>
	{
		internal ResourcePoolQuery(ResourcePoolQueryRequest request, IQueryExecutor<TObject> executor)
			: base(request, executor)
		{
		}

		private new ResourcePoolQueryRequest Request => (ResourcePoolQueryRequest)base.Request;

		/// <summary>
		/// Sets the workspace ID for which the query should be executed.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <returns>The query object.</returns>
		public ResourcePoolQuery<TObject> For(int workspaceId)
		{
			Request.WorkspaceId = workspaceId;

			return this;
		}
	}
}
