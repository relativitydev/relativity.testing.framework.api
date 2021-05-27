# ApiComponent

Before using the ApiComponent, make sure that you have relied on the CoreComponent.
See the [Getting Started guide](https://github.com/relativityone/relativity.testing.framework/wiki/Getting-Started) for Relativity.Testing.Framework for more information on this.

## Relying on the ApiComponent

The ApiComponent must be relyed on before it can be used.
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

The ApiComponent groups functionality by classes called Services.
To make a request to Relativity, you first must resolve the service that you want to use.

```
_workspaceService = RelativityFacade.Instance.Resolve<IWorkspaceService>();
```

## Calling Strategies

All public strategies will be attached to the services mentioned above.
To make a request to Relativity, just call the desired method on the service.

```
Workspace result = _workspaceService.Create(new Workspace());
```

### Making Requests

Most strategies use DTOs defined in the Relativity.Testing.Framework repository to make the request, and to store information back to the object.
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

When you create or request an object with Relativity.Testing.Framework, the return value will usually be a DTO with the properties filled out on it.
If you need to reference any of the values on the object, they are available as regular properties.

```
Console.WriteLine(workspace.Name);
Console.WriteLine(workspace.ArtifactID);
```