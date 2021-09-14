namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IClientDomainRequestKeyStrategy
	{
		string Request(int clientArtifactID);
	}
}
