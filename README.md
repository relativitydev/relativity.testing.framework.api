# Relativity.Testing.Framework.Api

:+1::tada: Welcome to Relativity.Testing.Framework.Api! :tada::+1:

This repository contains a C#/NuGet library that abstracts out API functionality for Relativity test setup and teardown.

## Table of Contents

- [Relativity.Testing.Framework.Api](#relativitytestingframeworkapi)
  - [Table of Contents](#table-of-contents)
  - [Supported Relativity Versions](#supported-relativity-versions)
  - [Documentation](#documentation)
  - [Where to get help](#where-to-get-help)
  - [Build Tasks](#build-tasks)
  - [Local Testing](#local-testing)
    - [Unit Testing](#unit-testing)
    - [Functional Testing](#functional-testing)
      - [Creating Runsettings](#creating-runsettings)
      - [Running Functional Tests](#running-functional-tests)
    - [Usage Metrics](#usage-metrics)
  - [Contributing](#contributing)
  - [Maintainers](#maintainers)
  - [Reporting Issues and Adding Feature Enhancements](#reporting-issues-and-adding-feature-enhancements)

## Supported Relativity Versions

The Relativity Testing Framework repository for RelativityOne will no longer be available publicly beginning **2024/05/31**.  Developers are recommended to move towards using the Relativity APIs directly. See post from Community Site [here](https://community.relativity.com/s/feed/0D5Qi0000087STIKA2).

Relativity Testing Framework will **continue to be supported for Relativity Server development**. Effective with the Server 2023 release you can find the latest Relativity Testing Framework SDK package on the **Server SDK package feed [here](https://relativitypackageseastus.jfrog.io/ui/native/server-nuget-remote/)**. For more information on the Server SDK feed and go-forward Server SDK strategy please see [here](https://platform.relativity.com/Server2023/Content/What_s_new/Server_SDK_Changes.htm).

## Documentation

For more details and common usage patterns check out [our documentation](https://relativitydev.github.io/relativity.testing.framework.api/).

## Where to get help

- For general help and questions, please start a [Discussion](https://github.com/relativitydev/relativity.testing.framework.api/discussions).

## Build Tasks

This repository builds with the [dotnet sdk](https://dotnet.microsoft.com/download). It supports standard tasks like dotnet build, dotnet test and dotnet pack.

## Local Testing

This repository has unit and functional tests in it. Functional tests have Category=FunctionalTests.

### Unit Testing

Run through the standard compile and (unit) test tasks. These can be done without any external environment to test on.

```PowerShell
dotnet test ./Source/ --filter TestCategory!=FunctionalTests
```

### Functional Testing

To do the rest of the testing, we need to provide an instance of Relativity to test against.
This should ideally be an ephemeral one that we can throw away afterwards.

#### Creating Runsettings

Before we can run the tests, we'll need to provide a runsettings file that points the tests to the instance of Relativity that is being tested against.
To create the runsettings file, run the following command, replacing instance settings parameters with what you need:

```PowerShell
./DevelopmentScripts/New-TestSettings.ps1 -ServerBindingType "https" -RelativityHostAddress "YOUR_HOST_ADDRESS" -AdminUsername "YOUR_ADMIN_USERNAME" -AdminPassword "YOUR_ADMIN_PASSWORD"

```

This script will create FunctionalTest.runsettings and put it next to itself. You shall use this file to setup functional testing.

#### Running Functional Tests

Run Tests in Visual Studio:

- Attach your runsettings file to the solution in Visual Studio.
- Use the built in test runner.

You can also run functional tests from the command line:

```PowerShell
dotnet test ./Source/ --filter TestCategory=FunctionalTests -s .\DevelopmentScripts\FunctionalTest.runsettings

```

### Usage Metrics

EnableApplicationInsights can be set in the RunSettings with the following values to set how much is logged. By default this is set to All and information about your code will be sent to Relativity. For more information about the different logging levels, see [Metrics-Collection](https://relativitydev.github.io/relativity.testing.framework.api/articles/Metrics-Collection.html).

- All
- UsageOnly
- None

## Contributing

See [CONTRIBUTING.md](https://github.com/relativitydev/relativity.testing.framework.api/blob/master/CONTRIBUTING.md).

## Maintainers

The Developer Environments team is the primary care-taker of this repository.

## Reporting Issues and Adding Feature Enhancements

For bug reports or feature enhancements, please create an [Issue](https://github.com/relativitydev/relativity.testing.framework.api/issues) using the respective template. Before reporting an issue, please follow these guidelines. It helps us in understanding the request and provide a quicker response time.

- Determine if it's a bug report or a feature enhancement.
- Perform a quick search in the [issue tracker](https://github.com/relativitydev/relativity.testing.framework.api/issues) to see if the issue has already been reported. If it has, add a comment to the existing issue instead of opening a new one.
- If you find a **Closed** issue that seems like it is the same thing that you're experiencing, open a new issue and include a link to the original issue in the body of your new one.
