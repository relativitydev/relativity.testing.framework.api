[Version range attribute](https://probable-happiness-2926a3e8.pages.github.io/api/Relativity.Testing.Framework.Versioning.VersionRangeAttribute.html) can simplify ignoring of tests for a specific version of Relativity. For example, if you need to run some test only on the Relativity version higher than goatsbeard all that you need is to add that attribute on it:

```
[Test]
[VersionRange("> 10.3")]
public void TestExample()
{
    //Some actions.
}
```

To take advantage of this, you need to do a few simple actions:

1. Inherit from [SessionBasedFixture](/api/Relativity.Testing.Framework.Api.Arrangement.SessionBasedFixture.html).
2. Override [OnSetUpFixture](/api/Relativity.Testing.Framework.Api.Arrangement.SessionBasedFixture.html#Relativity_Testing_Framework_Api_Arrangement_SessionBasedFixture_OnSetUpFixture) method from [SessionBasedFixture](/api/Relativity.Testing.Framework.Api.Arrangement.SessionBasedFixture.html) and use [CheckVersionRangeForFixture](/api/Relativity.Testing.Framework.Api.Arrangement.SessionBasedFixture.html#Relativity_Testing_Framework_Api_Arrangement_SessionBasedFixture_CheckVersionRangeForFixture) method inside. That enables version check for a test fixture. But make sure that before using the [CheckVersionRangeForFixture](/api/Relativity.Testing.Framework.Api.Arrangement.SessionBasedFixture.html#Relativity_Testing_Framework_Api_Arrangement_SessionBasedFixture_CheckVersionRangeForFixture) method you rely on the [ApiComponent](/api/Relativity.Testing.Framework.Api.ApiComponent.html).
3. Override [OnSetUpTest](/api/Relativity.Testing.Framework.Api.Arrangement.SessionBasedFixture.html#Relativity_Testing_Framework_Api_Arrangement_SessionBasedFixture_OnSetUpTest) method from [SessionBasedFixture](/api/Relativity.Testing.Framework.Api.Arrangement.SessionBasedFixture.html) and use [CheckVersionRangeForTest](/api/Relativity.Testing.Framework.Api.Arrangement.SessionBasedFixture.html#Relativity_Testing_Framework_Api_Arrangement_SessionBasedFixture_CheckVersionRangeForTest) method inside. That enables version check for each test in the fixture.
