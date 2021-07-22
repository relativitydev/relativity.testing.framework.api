using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ApplicationFieldCodeUpdateStrategyNotSupported : IApplicationFieldCodeUpdateStrategy
	{
		public ApplicationFieldCode Update(int workspaceId, ApplicationFieldCode applicationFieldCode)
		{
			throw new ArgumentException("The method Update ApplicationFieldCode does not support version of Relativity lower than 12.1.");
		}

		public Task<ApplicationFieldCode> UpdateAsync(int workspaceId, ApplicationFieldCode applicationFieldCode)
		{
			throw new ArgumentException("The method Update ApplicationFieldCode does not support version of Relativity lower than 12.1.");
		}
	}
}
