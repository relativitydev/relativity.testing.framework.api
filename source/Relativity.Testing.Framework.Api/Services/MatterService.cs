using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class MatterService : IMatterService
	{
		private readonly ICreateStrategy<Matter> _createStrategy;

		private readonly IRequireStrategy<Matter> _requireStrategy;

		private readonly IDeleteByIdStrategy<Matter> _deleteByIdStrategy;

		private readonly IGetByIdStrategy<Matter> _getByIdStrategy;

		private readonly IMatterGetByNameAndClientIdStrategy _getByNameAndClientIdStrategy;

		private readonly IUpdateStrategy<Matter> _updateStrategy;

		public MatterService(
			ICreateStrategy<Matter> createStrategy,
			IRequireStrategy<Matter> requireStrategy,
			IDeleteByIdStrategy<Matter> deleteByIdStrategy,
			IGetByIdStrategy<Matter> getByIdStrategy,
			IMatterGetByNameAndClientIdStrategy getByNameAndClientIdStrategy,
			IUpdateStrategy<Matter> updateStrategy)
		{
			_createStrategy = createStrategy;
			_requireStrategy = requireStrategy;
			_deleteByIdStrategy = deleteByIdStrategy;
			_getByIdStrategy = getByIdStrategy;
			_getByNameAndClientIdStrategy = getByNameAndClientIdStrategy;
			_updateStrategy = updateStrategy;
		}

		public Matter Create(Matter entity)
			=> _createStrategy.Create(entity);

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
	}
}
