using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12")]
	internal class LibraryApplicationDeleteByIdStrategyNotSupported : IDeleteByIdStrategy<LibraryApplication>
	{
		public void Delete(int id)
		{
			throw new ArgumentException("The method Delete does not support version of Relativity lower than 12.");
		}
	}
}
