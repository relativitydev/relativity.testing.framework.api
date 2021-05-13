function Test-KeplerEndpoint
{
    [CmdletBinding()]
    [OutputType([System.Boolean])]
    param (
        [Parameter(Mandatory)]
        [Uri] $Uri,

        [Parameter(Mandatory)]
        [PSCredential] $Credential
    )

    process
    {
        $ErrorActionPreference = 'stop'
        $VerbosePreference = 'continue'

        $fullUri = $Uri
        if ($Uri.AbsolutePath -match 'Manager/*$')
        {
            $builder = New-Object System.UriBuilder($Uri)
            $builder.Path = Join-Path $builder.Path 'GetKeplerStatusAsync'
            $fullUri = $builder.Uri
        }
        elseif ($Uri.AbsolutePath -notmatch '/GetKeplerStatusAsync/*$')
        {
            $builder = New-Object System.UriBuilder($Uri)
            $partialPath = $builder.Path -replace '/[^/]+/*$', '/'
            $builder.Path = Join-Path $partialPath 'GetKeplerStatusAsync'
            $fullUri = $builder.Uri
        }

        $headers = @{
            Authorization = "Basic $(New-BasicAuthorizationCredential -Credential $Credential)"
            'X-CSRF-Header' = '.'
            'X-Kepler-Version' = '2.0'
        }
        try
        {
            Invoke-WebRequest -Method Get -Headers $headers -Uri $fullUri -UseBasicParsing -ErrorAction Stop
            $true
        }
        catch [Exception]
        {
            Write-Verbose "An error occurred while making the request"
            $_ | Write-Verbose
            $false
        }
    }
}