if (-not ([System.Management.Automation.PSTypeName]'System.Net.Http.HttpClient').Type)
{
    Add-Type -AssemblyName System.Net.Http
}

function Invoke-KeplerFileStream
{
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [Uri] $Uri,

        [Parameter(Mandatory)]
        [string] $File,

        [Parameter(Mandatory)]
        [string] $StreamParameterName,

        [ValidateNotNull()]
        [Hashtable] $OtherParameters = @{},

        [Parameter(Mandatory)]
        [PSCredential] $Credential
    )

    begin
    {
        function New-JsonContent
        {
            [CmdletBinding(SupportsShouldProcess, ConfirmImpact='Medium')]
            [OutputType([System.Net.Http.StreamContent])]
            Param(
                [object] $Content
            )
            Process {
                if ($Force -or $PSCmdlet.ShouldProcess("ShouldProcess?")) {

                    $contentBody = $Content | ConvertTo-Json -Compress
                    $jsonStream = [System.IO.MemoryStream]::new([System.Text.Encoding]::UTF8.GetBytes($contentBody))
                    $jsonContentDisposition = [System.Net.Http.Headers.ContentDispositionHeaderValue]::new('form-data')
                    $jsonContentDisposition.Name = 'json'
                    #$jsonContentDisposition.FileName = 'lksdhjfklsedjflk'
                    $jsonData = [System.Net.Http.StreamContent]::new($jsonStream)
                    $jsonData.Headers.ContentDisposition = $jsonContentDisposition
                    $jsonData.Headers.ContentType = [System.Net.Http.Headers.MediaTypeHeaderValue]::Parse('application/json')

                    $jsonData
                }
            }
        }

        function New-FileContent
        {
            [CmdletBinding(SupportsShouldProcess, ConfirmImpact='Medium')]
            [OutputType([System.Net.Http.StreamContent])]
            Param(
                [string] $Path, [string] $ParameterName
            )
            Process {
                if ($Force -or $PSCmdlet.ShouldProcess("ShouldProcess?")) {
                    if ([System.IO.Path]::IsPathRooted($Path))
                    {
                        $fullFilePath = $Path
                    }
                    else
                    {
                        $fullFilePath = Join-Path (Get-Location) $Path
                    }

                    $fileStream = [System.IO.FileStream]::new($fullFilePath, [System.IO.FileMode]::Open)
                    $fileContentDisposition = [System.Net.Http.Headers.ContentDispositionHeaderValue]::new('form-data')
                    $fileContentDisposition.Name = $ParameterName
                    $fileContentDisposition.FileName = Split-Path -Leaf $fullFilePath
                    $fileData = [System.Net.Http.StreamContent]::new($fileStream)
                    $fileData.Headers.ContentDisposition = $fileContentDisposition
                    $fileData.Headers.ContentType = [System.Net.Http.Headers.MediaTypeHeaderValue]::Parse('application/octet-stream')

                    $fileData
                }
            }
        }
    }

    process
    {

        $ErrorActionPreference = 'stop'
        $VerbosePreference = 'continue'

        if (-not (Test-Path -PathType Leaf $File))
        {
            throw "Could not find file '$File'"
        }

        $jsonData = New-JsonContent $OtherParameters
        $fileData = New-FileContent $File $StreamParameterName

        $formData = [System.Net.Http.MultipartFormDataContent]::new()
        $formData.Add($jsonData)
        $formData.Add($fileData)

        # Block stolen from: http://blog.majcica.com/2016/01/13/powershell-tips-and-tricks-multipartform-data-requests/
        # NB: This can be replaced by Invoke-WebRequest/Invoke-RestMethod if you are using Powershell >= 6.0. See the
        #     last example in the documentation for Invoke-WebRequest:
        #     https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/invoke-webrequest?view=powershell-6#examples
        try
        {
            $client = [System.Net.Http.HttpClient]::new()
            $basicAuthCred = New-BasicAuthorizationCredential -Credential $Credential
            $client.DefaultRequestHeaders.Authorization = [System.Net.Http.Headers.AuthenticationHeaderValue]::new('Basic', $basicAuthCred)
            $client.DefaultRequestHeaders.Add('X-CSRF-Header', '.')
            $client.DefaultRequestHeaders.Add('X-Kepler-Version', '2.0')

            Write-Verbose "Beginning request against URI: $Uri"
            $response = $client.PostAsync($Uri, $formData).Result

            if ($null -ne $response)
            {

                if (!$response.IsSuccessStatusCode)
                {
                    $responseBody = $response.Content.ReadAsStringAsync().Result
                    $errorMessage = "Status code {0}. Reason {1}. Server reported the following message: {2}." -f $response.StatusCode, $response.ReasonPhrase, $responseBody

                    throw [System.Net.Http.HttpRequestException] $errorMessage
                }

                $response.Content.ReadAsStringAsync().Result
            }
            else
            {
                throw 'Received no response from server'
            }
        }
        catch [Exception]
        {
            $PSCmdlet.ThrowTerminatingError($_)
        }
        finally
        {
            if ($null -ne $client)
            {
                $client.Dispose()
            }

            if ($null -ne $response)
            {
                $response.Dispose()
            }
        }
    }
}
