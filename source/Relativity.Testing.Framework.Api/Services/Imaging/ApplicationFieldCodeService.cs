using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services.Imaging
{
	internal class ApplicationFieldCodeService : IApplicationFieldCodeService
	{
		private readonly IApplicationFieldCodeGetStrategy _applicationFieldCodeGetStrategy;
		private readonly IApplicationFieldCodeCreateStrategy _applicationFieldCodeCreateStrategy;
		private readonly IApplicationFieldCodeUpdateStrategy _applicationFieldCodeUpdateStrategy;
		private readonly IApplicationFieldCodeDeleteStrategy _applicationFieldCodeDeleteStrategy;

		public ApplicationFieldCodeService(
				IApplicationFieldCodeGetStrategy applicationFieldCodeGetStrategy,
				IApplicationFieldCodeCreateStrategy applicationFieldCodeCreateStrategy,
				IApplicationFieldCodeUpdateStrategy applicationFieldCodeUpdateStrategy,
				IApplicationFieldCodeDeleteStrategy applicationFieldCodeDeleteStrategy)
		{
			_applicationFieldCodeGetStrategy = applicationFieldCodeGetStrategy;
			_applicationFieldCodeCreateStrategy = applicationFieldCodeCreateStrategy;
			_applicationFieldCodeUpdateStrategy = applicationFieldCodeUpdateStrategy;
			_applicationFieldCodeDeleteStrategy = applicationFieldCodeDeleteStrategy;
		}

		public ApplicationFieldCode Create(int workspaceId, ApplicationFieldCode applicationFieldCode)
			=> _applicationFieldCodeCreateStrategy.Create(workspaceId, applicationFieldCode);

		public void Delete(int workspaceId, int applicationFieldCodeId)
			=> _applicationFieldCodeDeleteStrategy.Delete(workspaceId, applicationFieldCodeId);

		public ApplicationFieldCode Get(int workspaceId, int applicationFieldCodeId)
			=> _applicationFieldCodeGetStrategy.Get(workspaceId, applicationFieldCodeId);

		public ApplicationFieldCode Update(int workspaceId, ApplicationFieldCode applicationFieldCode)
			=> _applicationFieldCodeUpdateStrategy.Update(workspaceId, applicationFieldCode);
	}
}
