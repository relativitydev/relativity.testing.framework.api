using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IItemLevelSecuritySetStrategy
	{
		Task SetAsync(int workspaceId, int itemId, bool isEnabled);
	}
}
