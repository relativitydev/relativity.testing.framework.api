using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IItemGroupSelectorGetStrategy
	{
		Task<GroupSelector> GetAsync(int workspaceId, int itemId);
	}
}
