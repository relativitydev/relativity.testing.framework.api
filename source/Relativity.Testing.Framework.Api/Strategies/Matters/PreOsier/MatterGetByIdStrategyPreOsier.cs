using System;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class MatterGetByIdStrategyPreOsier : ObjectQueryGetByIdStrategy<Matter>, IMatterGetByIdStrategy
	{
		public MatterGetByIdStrategyPreOsier(IObjectService objectService)
			: base(objectService)
		{
		}

		public Matter Get(int id, bool withExtendedMetadata = false)
		{
			if (withExtendedMetadata)
			{
				throw new ArgumentException("The method Get with extended metadata does not support version of Relativity lower than 12.1");
			}

			return base.Get(id);
		}
	}
}
