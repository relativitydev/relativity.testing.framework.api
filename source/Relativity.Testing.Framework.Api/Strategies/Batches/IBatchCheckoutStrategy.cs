namespace Relativity.Testing.Framework.Api.Strategies
{
	public interface IBatchCheckoutStrategy
	{
		void Checkout(int workspaceId, int batchId, int userId);
	}
}
