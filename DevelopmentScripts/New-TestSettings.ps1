[CmdletBinding()]
param (
    [Parameter()]
    [String]
    $ServerBindingType,

    [Parameter()]
    [String]
    $RelativityHostAddress,

    [Parameter()]
    [String]
    $RestServicesHostAddress,

    [Parameter()]
    [String]
    $WebApiHostAddress,

    [Parameter()]
    [String]
    $AdminUsername,

    [Parameter()]
    [String]
    $AdminPassword,

    [Parameter()]
    [String]
    $RAPDirectory,

    [Parameter()]
    [String]
    $RunSettingsPrefix
)

if(-not $ServerBindingType)
{
    $PSBoundParameters['ServerBindingType'] = "https"
}

if(-not $RestServicesHostAddress)
{
    $PSBoundParameters['RestServicesHostAddress'] = "$($PSBoundParameters['RelativityHostAddress'])"
}

if (-not $WebApiHostAddress)
{
    $PSBoundParameters['WebApiHostAddress'] = "$($PSBoundParameters['RelativityHostAddress'])"
}

if(-not $RAPDirectory)
{
    $PSBoundParameters['RAPDirectory'] = Join-Path $PSScriptRoot ..\Artifacts
}

Remove-Item (Join-Path $PSScriptRoot "..\$($RunSettingsPrefix)FunctionalTestSettings") -Force -ErrorAction SilentlyContinue
Remove-Item (Join-Path $PSScriptRoot "..\$($RunSettingsPrefix)FunctionalTest.runsettings") -Force -ErrorAction SilentlyContinue

[xml]$runSettingsDocument = New-Object System.Xml.XmlDocument
$runSettings = $runSettingsDocument.AppendChild($runSettingsDocument.CreateNode("element", "RunSettings", $null))
$testRunParameters = $runSettings.AppendChild($runSettingsDocument.CreateNode("element", "TestRunParameters", $null))

foreach($parameterKey in $PSBoundParameters.Keys)
{
    $parameter = $testRunParameters.AppendChild($runSettingsDocument.CreateNode("element", "Parameter", $null))
    $parameter.SetAttribute("name", $parameterKey)
    $parameter.SetAttribute("value", $PSBoundParameters[$parameterKey])
}

foreach($parameter in $testRunParameters.ChildNodes)
{
    Add-Content (Join-Path $PSScriptRoot "..\$($RunSettingsPrefix)FunctionalTestSettings") "--params $($parameter.Name)=$($parameter.Value)"
}

$runSettingsDocument.Save((Join-Path $PSScriptRoot "$($RunSettingsPrefix)FunctionalTest.runsettings"))