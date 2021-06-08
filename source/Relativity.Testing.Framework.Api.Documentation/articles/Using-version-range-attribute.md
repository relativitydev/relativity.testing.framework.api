Version range attribute can simplify ignoring of tests for a specific version of Relativity. For example, if you need to run some test only on the Relativity version higher than goatsbeard all that you need is to add that attribute on it:

```
[Test]
[VersionRange("> 10.3")]
public void TestExample()
{
    //Some actions.
}
```

To take advantage of this, you need to do a few simple actions:

1. Inherit from SessionBasedFixture.
2. Override OnSetUpFixture method from UITestFixture and use CheckVersionRangeForFixture method inside. That enables version check for a test fixture. But make sure that before using the CheckVersionRangeForFixture method you rely on the API component
3. Override OnSetUpTest method from UITestFixture and use CheckVersionRangeForTest method inside. That enables version check for each test in the fixture.
