# Relativity.Testing.Framework.Api

This repository contains a C#/NuGet library that abstracts out API functionality for Relativity test setup and teardown.

## Build Tasks

This repository builds with the dotnet sdk. It supports standard tasks like dotnet build, dotnet test (for unit testing), and dotnet pack.

## Local Testing

### Unit Testing

Run through the standard compile and (unit) test tasks. These can be done without any external environment to test on.
Note that unit tests located at Relativity.Testing.Framework.Api.Tests folder.

```PowerShell
dotnet test
```

### Functional Testing

To do the rest of the testing, we need to provide an instance of Relativity to test against.
This should ideally be an ephemeral one that we can throw away afterwards.

#### Creating Runsettings

Before we can run the tests, we'll need to provide a runsettings file that points the tests to the instance of Relativity that is being tested against.
To create the runsettings file in project root run the following comand replacing instance settings parameters with what you need:

```PowerShell
./DevelopmentScripts/New-TestSettings.ps1 -ServerBindingType "https" -RelativityHostAddress "TheOneSut" -AdminUsername "TheOneAdmin@kcura.com" -AdminPassword "TheOnePassword1!"

```
This script will create FunctionalTest.runsettings and put it next to itself. You shall use this file to setup functional testing.

#### Running Functional Tests

Run Tests:

* Attach your runsettings file to the solution in Visual Studio.
* Use the built in test runner.

## Documentation

For more details check out our documentation that can be found [here](https://glowing-spork-1e23a31b.pages.github.io/)