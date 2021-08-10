namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// <para>Represents the REST HTTP service that provides a set of methods to communicate with Relativity REST APIs.</para>
	/// <para>Under normal circumstances, it is not necessary to directly utilize the IRestService, as calls to it are made by the specific Services/Strategies.</para>
	/// </summary>
	/// <example>
	/// In this example, we make a request to the GetRelativityVersionAsync, and deserialize it into a string value.
	/// <code>
	/// IRestService restService = RelativityFacade.Instance.Resolve&lt;IRestService&gt;();
	///
	/// string version = restService.Post&lt;string&gt;("Relativity.Services.InstanceDetails.IInstanceDetailsModule/InstanceDetailsService/GetRelativityVersionAsync");
	/// </code>
	/// </example>
	public interface IRestService : IHttpService
	{
	}
}
