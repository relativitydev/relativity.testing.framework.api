using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IApplicationFieldCodeUpdateStrategy
	{
		ApplicationFieldCode Update(int workspaceId, ApplicationFieldCode applicationFieldCode);

		Task<ApplicationFieldCode> UpdateAsync(int workspaceId, ApplicationFieldCode applicationFieldCode);
	}
}
