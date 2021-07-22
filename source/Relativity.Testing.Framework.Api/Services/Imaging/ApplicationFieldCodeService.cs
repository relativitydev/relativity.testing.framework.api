using System.Threading.Tasks;
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

		public async Task<ApplicationFieldCode> CreateAsync(int workspaceId, ApplicationFieldCode applicationFieldCode)
			=> await _applicationFieldCodeCreateStrategy.CreateAsync(workspaceId, applicationFieldCode).ConfigureAwait(false);

		public void Delete(int workspaceId, int applicationFieldCodeId)
			=> _applicationFieldCodeDeleteStrategy.Delete(workspaceId, applicationFieldCodeId);

		public async Task DeleteAsync(int workspaceId, int applicationFieldCodeId)
			=> await _applicationFieldCodeDeleteStrategy.DeleteAsync(workspaceId, applicationFieldCodeId).ConfigureAwait(false);

		public ApplicationFieldCode Get(int workspaceId, int applicationFieldCodeId)
			=> _applicationFieldCodeGetStrategy.Get(workspaceId, applicationFieldCodeId);

		public async Task<ApplicationFieldCode> GetAsync(int workspaceId, int applicationFieldCodeId)
			=> await _applicationFieldCodeGetStrategy.GetAsync(workspaceId, applicationFieldCodeId).ConfigureAwait(false);

		public ApplicationFieldCode Update(int workspaceId, ApplicationFieldCode applicationFieldCode)
			=> _applicationFieldCodeUpdateStrategy.Update(workspaceId, applicationFieldCode);

		public async Task<ApplicationFieldCode> UpdateAsync(int workspaceId, ApplicationFieldCode applicationFieldCode)
			=> await _applicationFieldCodeUpdateStrategy.UpdateAsync(workspaceId, applicationFieldCode).ConfigureAwait(false);
	}
}
