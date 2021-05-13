function Stop-Instance
{
    [CmdletBinding(SupportsShouldProcess, ConfirmImpact='Medium')]
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
        if ($Force -or $PSCmdlet.ShouldProcess("ShouldProcess?")) {

          $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/queue/instance/$Id/stop"

          $parameters = @{
              Method = 'Post'
              Headers = $global:hopperHeaders
              Uri = $uri
          }

          Write-Verbose "Stopping instance with ID $Id"

          Invoke-RestMethodWithRetries -Parameters $parameters -Verbose:$VerbosePreference 

          Write-Verbose "Instance with ID $Id stopped"
        }
    }
}