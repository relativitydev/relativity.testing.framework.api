function Get-Instance
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

        [Parameter(ParameterSetName = "ByCorrelationId")]
        [string] $CorrelationId,

        [Parameter(ParameterSetName = "ById")]
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
        if ($PSCmdlet.ParameterSetName -eq "ById")
        {
            Write-Verbose "Retrieving instance with Id $Id"

            $uri = Join-Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/v2/instance/$id"

            $parameters = @{
                Method = 'Get'
                Headers = $global:hopperHeaders
                ContentType = 'application/json'
                Uri = $uri
            }

            try
            {
                $instance =  Invoke-RestMethodWithRetries -Parameters $parameters -Verbose:$VerbosePreference
                Write-Verbose "Found instance with Id: $Id"
                $instance
            }
            catch
            {
                Write-Verbose "Could not find instance with Id: $Id"
                $null
            }
        }
        else
        {
            if ($CorrelationId)
            {
                $filters = @{ CorrelationId = $CorrelationId }
            }
            else
            {
                $filters = @{} # Need to provide this or we get a 500.
            }

            $reqOptions = @{
                'ItemsPerPage' = 1;
                "SortProperty" = "-Id";
                "PageIndex" = 0;
                "Filters" = $filters;
            }

            # This endpoint returns the same object as /v2/instance/{instanceId}, but seems to have
            # more details filled out, so we just get all the Ids then filter down if necessary.

            $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/v2/instance"

            $parameters = @{
                Method = 'Post'
                Headers = $global:hopperHeaders
                ContentType = 'application/json'
                Uri = $uri
                Body = ($reqOptions | ConvertTo-Json)
            }

            Write-Verbose "Retrieving instance with correlation ID $CorrelationId"

            try {
                $response =  Invoke-RestMethodWithRetries -Parameters $parameters -Verbose:$VerbosePreference

                $instance = $response.Instances[0]
                Write-Verbose "Found instance using correlation ID $CorrelationId`: $($instance.Id)"
                
                return $instance
            }
            catch {
                Write-Verbose "Could not find instance with correlation ID $CorrelationId"
                $null
            }
            
        }
    }
}