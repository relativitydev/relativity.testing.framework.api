using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingSetUpdateStrategyNotSupported : IImagingSetUpdateStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Update Imaging Set does not support version of Relativity lower than 12.1.";

		public int Update(int workspaceId, int imagingSetId, ImagingSetRequest imagingSetRequest)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
