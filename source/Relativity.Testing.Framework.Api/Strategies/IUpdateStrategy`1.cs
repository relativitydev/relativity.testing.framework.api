namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IUpdateStrategy<T>
	{
		T Update(T entity);
	}
}
