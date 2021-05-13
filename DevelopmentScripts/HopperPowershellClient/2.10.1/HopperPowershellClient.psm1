<#
    Please format your verbose messages as such:
        Write-Verbose "$($MyInvocation.MyCommand.Name): <Message>"

    Which tells the caller exactly which command the message came from
#>

# if debugging, set moduleRoot to current directory
if ($MyInvocation.MyCommand.Path) {
  $moduleRoot = Split-Path -Path $MyInvocation.MyCommand.Path
}else {
  $moduleRoot = $PWD.Path
}

# Load up the dependent functions
"$moduleRoot\private\*.ps1" |
  Resolve-Path |
     Where-Object { -not ($_.ProviderPath.ToLower().Contains('.tests.')) } |
       ForEach-Object { . $_.ProviderPath }

"$moduleRoot\public\*.ps1" |
  Resolve-Path |
     Where-Object { -not ($_.ProviderPath.ToLower().Contains('.tests.')) } |
       ForEach-Object { . $_.ProviderPath }

"$moduleRoot\keplerprivate\*.ps1" |
  Resolve-Path |
    Where-Object { -not ($_.ProviderPath.ToLower().Contains('.tests.')) } |
      ForEach-Object { . $_.ProviderPath }

"$moduleRoot\keplerpublic\*.ps1" |
      Resolve-Path |
        Where-Object { -not ($_.ProviderPath.ToLower().Contains('.tests.')) } |
          ForEach-Object { . $_.ProviderPath }

Export-ModuleMember `
 Find-InstanceFromTemplate, `
 Get-Template, `
 Get-HopperApiUrl, `
 Get-Instance, `
 Get-InstanceCredentials, `
 Invoke-KeplerFileStream, `
 Invoke-KeplerMethod, `
 Invoke-TransferInstance, `
 New-Instance, `
 New-Jwt, `
 Remove-Instance, `
 Reset-InstanceLease, `
 Stop-Instance
