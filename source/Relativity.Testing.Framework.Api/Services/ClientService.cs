using System;
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

		private readonly IUpdateStrategy<Client> _updateStrategy;

		public ClientService(
			ICreateStrategy<Client> createStrategy,
			IRequireStrategy<Client> requireStrategy,
			IDeleteByIdStrategy<Client> deleteByIdStrategy,
			IGetByIdStrategy<Client> getByIdStrategy,
			IGetByNameStrategy<Client> getByNameStrategy,
			IUpdateStrategy<Client> updateStrategy)
		{
			_createStrategy = createStrategy;
			_requireStrategy = requireStrategy;
			_deleteByIdStrategy = deleteByIdStrategy;
			_getByIdStrategy = getByIdStrategy;
			_getByNameStrategy = getByNameStrategy;
			_updateStrategy = updateStrategy;
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
		{
			ValidateClientName(name);
			var result = _getByNameStrategy.Get(name);
			return result;
		}

		public void Update(Client entity)
			=> _updateStrategy.Update(entity);

		public void ValidateClientId(int id)
		{
			if (id < 1)
			{
				throw new ArgumentException("Client Artifact ID must be greater than zero.");
			}
		}

		public void ValidateClientName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Client name must not be empty nor whitespace.");
			}
		}
	}
}
