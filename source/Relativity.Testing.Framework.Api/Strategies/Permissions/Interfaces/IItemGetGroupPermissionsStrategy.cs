using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IItemGetGroupPermissionsStrategy
	{
		Task<GroupPermissions> GetAsync(int workspaceId, int itemId, int groupId);
	}
}
