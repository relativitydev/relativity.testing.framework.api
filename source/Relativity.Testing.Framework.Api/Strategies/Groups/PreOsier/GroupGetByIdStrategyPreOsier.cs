using System;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class GroupGetByIdStrategyPreOsier : ObjectQueryGetByIdStrategy<Group>, IGroupGetByIdStrategy
	{
		public GroupGetByIdStrategyPreOsier(IObjectService objectService)
			: base(objectService)
		{
		}

		public Group Get(int id, bool includeMetadata = false, bool includeActions = false)
		{
			if (includeMetadata || includeActions)
			{
				throw new ArgumentException("The method Get Group for version of Relativity lower than 12.1 does not support including Metadata nor Actions.");
			}

			return base.Get(id);
		}
	}
}
