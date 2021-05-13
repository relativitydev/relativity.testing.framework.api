using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AgentTypeGetAllStrategy : IGetAllStrategy<AgentType>
	{
		private readonly IObjectService _objectService;

		public AgentTypeGetAllStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public AgentType[] GetAll()
		{
			return _objectService.GetAll<AgentType>();
		}
	}
}
