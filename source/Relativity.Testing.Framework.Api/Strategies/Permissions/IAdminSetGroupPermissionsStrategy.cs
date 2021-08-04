using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IAdminSetGroupPermissionsStrategy
	{
		Task SetAsync(GroupPermissions groupPermissions);
	}
}
