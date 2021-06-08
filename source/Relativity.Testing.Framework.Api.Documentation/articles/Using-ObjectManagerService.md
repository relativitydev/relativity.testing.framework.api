# Create Object Manager Service

Before you can use the ApiComponent, remember to [configure and rely on ApiComponent](Getting-Started.html).

After that, we can resolve object manager service:

```
[Test]
public void ObjectManager_Resolve()
{
    RelativityFacade.Instance.Resolve<IObjectService>();
}
```

# Create your own model

Object manager service can receive any model, and if all names will mapping correct then all methods will work fine. For example you can have model like this and all will work correct:

```

public class Client : Artifact
{
    public string Name { get; internal set; }
}
```

This will work fine because the object type of this model really named 'Client' and this artifact has field 'Name'.

A model like this will fail if an object named "Short Client" with field "Client Name" does not exist in Relativity:

```
public class ShortClient : Artifact
{
    public string ClientName { get; internal set; }
}
```

---
**NOTE**

Note the space! By convention, the framework assumes a class "NamedLikeThis" refers to an artifact in Relativity "Named Like This".

---

If this does not work for you there are two options:

1. Using our attributes, 'ObjectTypeName' for the class name and 'FieldName' for the property name.
2. Renaming class and properties. This way will work fine but not always it is possible.

An example of overriding the conventional approach using attributes:

```
[ObjectTypeName("Client")]
public class ShortClient : Artifact
{
    [FieldName("Name")]
    public string ClientName { get; internal set; }
}
```

But if don't need to parse some property then you can mark it like NonField and we will not deserialize that property.

```
[ObjectTypeName("Client")]
public class ShortClient : Artifact
{
    [NonField]
    public string ClientName { get; internal set; }
}
```

As of version 0.27.0, you can also specify object type and fields by Guids.

```
[ObjectTypeGuid(TheGuidForClient)]
public class AClientButWithADifferentNameForSomeReason : Artifact
{
    [FieldGuid(TheGuidForTheNameFieldOnClients)]
    public string NameButWithADifferentPropertyNameForSomeReason { get; internal set; }
}
```

# Delete entity 

RTF object manager service supports the deletion of an entity by using its artifact ID. Mainly object manager deletion the same as in all other services.

```
[Test]
public void ObjectService_Delete()
{
    var objectManagerService = Facade.Resolve<IObjectService>();
    MarkupSet toDelete = null;
 
    ArrangeWorkingWorkspace(x => x.Create(new MarkupSet()).Pick(out toDelete ));
 
    _objectService.Delete(DefaultWorkspace.ArtifactID, toDelete.ArtifactId);
 
    Facade.Resolve<IGetWorkspaceEntityByIdStrategy<MarkupSet>>().Get(DefaultWorkspace.ArtifactID, toDelete.ArtifactID).
        Should().BeNull();
}
```

# Create entity 

RTF object manager service supports the creation of an entity through the model. All that you need to do it is create a correctly model. Mainly object manager creation the same as in all other services.

```
[Test]
public void ObjectService_Create()
{
    var objectManagerService = Facade.Resolve<IObjectService>();
    var entity = new MarkupSet();
    entity.FillRequiredProperties();
 
    var result = _objectService.Create(DefaultWorkspace.ArtifactID, entity);
 
    result.ArtifactID.Should().BePositive();
    result.Name.Should().NotBeNullOrEmpty();
    result.RedactionText.Should().NotBeNullOrEmpty();
}
```

That's all. But you should be sure that all the properties mapping correct, if you don't know how to do it read 'Create your own model' section. 

# Receive entities 

## Get all artifacts using object manager

For getting all elements using object manager all you need to specify is the model of artifact. The example represents bellow: 

```
[Test]
public void Client_GetAll()
{
    var objectManagerService = Facade.Resolve<IObjectService>();
 
    var clients = objectManagerService.GetAll<Client>();
 
    clients.Should().NotBeNullOrEmpty();
}
```

For now, method GetAll does not support workspace level search, so if you need to do it please use the Query method.

--- 
**NOTE**

You should understand that object manager do not support all of the Relativity artifacts, please check the list of supported artifacts [here](https://platform.relativity.com/RelativityOne/Content/RSAPI/Searching_Relativity/Searching_Relativity.htm#SystemTypes).


---

## Query artifacts using object manager

The basic method of querying artifact look like:

```
[Test]
public void Client_Query()
{
    var objectManagerService = Facade.Resolve<IObjectService>();
 
    var clients = objectManagerService.Query<Choice>().ToArray();
 
    clients.Should().NotBeNullOrEmpty();
}
```

### How to use fields to return specific artifacts

This example returns the same result us method GetAll. But very often we need to specify some field and you can do it with method where. 
Method 'where' use predicate, so you can write any boolean operation for getting artifacts.

```
[Test]
public void Client_QueryUsingModelProperty()
{
    var clients = Facade.Resolve<IObjectService>().Query<Choice>().Where(x => x.Name == "Name").ToArray();
 
    clients.Should().NotBeNullOrEmpty();
}
```

### How to fetch specific fields

Some times we need only some specific fields. And method 'Fetch' can help you with it:

```

[Test]
public void Client_Query()
{
    var clientFields = Facade.Resolve<IObjectService>().Query<Choice>()
        .Fetch(nameof(Choice.Field)).Select(x => x.Field).ToArray();
 
    clientFields.Should().NotBeNullOrEmpty();
}
```

### How to query workspace artifacts

For querying workspace artifacts you need to specify workspace id. You should use method 'for' for it:

```
[Test]
public void Client_Query()
{
    var clients = Facade.Resolve<IObjectService>().Query<Choice>()
        .For(DefaultWorkspace.ArtifactID).ToArray();
 
    clients.Should().NotBeNullOrEmpty();
}
```

# Update entity 

RTF object manager service supports the update of an entity through the model. All that you need to do it is create a correct model. Mainly object manager creation the same as in all other services.

```
[Test]
public void ObjectService_Update()
{
    var objectManagerService = Facade.Resolve<IObjectService>();
    MarkupSet toUpdate = null;
 
 
    ArrangeWorkingWorkspace(x => x.Create(new MarkupSet()).Pick(out toUpdate));
 
 
    toUpdate.Name = Randomizer.GetString();
    toUpdate.Order = Randomizer.GetInt(500);
    toUpdate.RedactionText = Randomizer.GetString();
 
    _objectService.Update(DefaultWorkspace.ArtifactID, toUpdate);
 
    var result = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<MarkupSet>>()
        .Get(DefaultWorkspace.ArtifactID, toUpdate.ArtifactID);
 
    result.Should().BeEquivalentTo(toUpdate);
}
```

That's all. But you should be sure that all the properties mapping correct, if you don't know how to do it read 'Create your own model' section. 
