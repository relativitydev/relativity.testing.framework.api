using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IApplicationFieldCodeGetStrategy
	{
		ApplicationFieldCode Get(int workspaceId, int applicationFieldCodeId);
	}
}
