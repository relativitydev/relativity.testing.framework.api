using System;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ClientDomainRequestKeyStrategyNotSupported : IClientDomainRequestKeyStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Request Key for Client Domain does not support version of Relativity lower than 12.1.";

		public string Request(int clientArtifactID)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
