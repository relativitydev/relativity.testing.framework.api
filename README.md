# Relativity.Testing.Framework.Api

This repository contains a C#/NuGet library that abstracts out API functionality for Relativity test setup and teardown.

## Build Tasks

This repository builds with Powershell through the `.\build.ps1` script.
It supports standard tasks like `.\build.ps1 compile`, `.\build.ps1 test`, and `.\build.ps1 package`.

## Local Testing

### Unit Testing

Run through the standard compile and (unit) test tasks. These can be done without any external environment to test on.

```PowerShell
.\build.ps1 compile
.\build.ps1 test
```

### Functional Testing

To do the rest of the testing, we need to provide an instance of Relativity to test against.
This should ideally be an ephemeral one that we can throw away afterwards.

#### Creating Runsettings

Before we can run the tests, we'll need to provide a runsettings file that points the tests to the instance of Relativity that is being tested against.

```PowerShell
cd ../Relativity.Testing.Framework
./DevelopmentScripts/New-TestSettings.ps1 -ServerBindingType "https" -RelativityHostAddress "TheOneSut" -AdminUsername "TheOneAdmin@kcura.com" -AdminPassword "TheOnePassword1!"

```

#### Running Functional Tests

Run Tests:

* Attach your runsettings to the solution in Visual Studio.
* Use the built in test runner.
