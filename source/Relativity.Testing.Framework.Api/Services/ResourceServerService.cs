using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	public class ResourceServerService : IResourceServerService
	{
		private readonly IGetAllStrategy<ResourceServer> _getAllStrategy;

		public ResourceServerService(IGetAllStrategy<ResourceServer> getAllStrategy)
		{
			_getAllStrategy = getAllStrategy;
		}

		public ResourceServer[] GetAll()
			=> _getAllStrategy.GetAll();
	}
}
