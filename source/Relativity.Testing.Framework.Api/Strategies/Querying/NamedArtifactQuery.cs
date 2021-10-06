using Relativity.Testing.Framework.Api.Querying;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class NamedArtifactQuery<TObject> : Query<TObject, NamedArtifactQuery<TObject>>
	{
		internal NamedArtifactQuery(NamedArtifactQueryRequest request, IQueryExecutor<TObject> executor)
			: base(request, executor)
		{
		}
	}
}
