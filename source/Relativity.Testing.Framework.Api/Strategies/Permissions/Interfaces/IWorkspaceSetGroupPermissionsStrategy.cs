using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IWorkspaceSetGroupPermissionsStrategy
	{
		Task SetAsync(int workspaceId, GroupPermissions groupPermissions);
	}
}
