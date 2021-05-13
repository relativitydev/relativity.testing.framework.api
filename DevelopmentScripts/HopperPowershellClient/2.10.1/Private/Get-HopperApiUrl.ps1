function Get-HopperApiUrl() {
  [CmdletBinding()]
  param(
      [parameter(Mandatory)]
      [string]$ApiUrl
  )

  $global:hopperApiUrl = if ($ApiUrl -ne $null) { $ApiUrl } else { $env:HopperApiUrl }
  if ($global:hopperApiUrl -eq $null) {
      throw "The ApiUrl parameter was not specified and the environment variable HOPPERAPIURL is not set.  Please use one or the other."
  }
  $global:hopperApiUrl
}
