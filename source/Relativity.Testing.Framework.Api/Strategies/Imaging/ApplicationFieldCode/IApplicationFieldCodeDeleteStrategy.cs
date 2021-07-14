using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IApplicationFieldCodeDeleteStrategy
	{
		void Delete(int workspaceId, int applicationFieldCodeId);

		Task DeleteAsync(int workspaceId, int applicationFieldCodeId);
	}
}
