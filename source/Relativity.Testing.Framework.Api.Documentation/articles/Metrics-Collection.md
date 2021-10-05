# Metrics

Relativity Testing Framework API collects metrics using [ApplicationInsights](https://docs.microsoft.com/pl-pl/azure/azure-monitor/app/app-insights-overview) to provide more context around usage and debugging.
In addition to the [default information](https://docs.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview#what-does-application-insights-monitor) collected from [ApplicationInsights](https://docs.microsoft.com/pl-pl/azure/azure-monitor/app/app-insights-overview), we also collect information about the parameters that are sent with the API request.

## What is collected

### Default ApplicationInsights Metrics

| Measurement | Description | Example |
| ----------- | ----------- | ------- |
| Event Time | The local time of the event. | 2/12/2021, 9:23:05 AM (Local time) |
| Event Name | The name of the event. | RelativityApplicationService.IsInstalledInWorkspace |
| Telemetry Type | The type of measurement. | customEvent |
| Device Type | Browser(JavaScript) or PC. | PC |
| Client IP Address | The IP address of the client device. | 0.0.0.0 |
| City | Scraped using <https://dev.maxmind.com/geoip/geoip2/geolite2/> | Des Moines |
| State or Province | Scraped using <https://dev.maxmind.com/geoip/geoip2/geolite2/> | Iowa |
| Country or region | Scraped using <https://dev.maxmind.com/geoip/geoip2/geolite2/> | United States |
| Cloud role instance | The name of the computer running the code. | a01dpjkdeaba014.kCura.corp |
| SDK version | Version of Application insights being used. | dotnet:2.15.0-4479 |
| Sample rate | Number of samples. | 1 |

### Custom Event Metrics

| Measurement | Description | Example |
| ----------- | ------- | ----- |
| RelativityTestingFrameworkVersion | The version of RTF that is running. | 1.1.1 |
| RelativityVersion | The version of Relativity that RTF is running against. | 12.1.2.3 |
| TestAssemblyName | The name of the assembly that NUnit is running. | Relativity.Testing.Framework.Api.FunctionalTests |
| Parameters | Contains all parameters you send into a strategy. Note that object parameters will not be expanded. | 1015024 && 1018047 |
| Method | The name of the method/strategy being called. | IsInstalledInWorkspace |
| Class | The name of the class/service running. | RelativityApplicationService |
| ProcessingTime | The time in the milliseconds that the method took to run. | 1123.897 |
| RingSetupVersion | The version of the Relativity.Testing.Framework.RingSetup assembly, if loaded | 0.13.0 |
| Hostname | The hostname of the Relativity environment that RTF is running against. | P-DV-VM-CUP7WET |

### API Error Metrics

| Measurement | Description | Example |
| ----------- | ------- | ----- |
| Message | Contains error message. **Might potentially contain sensitive information (information about the code being run)** | Validation failed: -- The entered E-Mail Address is already associated with another user in the system. ... |
| Exception Type | Contains the type of exception. | System.Net.Http.HttpRequestException |
| Failed method | Contains info about a method where an exception was thrown. | Relativity.Testing.Framework.Api.HttpService.CheckResponseStatus |
| Call stack | The call stack of an error. **Might potentially contain sensitive information (information about the code being run)**. | System.Net.Http.HttpRequestException: at Relativity.Testing.Framework.Api.HttpService.CheckResponseStatus (Relativity.Testing.Framework.Api, ... |

## Opting out of Metrics Collection

If you wish to opt out of automatic metrics collection, set the _EnableApplicationInsights_ TestRunParameter to UsageOnly or None.

```text
<RunSettings>
  <TestRunParameters>
    <Parameter name="ServerBindingType" value="https" />
    <Parameter name="RelativityHostAddress" value="P-DV-VM-CUP7WET" />
    <Parameter name="AdminUsername" value="relativity.admin@kcura.com" />
    <Parameter name="AdminPassword" value="APassword1234!" />
    <Parameter name="RestServicesHostAddress" value="P-DV-VM-CUP7WET" />
    <Parameter name="WebApiHostAddress" value="P-DV-VM-CUP7WET" />
    <Parameter name="EnableApplicationInsights" value="All" />
  </TestRunParameters>
</RunSettings>
```
