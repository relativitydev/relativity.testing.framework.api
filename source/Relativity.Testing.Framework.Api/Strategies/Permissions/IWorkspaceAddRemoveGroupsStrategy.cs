using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IWorkspaceAddRemoveGroupsStrategy
	{
		Task AddRemoveWorkspaceGroupsAsync(int workspaceId, GroupSelector selector);
	}
}
