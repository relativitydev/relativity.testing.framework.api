function Get-Template
{
    [CmdletBinding(DefaultParameterSetName='all')]
    [OutputType([System.Collections.Hashtable])]
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
        [string] $Name
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
        if ($Name)
        {
            $filters = @{ Name = $Name }
        }
        else
        {
            $filters = @{} # Need to provide this or we get a 500.
        }

        $reqOptions = @{
            'ItemsPerPage' = 10;
            "SortProperty" = "-Id";
            "PageIndex" = 0;
            "Filters" = $filters;
        }

        $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/v2/template"

        $findTemplateParameters = @{
            Method = 'Post'
            Headers = $global:hopperHeaders
            ContentType = 'application/json'
            Uri = $uri
            Body = ($reqOptions | ConvertTo-Json)
        }

        Write-Verbose "Retrieving template ID for name $Name"

        $findTemplateResponse =  Invoke-RestMethodWithRetries -Parameters $findTemplateParameters -Verbose:$VerbosePreference

        $template = $findTemplateResponse.Templates | Where-Object {$_.Name.ToLower() -eq $Name.ToLower()}

        if(-not $template)
        {
            throw "Template with name $Name was not found."
        }

        $template_id = $template.Id

        Write-Verbose "Template named $Name was found with ID $template_id"

        $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/admin/template/$template_id"

        $getTemplateParameters = @{
            Method = 'Get'
            Headers = $global:hopperHeaders
            ContentType = 'application/json'
            Uri = $uri
        }

        Write-Verbose "Retrieving template with ID $template_id"

        $getTemplateResponse = Invoke-RestMethodWithRetries -Parameters $getTemplateParameters -Verbose:$VerbosePreference
        $getTemplateResponse.Template

        Write-Verbose "Found template with ID $template_id"
    }
}