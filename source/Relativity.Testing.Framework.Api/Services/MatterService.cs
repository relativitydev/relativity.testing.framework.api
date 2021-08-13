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

		private readonly IMatterGetByIdStrategy _getByIdStrategy;

		private readonly IMatterGetByNameAndClientIdStrategy _getByNameAndClientIdStrategy;

		private readonly IMatterUpdateStrategy _updateStrategy;

		private readonly IMatterGetEligibleStatusesStrategy _getEligibleStatusesStrategy;

		private readonly IMatterGetEligibleClientsStrategy _getEligibleClientsStrategy;

		public MatterService(
			ICreateStrategyWithAsync<Matter> createStrategy,
			IRequireStrategy<Matter> requireStrategy,
			IDeleteByIdStrategy<Matter> deleteByIdStrategy,
			IMatterGetByIdStrategy getByIdStrategy,
			IMatterGetByNameAndClientIdStrategy getByNameAndClientIdStrategy,
			IMatterUpdateStrategy updateStrategy,
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

		public Matter Get(int id, bool withExtendedMetadata = false)
			=> _getByIdStrategy.Get(id, withExtendedMetadata);

		public Matter Get(string name, int clientId)
			=> _getByNameAndClientIdStrategy.Get(name, clientId);

		public void Update(Matter entity, bool restrictedUpdate = false)
			=> _updateStrategy.Update(entity, restrictedUpdate);

		public ArtifactIdNamePair[] GetEligibleClients()
			=> _getEligibleClientsStrategy.GetAll();

		public async Task<ArtifactIdNamePair[]> GetEligibleClientsAsync()
			=> await _getEligibleClientsStrategy.GetAllAsync().ConfigureAwait(false);

		public ArtifactIdNamePair[] GetEligibleStatuses()
			=> _getEligibleStatusesStrategy.GetAll();

		public async Task<ArtifactIdNamePair[]> GetEligibleStatusesAsync()
			=> await _getEligibleStatusesStrategy.GetAllAsync().ConfigureAwait(false);
	}
}
