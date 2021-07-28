using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ToggleService : IToggleService
	{
		private readonly IGetAllStrategy<Api.Toggle> _getAllStrategy;
		private readonly IGetByNameStrategy<Api.Toggle> _getEntityByNameStrategy;

		public ToggleService(IGetAllStrategy<Api.Toggle> getAllStrategy, IGetByNameStrategy<Api.Toggle> getEntityByNameStrategy)
		{
			_getAllStrategy = getAllStrategy;
			_getEntityByNameStrategy = getEntityByNameStrategy;
		}

		public Toggle Get(string name)
			=> _getEntityByNameStrategy.Get(name);

		public Toggle[] GetAll()
			=> _getAllStrategy.GetAll();

		public void Require(Toggle toggle)
		{
			throw new NotImplementedException();
		}
	}
}
