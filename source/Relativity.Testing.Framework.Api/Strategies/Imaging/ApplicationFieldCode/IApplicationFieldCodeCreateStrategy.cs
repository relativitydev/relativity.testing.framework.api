using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IApplicationFieldCodeCreateStrategy
	{
		ApplicationFieldCode Create(int workspaceId, ApplicationFieldCode applicationFieldCode);
	}
}
