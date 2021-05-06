using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ClientService : IClientService
	{
		private readonly ICreateStrategy<Client> _createStrategy;

		private readonly IRequireStrategy<Client> _requireStrategy;

		private readonly IDeleteByIdStrategy<Client> _deleteByIdStrategy;

		private readonly IGetByIdStrategy<Client> _getByIdStrategy;

		private readonly IGetByNameStrategy<Client> _getByNameStrategy;

		public ClientService(
			ICreateStrategy<Client> createStrategy,
			IRequireStrategy<Client> requireStrategy,
			IDeleteByIdStrategy<Client> deleteByIdStrategy,
			IGetByIdStrategy<Client> getByIdStrategy,
			IGetByNameStrategy<Client> getByNameStrategy)
		{
			_createStrategy = createStrategy;
			_requireStrategy = requireStrategy;
			_deleteByIdStrategy = deleteByIdStrategy;
			_getByIdStrategy = getByIdStrategy;
			_getByNameStrategy = getByNameStrategy;
		}

		public Client Create(Client entity)
			=> _createStrategy.Create(entity);

		public Client Require(Client entity)
			=> _requireStrategy.Require(entity);

		public void Delete(int id)
			=> _deleteByIdStrategy.Delete(id);

		public Client Get(int id)
			=> _getByIdStrategy.Get(id);

		public Client Get(string name)
			=> _getByNameStrategy.Get(name);
	}
}
