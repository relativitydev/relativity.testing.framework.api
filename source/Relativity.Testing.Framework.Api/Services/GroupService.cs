using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class GroupService : IGroupService
	{
		private readonly ICreateStrategy<Group> _createStrategy;

		private readonly IRequireStrategy<Group> _requireStrategy;

		private readonly IDeleteByIdStrategy<Group> _deleteByIdStrategy;

		private readonly IGetByIdStrategy<Group> _getByIdStrategy;

		private readonly IGetByNameStrategy<Group> _getByNameStrategy;

		private readonly IGetAllByNamesStrategy<Group> _getAllByNamesStrategy;

		private readonly IUpdateStrategy<Group> _updateStrategy;

		public GroupService(
			ICreateStrategy<Group> createStrategy,
			IRequireStrategy<Group> requireStrategy,
			IDeleteByIdStrategy<Group> deleteByIdStrategy,
			IGetByIdStrategy<Group> getByIdStrategy,
			IGetByNameStrategy<Group> getByNameStrategy,
			IGetAllByNamesStrategy<Group> getAllByNamesStrategy,
			IUpdateStrategy<Group> updateStrategy)
		{
			_createStrategy = createStrategy;
			_requireStrategy = requireStrategy;
			_deleteByIdStrategy = deleteByIdStrategy;
			_getByIdStrategy = getByIdStrategy;
			_getByNameStrategy = getByNameStrategy;
			_getAllByNamesStrategy = getAllByNamesStrategy;
			_updateStrategy = updateStrategy;
		}

		public Group Create(Group entity)
			=> _createStrategy.Create(entity);

		public Group Require(Group entity)
			=> _requireStrategy.Require(entity);

		public void Delete(int id)
			=> _deleteByIdStrategy.Delete(id);

		public Group Get(int id)
			=> _getByIdStrategy.Get(id);

		public Group Get(string name)
			=> _getByNameStrategy.Get(name);

		public IEnumerable<Group> GetAll(IEnumerable<string> names)
			=> _getAllByNamesStrategy.GetAll(names);

		public Group Update(Group entity)
			=> _updateStrategy.Update(entity);
	}
}
