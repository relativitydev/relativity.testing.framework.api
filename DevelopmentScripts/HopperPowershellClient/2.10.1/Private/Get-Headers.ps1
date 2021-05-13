function Get-Headers() {
  [CmdletBinding()]
  param(
    [Parameter(Mandatory = $true)]
    [string]$token,

    [Parameter()]
    [string]$Referrer
  )

  if ([string]::IsNullOrWhiteSpace($Referrer))
  {
      $Referrer = 'https://homeimprovement.relativity.com/'
      Write-Verbose "No Referrer specified, using: $Referrer"
  }

  $headers = @{
    'Authorization' = "Bearer $token";
    'Accept' = 'application/json, text/plain, */*';
    'Content-Type' = 'application/json';
    'Referer' = $Referrer;
  }

  $headers
}