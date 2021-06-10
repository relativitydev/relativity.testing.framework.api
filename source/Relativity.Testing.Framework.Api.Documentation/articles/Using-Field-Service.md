Before you can use the [ApiComponent](/api/Relativity.Testing.Framework.Api.ApiComponent.html), remember to configure and rely on [CoreComponent](https://probable-happiness-2926a3e8.pages.github.io/api/Relativity.Testing.Framework.CoreComponent.html).

After that, we can resolve [field service](/api/Relativity.Testing.Framework.Api.Services.IFieldService.html):

```
[Test]
public void FieldService_Resolve()
{
    RelativityFacade.Instance.Resolve<IFieldService>();
}
```

For a list of available actions, please check out the interface found [here](https://github.com/relativitydev/relativity.testing.framework.api/blob/master/source/Relativity.Testing.Framework.Api/Services/IFieldService.cs).

# Create a field

In Relativity, we have a lot of different [field types](https://probable-happiness-2926a3e8.pages.github.io/api/Relativity.Testing.Framework.Models.FieldType.html) and each of them has its own list of required properties, this is mean that we can't use the base field model for the creation of that. That's why you need to use a specific field model for a specific type. The list of available fields represents bellow:

* CurrencyField
* DateField
* DecimalField
* FixedLenthTextField
* LongTextField
* MultipleChoiceField
* MultipleObjectField
* SingleChoiceField
* SingleObjectField
* UserField
* WholeNumberField
* YesNoField

---
**NOTE**

Do not use the base [Field](https://probable-happiness-2926a3e8.pages.github.io/api/Relativity.Testing.Framework.Models.Field.html) model for creation. It doesn't contain the required properties for the specific field type and creation will fail.

---

Example of code for creation of field artifact:

```
[Test]
public void Field_Create()
{
    var result = Facade.Resolve<IFieldService>()
        .Create(DefaultWorkspace.ArtifactID, new FixedLengthTextField());
}
```
