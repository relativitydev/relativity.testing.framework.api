function Find-InstanceFromTemplate
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

        [Parameter(Mandatory = $true)]
        [string] $TemplateName
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
        $reqOptions = @{
            'ItemsPerPage' = 500;
            "SortProperty" = "-Id";
            "PageIndex" = 0;
            "Filters" = @{
                "Template" = $TemplateName
            }
        }

        $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/v2/instance"

        Write-Verbose "Finding instance for template $templateName"
        
        $parameters = @{
            Method = 'Post'
            Headers = $global:hopperHeaders
            ContentType = 'application/json'
            Uri = $uri
            Body = $reqOptions | ConvertTo-Json
        }

        $response =  Invoke-RestMethodWithRetries -Parameters $parameters -Verbose:$VerbosePreference
        $instanceIds = $response.instances | Select-Object -ExpandProperty Id

        if($instanceIds.Length -gt 1){
            Write-Verbose "Found instance for template $templateName`: $instanceIds"
            return $instanceIds
        }
        else {
            Write-Verbose "Could not find any instances for template $templateName"
            return ,@()
        }     
    }
}