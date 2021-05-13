function Set-ModuleScopedVars() {
  [CmdletBinding()]
  param (
    [Parameter(Mandatory = $true)]
    [string] $ApiUrl,

    [Parameter(Mandatory = $true)]
    [string] $ApiKey,

    [Parameter(Mandatory = $true)]
    [string] $ApiUsername,

    [Parameter()]
    [string] $Referrer,

    [Parameter()]
    [int] $UserId,

    [AllowEmptyString()]
    [string] $Token    
)
    #If this public method was called without a token and userId, we'll need to look them up.
    #Else, We can just use them inside the process block.
    if ([string]::IsNullOrEmpty($global:hopperApiUrl)) {
        $global:hopperApiUrl = $(if ([string]::IsNullOrEmpty($ApiUrl)) { $env:HopperApiUrl } else { $ApiUrl })
    }

    if ([string]::IsNullOrEmpty($global:hopperApiKey)) {
        $global:hopperApiKey = $(if ([string]::IsNullOrEmpty($ApiKey)) { $env:HopperApiKey } else { $ApiKey })
    }

    if ([string]::IsNullOrEmpty($global:hopperApiUserName)) {
        $global:hopperApiUserName = $(if ([string]::IsNullOrEmpty($ApiUsername)) { $env:HopperUserName } else { $ApiUsername })
    }

    if ([string]::IsNullOrEmpty($global:hopperApiKey) -eq $false) {
        # perhaps in a future version we can check the exp field and only recreate the token if expired.  -and [string]::IsNullOrEmpty($global:hopperToken)) {
        $global:hopperToken = Get-Token -ApiKey $global:hopperApiKey -ApiUserName $global:hopperApiUserName
    }
 
    #Can't cache the headers, they include the token.

    $params = @{
        Token = $global:hopperToken
        Referrer = $Referrer
    }

    $global:hopperHeaders = Get-Headers @params
    if ($global:hopperUserId -eq $null -or $global:hopperUserId -eq 0) {
        Write-Verbose "Retrieving Hopper user ID"
        $global:hopperUserId = Get-UserId -ApiUrl $global:hopperApiUrl -Headers $global:hopperHeaders -Verbose:$VerbosePreference
        Write-Verbose "Hopper user ID is $global:hopperUserId"
    }
}
