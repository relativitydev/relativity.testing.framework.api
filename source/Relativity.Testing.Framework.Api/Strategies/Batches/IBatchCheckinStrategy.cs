namespace Relativity.Testing.Framework.Api.Strategies
{
	public interface IBatchCheckinStrategy
	{
		void Checkin(int workspaceId, int batchId, bool isCompleted);
	}
}
