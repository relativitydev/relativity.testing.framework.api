param(
    $ClientID,
    $ClientSecret,
    $DirectoryID,
    $VirtualMachineName,
    $HopperAPIURL,
    $HopperUsername,
    $BuildOwner,
    $ProductName,
    $SUTVersion
)

$Body = @{
  grant_type = "client_credentials"
  client_id = "$ClientID"
  client_secret = "$ClientSecret"
  scope = "https://vault.azure.net/.default"
}

$AccessToken = (Invoke-RestMethod -Method POST `
  -Uri "https://login.microsoftonline.com/$DirectoryID/oauth2/v2.0/token" `
  -Body $Body).access_token

$HopperApiKey = (Invoke-RestMethod -Method GET `
  -Uri "https://testengineering-github.vault.azure.net/secrets/HopperApiKey?api-version=7.1" `
  -Headers @{ Authorization="Bearer $AccessToken" }).value

Import-Module ./HopperPowerShellClient/*/HopperPowerShellClient.psm1

$Instance = New-Instance -ApiUrl "$HopperAPIURL" `
  -ApiKey $HopperApiKey `
  -ApiUsername "$HopperUsername" `
  -TemplateName $SUTVersion `
  -VmName "$VirtualMachineName" `
  -Description "$VirtualMachineName" `
  -Tags @{"Build Owner" = "$BuildOwner"; "Product Name" = "$ProductName"} `
  -TargetRegion 3 `
  -Referrer "https://testengineering.relativity.com/" `
  -Verbose `
  -PassThru

if(!$?) {throw "An error ocurred while creating the instance in Hopper. Please check the logs."}

Start-Sleep 180 # Environments are less likely to throw 500s if they are up for a bit before running tests.

$Credentials = $Instance.Credentials | Where-Object { $_.CredentialType.Name -eq "Relativity" }
$RelativityHostAddress = "$($instance.ServiceName).relativityhopper.com"

./New-TestSettings.ps1 -ServerBindingType "https" `
  -RelativityHostAddress $RelativityHostAddress `
  -AdminUsername $Credentials.Username `
  -AdminPassword $Credentials.Password `
  -RunSettingsPrefix $SUTVersion

$TestSettings = "$($SUTVersion)FunctionalTest.runsettings"
$LogFilePath = Join-Path (Get-Location) "Artifacts/Logs/{assembly}.{framework}.$($SUTVersion)TestResults.xml"
$ResultsPath = Join-Path (Get-Location) "Artifacts/Logs/$SUTVersion"

# This sets the location for the trace logs for the tests.
$Env:ResultsLocation = $ResultsPath

dotnet test ..\Tests\Relativity.Testing.Framework.Api.FunctionalTests.dll `
  --nologo `
  --ResultsDirectory $ResultsPath `
  "--logger:nunit;LogFilePath=$LogFilePath" `
  -s $TestSettings

Remove-Instance -ApiUrl "$HopperAPIURL" `
  -ApiKey $HopperApiKey `
  -ApiUsername "$HopperUsername" `
  -Id $Instance.Id `
  -Referrer "https://testengineering.relativity.com/" `
  -Verbose

if(!$?) {throw "An error ocurred while deleting the Hopper instance. Please check the logs."}