using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IAdminGetGroupPermissionsStrategy
	{
		Task<GroupPermissions> GetAsync(int groupId);

		Task<GroupPermissions> GetAsync(string groupName);
	}
}
