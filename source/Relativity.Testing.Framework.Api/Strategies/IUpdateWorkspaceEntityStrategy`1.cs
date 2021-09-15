namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IUpdateWorkspaceEntityStrategy<T>
	{
		T Update(int workspaceId, T entity);
	}
}
