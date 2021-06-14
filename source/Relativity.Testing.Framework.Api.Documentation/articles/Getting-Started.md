# ApiComponent

Before using the [ApiComponent](/api/Relativity.Testing.Framework.Api.ApiComponent.html), make sure that you have relied on the [CoreComponent](https://probable-happiness-2926a3e8.pages.github.io/api/Relativity.Testing.Framework.CoreComponent.html).
See the [Getting Started guide](https://github.com/relativityone/relativity.testing.framework/wiki/Getting-Started) for [Relativity.Testing.Framework](https://probable-happiness-2926a3e8.pages.github.io/) for more information on this.

## Relying on the ApiComponent

The [ApiComponent](/api/Relativity.Testing.Framework.Api.ApiComponent.html) must be relyed on before it can be used.
There are no configuration options to pass in while initializing this component.

```
public class Tests
{
    [OneTimeSetUp]
    public void Setup()
    {
        RelativityFacade.Instance.RelyOn<CoreComponent>();
        RelativityFacade.Instance.RelyOn<ApiComponent>();
    }
}
```

## Resolving Services

The [ApiComponent](/api/Relativity.Testing.Framework.Api.ApiComponent.html) groups functionality by classes called [Services](/api/Relativity.Testing.Framework.Api.Services.html).
To make a request to Relativity, you first must resolve the service that you want to use.

```
_workspaceService = RelativityFacade.Instance.Resolve<IWorkspaceService>();
```

## Calling Strategies

All RTF strategies will be attached to the [services](/api/Relativity.Testing.Framework.Api.Services.html) mentioned above.
To make a request to Relativity, just call the desired method on the [service](/api/Relativity.Testing.Framework.Api.Services.html).

```
Workspace result = _workspaceService.Create(new Workspace());
```

### Making Requests

Most RTF strategies use DTOs defined in the [Relativity.Testing.Framework repository](https://github.com/relativitydev/relativity.testing.framework) to make the request, and to store information back to the object.
In the example above, we didn't provide any values to the DTO, so defaults will be generated or found in the environment.

We can also provide values to the DTO, and they will be used in the request.

```
var workspace = new Workspace
{
    Name = "MySpecialWorkspace",
};

Workspace workspace = _workspaceService.Create(workspace);
```

### Accessing properties

When you create or request an object with [Relativity.Testing.Framework](https://probable-happiness-2926a3e8.pages.github.io/), the return value will usually be a DTO with the properties filled out on it.
If you need to reference any of the values on the object, they are available as regular properties.

```
Console.WriteLine(workspace.Name);
Console.WriteLine(workspace.ArtifactID);
```