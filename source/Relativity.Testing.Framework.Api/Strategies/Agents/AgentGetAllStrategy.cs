using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AgentGetAllStrategy : IGetAllStrategy<Agent>
	{
		private readonly IObjectService _objectService;

		public AgentGetAllStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Agent[] GetAll()
		{
			return _objectService.GetAll<Agent>();
		}
	}
}
