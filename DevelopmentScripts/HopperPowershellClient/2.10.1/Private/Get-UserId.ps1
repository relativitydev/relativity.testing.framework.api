function Get-UserId () {
  [CmdletBinding()]
  param(
    [parameter(Mandatory)]
    [string]$ApiUrl,

    [parameter(Mandatory)]
    [hashtable] $headers
  )
  $uri = Join-Uri -Uri $ApiUrl -ChildPath "/account/id"

  
  $parameters = @{
    Method = 'Get'
    Headers = $headers
    Uri = $uri
    ContentType = 'application/json'
}
  $UserId = Invoke-RestMethodWithRetries -Parameters $parameters -ErrorAction Stop
  $UserId
}