using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies.Tabs
{
	internal interface ITabGetAvailableObjectTypesByWorkspaceIDStrategy
	{
		List<ObjectType> Get(int id);
	}
}
