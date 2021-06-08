Http service represents a set of methods to communicate with Relativity REST API.

Inside this service we use a HttpClient and set all required for Relativity fields, this service also provides deserialization functionality by using Newtonsoft.Json.

See the documentation for more information about the HTTPSerice.

Under normal circumstances, you will not need to use the HTTPService.
RTF.API has a wrapper RESTService that consumes this, and is used by all services and strategies.

The most obvious use case for standalone use of the HttpService is to explicitly specific the parameters passed in for requests
Here is an example that you can find [here](https://github.com/relativitydev/relativity.testing.framework.api/blob/master/source/Relativity.Testing.Framework.Api.FunctionalTests/HttpServiceFixture.cs).

# Using relativity REST in RTF

For using services which have base URL like ServerBindingType://HostAddress/relativity.rest/ RTF represents the service: IRestService.
So if you need to send some custom rest request all that you need to do it is to resolve IRestService and use it, but remember to configure and rely on [ApiComponent](Getting-Started.html).

```
public class Tests
{
    private IRestService _restService;
  
    [OneTimeSetUp]
    public void Setup()
    {
        RelativityFacade.Instance.RelyOn<CoreComponent>();
        RelativityFacade.Instance.RelyOn<ApiComponent>();
        _restService = RelativityFacade.Instance.Resolve<IRestService>();
    }
 
    [Test]
    public void Post()
    {
        string result = _restService.Post<string>("Relativity.Services.InstanceDetails.IInstanceDetailsModule/InstanceDetailsService/GetRelativityVersionAsync");
 
        result.Should().NotBeNullOrWhiteSpace(); // Here we use FluentAssertions package
    }
}
```

# Deserialize response of services based on HttpService.

All methods in HttpService have a generic parameter, this parameter specified the type of response which we waiting for. For example, if we waiting for some integer as a response then we should specify it like below:

```
[Test]
public void Post()
{
    int result = _restService.Post<int>("Some url.");
 
    result.Should().NotBeNullOrWhiteSpace(); // Here we use FluentAssertions package
}
```

If we don't wait for any result then do not use generic, like in the example below:

```
[Test]
public void Post()
{
    int result = _restService.Post("Some url.");
 
    result.Should().NotBeNullOrWhiteSpace(); // Here we use FluentAssertions package
}
```

Also, you can use any model and if all names will mapping correct then all methods will work fine. For example, you can have a model like this and use client Get call, all will work correct:

```
public class Client : Artifact
{
    public string Name { get; internal set; }
}
```

But if you have some custom property like ClientName which you do not return in response than it will fail. If you need that property but name in response is the difference then you can rename property or overload it by using FieldName attribute or just.

```
public class ShortClient : Artifact
{
    [FieldName("Name")]
    public string ClientName { get; internal set; }
}
```

But if don't need to parse this property then you can mark it like NonField and we will not deserialize that property.

```
public class ShortClient : Artifact
{
    [NonField]
    public string ClientName { get; internal set; }
}
```
