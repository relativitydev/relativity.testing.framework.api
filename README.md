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
  - [Creating Support Tickets](#creating-support-tickets)
  - [Feature Enhancements](#feature-enhancements)

## Supported Relativity Versions

- Official support is only provided for the following versions.
  - RelativityOne current and preview versions
  - Relativity Server 2022 (12.1) and later

## Documentation

For more details and common usage patterns check out [our documentation](https://relativitydev.github.io/relativity.testing.framework.api/).

## Where to get help

- For general help and questions, please use slack [#help-relativity-testing-framework](https://kcura-pd.slack.com/archives/CMG6M7D6D).

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

The [Developer Environments](https://einstein.kcura.com/x/ehUcBg) team is the primary care-taker of this repository. Contact and support information may be found on the linked team page.

## Creating Support Tickets

For issues with relativity.testing.framework.api please email "help-esys@relativity.com".  Please Include "relativity.testing.framework.api" in the email subject with all important aspects of the issue in the email body,  ie. Errors, exceptions, failures, screenprints.  The email will generate a support ticket the Developer Environments team will look at.

## Feature Enhancements

For feature enhancements, please open a PR against the repository.  Maintainers of the repository will be notified and review the PR.