<#
    Returns the appropriate HTTP status code for the WebException
    associated with the given error. Accepts exceptions, ErrorRecords,
    and error variable contents. Only returns the first such status
    code in each case, and does not dig recursively into inner exceptions.
#>
function Get-WebExceptionStatusCode
{
    param (
        [Parameter(Mandatory, ValueFromPipeline, Position=0)]
        $Exception
    )

    process
    {
        if ($Exception -is [System.Management.Automation.ErrorRecord])
        {
            $rootException = $Exception.Exception
        }
        elseif ($Exception -is [System.Collections.ArrayList])
        {
            $rootException = $Exception | ForEach-Object InnerException | Where-Object { $_.Response.StatusCode } | Select-Object -First 1
        }
        else
        {
            $rootException = $Exception
        }

        $rootException.Response.StatusCode
    }
}
