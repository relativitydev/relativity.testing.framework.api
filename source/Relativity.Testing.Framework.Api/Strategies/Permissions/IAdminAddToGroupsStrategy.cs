using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IAdminAddToGroupsStrategy
	{
		Task AddToGroupsAsync(params int[] groupIds);

		Task AddToGroupsAsync(params string[] groupNames);
	}
}
