using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class LibraryApplicationGetByNameStrategyV1 : IGetByNameStrategy<LibraryApplication>
	{
		private readonly IObjectService _objectService;

		private readonly IGetByIdStrategy<LibraryApplication> _getByIdStrategy;

		public LibraryApplicationGetByNameStrategyV1(
			IObjectService objectService,
			IGetByIdStrategy<LibraryApplication> getByIdStrategy)
		{
			_objectService = objectService;
			_getByIdStrategy = getByIdStrategy;
		}

		public LibraryApplication Get(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			var artifact = _objectService.Query<LibraryApplication>().
				FetchOnlyArtifactID().
				Where(x => x.Name, name).
				FirstOrDefault();

			if (artifact == null)
			{
				return null;
			}

			return _getByIdStrategy.Get(artifact.ArtifactID);
		}
	}
}
