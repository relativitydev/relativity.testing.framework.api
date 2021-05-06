param (
    [string]$version
)

$testSettings = Join-Path $PSScriptRoot "$($version)_FunctionalTest.runsettings"
$LogFilePath = Join-Path $PSScriptRoot "./Artifacts/Logs/{assembly}.{framework}.$($version)TestResults.xml"
$ResultsPath = Join-Path $PSScriptRoot "./Artifacts/Logs/$version"

# It's not possible to add additional test run parameters with the current dotnet test (maybe just needs to be a newer version)
# So we need to set an environment variable for the ResultsPath instead.
$Env:ResultsLocation = $ResultsPath

# Code coverage also locks the dlls, so we need to have this run only once, or run code coverage at a different time.
# /p:collectcoverage=true /p:CoverletOutputFormat=cobertura
& dotnet test .\Source\Relativity.Testing.Framework.Api.sln --no-build -r $ResultsPath --filter "TestCategory=FunctionalTests" "--logger:nunit;LogFilePath=$LogFilePath" -s $testSettings
Compress-Archive -Path $ResultsPath -DestinationPath "$($ResultsPath).zip" -Force