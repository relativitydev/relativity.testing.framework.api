﻿using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IItemSetGroupPermissionsStrategy
	{
		Task SetAsync(int workspaceId, int itemId, GroupPermissions groupPermissions);
	}
}
