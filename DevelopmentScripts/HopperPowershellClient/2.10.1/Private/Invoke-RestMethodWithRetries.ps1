function Invoke-RestMethodWithRetries {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $True)]
        [hashtable] $Parameters,

        [parameter()]
        [int]$RetryDelay = 45,

        [parameter()]
        [int]$MaxRetries = 3,

        [parameter()]
        [int[]]$NonFatalErrorCodes = @()
    )

    Invoke-WithRetry -RetryDelay $RetryDelay  -MaxRetries $MaxRetries -Command {
        try
        {
            $result = Invoke-RestMethod @Parameters -Verbose:$VerbosePreference
        }
        catch
        {
            $statusCode = [int] (Get-WebExceptionStatusCode $_)
            if ($statusCode -in $NonFatalErrorCodes)
            {
                Write-Verbose "Request failed with status code ${statusCode}; this is marked as non-fatal, so continuing without error"
                return $null
            }
            else
            {
                throw
            }
        }

        return $result
    }
}