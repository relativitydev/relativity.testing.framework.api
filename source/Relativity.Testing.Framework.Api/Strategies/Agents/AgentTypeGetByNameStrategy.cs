using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AgentTypeGetByNameStrategy : IGetByNameStrategy<AgentType>
	{
		private readonly IObjectService _objectService;

		public AgentTypeGetByNameStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public AgentType Get(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			return _objectService.Query<AgentType>()
				.Where(x => x.Name, name).FirstOrDefault();
		}
	}
}
