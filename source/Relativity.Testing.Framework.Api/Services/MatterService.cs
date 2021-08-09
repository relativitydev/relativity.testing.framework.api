using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class MatterService : IMatterService
	{
		private readonly ICreateStrategyWithAsync<Matter> _createStrategy;

		private readonly IRequireStrategy<Matter> _requireStrategy;

		private readonly IDeleteByIdStrategy<Matter> _deleteByIdStrategy;

		private readonly IGetByIdStrategy<Matter> _getByIdStrategy;

		private readonly IMatterGetByNameAndClientIdStrategy _getByNameAndClientIdStrategy;

		private readonly IUpdateStrategy<Matter> _updateStrategy;

		private readonly IMatterGetEligibleStatusesStrategy _getEligibleStatusesStrategy;

		private readonly IMatterGetEligibleClientsStrategy _getEligibleClientsStrategy;

		public MatterService(
			ICreateStrategyWithAsync<Matter> createStrategy,
			IRequireStrategy<Matter> requireStrategy,
			IDeleteByIdStrategy<Matter> deleteByIdStrategy,
			IGetByIdStrategy<Matter> getByIdStrategy,
			IMatterGetByNameAndClientIdStrategy getByNameAndClientIdStrategy,
			IUpdateStrategy<Matter> updateStrategy,
			IMatterGetEligibleStatusesStrategy getEligibleStatusesStrategy,
			IMatterGetEligibleClientsStrategy getEligibleClientsStrategy)
		{
			_createStrategy = createStrategy;
			_requireStrategy = requireStrategy;
			_deleteByIdStrategy = deleteByIdStrategy;
			_getByIdStrategy = getByIdStrategy;
			_getByNameAndClientIdStrategy = getByNameAndClientIdStrategy;
			_updateStrategy = updateStrategy;
			_getEligibleStatusesStrategy = getEligibleStatusesStrategy;
			_getEligibleClientsStrategy = getEligibleClientsStrategy;
		}

		public Matter Create(Matter entity)
			=> _createStrategy.Create(entity);

		public async Task<Matter> CreateAsync(Matter entity)
			=> await _createStrategy.CreateAsync(entity).ConfigureAwait(false);

		public Matter Require(Matter entity)
			=> _requireStrategy.Require(entity);

		public void Delete(int id)
			=> _deleteByIdStrategy.Delete(id);

		public Matter Get(int id)
			=> _getByIdStrategy.Get(id);

		public Matter Get(string name, int clientId)
			=> _getByNameAndClientIdStrategy.Get(name, clientId);

		public void Update(Matter entity)
			=> _updateStrategy.Update(entity);

		public async Task<ArtifactIdNamePair[]> GetEligibleClientsAsync()
			=> await _getEligibleClientsStrategy.GetAllAsync().ConfigureAwait(false);

		public async Task<ArtifactIdNamePair[]> GetEligibleStatusesAsync()
			=> await _getEligibleStatusesStrategy.GetAllAsync().ConfigureAwait(false);
	}
}
