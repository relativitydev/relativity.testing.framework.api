function Invoke-KeplerMethod
{
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [Uri] $Uri,

        [ValidateNotNull()]
        [Hashtable] $Parameters = @{},

        [string] $Method = 'Post',

        [Parameter(Mandatory)]
        [PSCredential] $Credential
    )

    process
    {
        $ErrorActionPreference = 'stop'
        $VerbosePreference = 'continue'

        $parameters = @{
            Method = $Method
            Headers = @{
                Authorization = "Basic $(New-BasicAuthorizationCredential -Credential $Credential)"
                'X-CSRF-Header' = '.'
                'X-Kepler-Version' = '2.0'
            }
            Uri = $Uri
            ContentType = 'application/json'
            Body = if ($Method -ne 'Get') { ($Parameters | ConvertTo-Json -Depth 10) } else { $null }
        }

        # NB: If Invoke-RestMethod returns an array, the array will be properly expanded by assigning
        # to a var and returning the var instead of directly returning the result of Invoke-RestMethod.
        $result = Invoke-RestMethodWithRetries -Parameters $parameters
        $result
    }
}