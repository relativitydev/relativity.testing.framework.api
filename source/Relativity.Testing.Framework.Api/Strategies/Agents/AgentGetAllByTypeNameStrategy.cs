using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AgentGetAllByTypeNameStrategy : IAgentGetAllByAgentTypeNameStrategy
	{
		private readonly IObjectService _objectService;

		public AgentGetAllByTypeNameStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Agent[] GetAllByTypeName(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException(nameof(typeName));
			}

			return _objectService.Query<Agent>().Where(x => x.AgentType, typeName).ToArray();
		}
	}
}
