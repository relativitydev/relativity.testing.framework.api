using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface ITabGetAllForNavigationStrategy
	{
		List<Tab> Get(int workspaceID);
	}
}
