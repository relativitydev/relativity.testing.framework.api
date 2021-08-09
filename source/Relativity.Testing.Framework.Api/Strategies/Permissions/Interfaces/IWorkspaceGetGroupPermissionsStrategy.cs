using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IWorkspaceGetGroupPermissionsStrategy
	{
		Task<GroupPermissions> GetAsync(int workspaceId, int groupId);

		Task<GroupPermissions> GetAsync(int workspaceId, string groupName);
	}
}
