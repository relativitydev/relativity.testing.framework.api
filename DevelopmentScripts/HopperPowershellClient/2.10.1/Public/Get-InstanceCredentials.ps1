function Get-InstanceCredentials
{
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

        [Parameter(ValueFromPipelineByPropertyName)]
        [string] $Id
    )

    begin
    {
        $ErrorActionPreference = "Stop"

        $params = @{
            ApiUrl = $ApiUrl
            ApiKey = $ApiKey
            ApiUsername = $ApiUsername
            Referrer = $Referrer
            Verbose = $VerbosePreference
        }

        Set-ModuleScopedVars @params
    }

    process
    {
        $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/instance/$Id/credentials"

        $parameters = @{
            Method = 'Get'
            Headers = $global:hopperHeaders
            Uri = $uri
        }

        Write-Verbose "Retrieving credentials for instance $Id"

        Invoke-RestMethodWithRetries -Parameters $parameters -Verbose:$VerbosePreference

        Write-Verbose "Found credentials for instance $Id"
    }
}