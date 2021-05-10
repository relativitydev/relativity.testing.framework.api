function Reset-InstanceLease
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
            
            $params = @{
                ApiUrl = $ApiUrl
                ApiKey = $ApiKey
                ApiUsername = $ApiUsername
                Referrer = $Referrer
                Id = $Id
                Verbose = $VerbosePreference
            }

            $instance = Get-Instance @params -ErrorAction Stop

            if ($null -eq $instance)
            {
                Write-Verbose "Instance with Id $Id does not exist, skipping the rest of the renew lease logic"
                return
            }
            
            $renewLeaseHeaders = @{
                'Accept' = 'application/json, text/plain, */*';
                'Content-Type' = 'application/json';
            }

            if($instance.RenewableLease -and $instance.LeaseEnd) {
                $utcLeaseEnd = $instance.LeaseEnd
                $token = Get-ResourceRefreshToken -UserId $global:hopperUserId -TokenExpirationTime $utcLeaseEnd -ResourceExpirationTime $utcLeaseEnd -ResourceId $Id
                $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/instance/renewlease?token=$token"

                $parameters = @{
                    Method = 'Get'
                    Headers = $renewLeaseHeaders
                    Uri = $uri
                }
    
                Write-Verbose "Renewing lease on instance with ID $Id"
    
                Invoke-RestMethodWithRetries -Parameters $parameters -Verbose:$VerbosePreference 
    
                Write-Verbose "Lease renewed on instance with ID $Id"
            } else {
                Write-Warning "Instance does not have renewable lease"
            }
        }
    }
}