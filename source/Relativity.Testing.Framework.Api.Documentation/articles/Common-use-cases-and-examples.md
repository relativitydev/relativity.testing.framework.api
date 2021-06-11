The best source of examples for using Relativity.Testing.Framework.Api are our own [functional tests](https://github.com/relativitydev/relativity.testing.framework.api/tree/master/source/Relativity.Testing.Framework.Api.FunctionalTests/Strategies).
These run as a part of pull requests and test out the strategies themselves.

Here are some examples of common use cases for things you might want to do with Relativity Testing Framework.

# Setup

Before creating anything, remember to RelyOn the [CoreComponent](https://probable-happiness-2926a3e8.pages.github.io/api/Relativity.Testing.Framework.CoreComponent.html) and [ApiComponent](/api/Relativity.Testing.Framework.Api.ApiComponent.html).

```
[OneTimeSetup]
public SetUp()
{
    RelativityFacade.Instance.RelyOn<CoreComponent>();
    RelativityFacade.Instance.RelyOn<ApiComponent>();
}
```

# Creating Workspaces

Before using an [Service](/api/Relativity.Testing.Framework.Api.Services.IWorkspaceService.html), it must be resolved.

```
_workspaceService = RelativityFacade.Instance.Resolve<IWorkspaceService>();
```

## Create a Workspace

The simplest way to use most strategies is to just in an empty DTO/model.
This will usually look up defaults and create the object for you 

```
Workspace workspace = _workspaceService.Create(new Workspace());
```

## A Workspace with a specific name

The properties on the [model](https://probable-happiness-2926a3e8.pages.github.io/api/Relativity.Testing.Framework.Models.Workspace.html) can be specified, and those value will be used to make the request.

```
Workspace workspace = new Workspace
{
    Name = "MySpecialWorkspace",
};
 
workspace = _workspaceService.Create(workspace);
```

# Creating Users and Groups

```
_userService = RelativityFacade.Instance.Resolve<IUserService>();
_groupService = RelativityFacade.Instance.Resolve<IGroupService>();
_permissionService = RelativityFacade.Instance.Resolve<IPermissionService>();
```

## Creating a User

```
User user = _userService.Create(new User());
```

## Creating a Group

```
Group group = _groupService.Create(new Group());
```

## Adding a User to a Group

```
_userService.AddToGroup(user.ArtifactID, group.ArtifactID);
```

## Adding a Group to a Workspace

```
_permissionService.AddWorkspaceToGroup(workspace.ArtifactID, group.ArtifactID);
```

# Installing Applications

```
_libraryApplicationService = RelativityFacade.Instance.Resolve<ILibraryApplicationService>();
```

## Installing an application to the library

```
_libraryApplicationService.InstallToLibrary(_pathToRapFile);
LibraryApplication application = _libraryApplicationService.Get(_rapName);
```

## Installing a library application to a workspace

```
_libraryApplicationService.InstallToWorkspace(workspace.ArtifactID, application .ArtifactID);
```

# Creating Objects

```
_objectService = RelativityFacade.Instance.Resolve<IObjectService>();
```

## Defining Object Types

In your code, you should model object types as DTOs.
This allows RTF to make requests using these object types, and map values to them as part of those requests.

In this example, we are assuming that your application contains an [ObjectType](https://probable-happiness-2926a3e8.pages.github.io/api/Relativity.Testing.Framework.Models.ObjectType.html) called SomeObjectType, and it has a field attached to it called SomeField.

```
public class SomeObjectType : NamedArtifact
{
    // ArtifactId and Name come from NamedArtifact.
    // Other fields on object types should be defined here.
    //public int ArtifactID { get; set; }
    //public string Name { get; set; }

    public sting SomeField { get; set; }
}
```

## Creating RDOs

We can then use the DTO for the [ObjectType](https://probable-happiness-2926a3e8.pages.github.io/api/Relativity.Testing.Framework.Models.ObjectType.html) to make RDOs using the [ObjectService](/api/Relativity.Testing.Framework.Api.ObjectManagement.IObjectService.html).

```
SomeObjectType someObjectType = _objectService.Create(_workspace.ArtifactID, new RapTemplate
{
    Name = "MyObject",
    SomeField = "SomeValue"
});
```

# Putting it all together

```
using System.IO;
using NUnit.Framework;
using Relativity.Testing.Framework;
using Relativity.Testing.Framework.Api;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace SomeObjectType.FunctionalTests.CI
{
    [TestFixture]
    public class Tests
    {
        private IWorkspaceService _workspaceService;
        private ILibraryApplicationService _applicationService;
        private IObjectService _objectService;
        private IGroupService _groupService;
        private IUserService _userService;
        private IPermissionService = _permissionService;

        private Workspace _workspace;
        private LibraryApplication _application;

        [OneTimeSetUp]
        public void SetupTests()
        {
            RelativityFacade.Instance.RelyOn<CoreComponent>();
            RelativityFacade.Instance.RelyOn<ApiComponent>();

            string rapPath = Path.Combine(RelativityFacade.Instance.Config.RelativityInstance.RapDirectory, "SomeObject.rap");

            _workspaceService = RelativityFacade.Instance.Resolve<IWorkspaceService>();
            _applicationService = RelativityFacade.Instance.Resolve<ILibraryApplicationService>();
            _objectService = RelativityFacade.Instance.Resolve<IObjectService>();
            _userService = RelativityFacade.Instance.Resolve<IUserService>();
            _groupService = RelativityFacade.Instance.Resolve<IGroupService>();
            _permissionService = RelativityFacade.Instance.Resolve<IPermissionService>();

            _workspace = _workspaceService.Create(new Workspace());
            _user = _userService.Create(new User());
            _group = _groupService.Create(new Group());
            _userService.AddToGroup(user.ArtifactID, group.ArtifactID);
            _permissionService.AddWorkspaceToGroup(workspace.ArtifactID, group.ArtifactID);

            _applicationService.InstallToLibrary(rapPath);
            _application = _applicationService.Get("SomeObject");
        }

        [Test]
        public void InstallingApplicationToAWorkspaceSucceeds()
        {
            _applicationService.InstallToWorkspace(_workspace.ArtifactID, _application.ArtifactID);
            Assert.IsTrue(_applicationService.IsInstalledInWorkspace(_workspace.ArtifactID, _application.ArtifactID));
        }

        [Test]
        public void AnInstanceOfSomeObjectTypeCanBeCreated()
        {
            SomeObjectType someObjectType = _objectService.Create(_workspace.ArtifactID, new SomeObjectType
            {
                Name = "AName",
                SomeField = "SomeValue"
            });

            Assert.That(someObjectType.ArtifactID > 0);
            Assert.That(someObjectType.Name == "AName");
            Assert.That(someObjectType.SomeField == "SomeValue");
        }

        [Test]
        public UserHasAccessToSomeObjectType()
        {
            // Some UI testing could be done here as user acceptance testing.
            //Login(_user.EmailAddress, _user.Password);
            //GoToWorkspace(_workspace.Name);
            //GoToTab("SomeObjectType");
            //GoToObject(Name = "AName");
            //VerifyProperties();
        }
    }

    public class SomeObjectType : NamedArtifact
    {
        public SomeField { get; set; }
    }
}
```