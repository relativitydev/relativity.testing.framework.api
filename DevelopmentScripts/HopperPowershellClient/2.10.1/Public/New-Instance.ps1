function New-Instance
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

        [Parameter(Mandatory=$true)]
        [string] $TemplateName,

        [Parameter(Mandatory=$true)]
        [string] $VmName,

        [Parameter()]
        [string] $Description = "This instance was created by a machine.",

        [Parameter()]
        [int] $TargetRegion = 3,

        [Parameter(Mandatory=$false)]
        [switch] $ForceProvision,

        [Parameter(Mandatory=$false)]
        [switch] $PassThru,

        [Parameter()]
        [hashtable] $Tags,

        [Parameter()]
        [int] $WaitTimeoutMs = [TimeSpan]::FromMinutes(10).TotalMilliseconds
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

        # Functions just for New-Instance
        function Test-ProvisioningComplete($Instance)
        {
            Test-ProvisioningSuccess $Instance -or Test-ProvisioningFailure $Instance
        }

        function Test-ProvisioningSuccess($Instance)
        {
            $Instance.State.IsRunning
        }

        function Test-ProvisioningFailure($Instance)
        {
            $Instance.State.IsStartFailed -or $Instance.State.IsProvisionFailed
        }
    }

    process
    {
        if ($Force -or $PSCmdlet.ShouldProcess("ShouldProcess?")) {

            $authParams = @{
                ApiUrl = $ApiUrl
                ApiKey = $ApiKey
                ApiUsername = $ApiUsername
                Referrer = $Referrer
            }

            $invokeCommandResult = Invoke-WithRetry -Command {
                $template = Get-Template -Name $TemplateName @authParams -Verbose:$VerbosePreference 
                if (-not $template)
                {
                    throw "Could not find template with name '$TemplateName'"
                }

                $templateId = $template.Id

                $g = New-Guid
                $correlationId = $g.ToString()

                $pool_payload = @{
                    "Name" = $VmName;
                    "NumToCreate" = 1;
                    "RegionId" = $TargetRegion;
                    "CorrelationId" = $correlationId;
                    "Description" = $Description;
                    "Tags" = $Tags;
                    "PullFromPoolIfAvailable" = (-not $ForceProvision)
                } | ConvertTo-Json

                $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/queue/template/$templateId/instance" 

                Write-Verbose "Creating instance from template with ID $templateId with correlation ID $correlationId"
                # Removing the write-verbose breaks the pipeline with incorrect json data
                $parameters = @{
                    Method = 'Post'
                    Headers = $global:hopperHeaders
                    Uri = $uri
                    Body = $pool_payload
                }
                
                Invoke-RestMethodWithRetries -Parameters $parameters -ErrorAction Stop -Verbose:$VerbosePreference | Write-Verbose

                Write-Verbose "Instance queued for creation with correlation ID $correlationId"

                # Wait for provision to start.
                $secondsToWaitForInstanceCreation = 600
                $timeSpan = New-Timespan -Seconds $secondsToWaitForInstanceCreation
                $stopwatch = [Diagnostics.StopWatch]::StartNew()
                $secondsToWaitForProvisioningStart = 5
                $provisioningStarted = $false
                Write-Verbose "Waiting for instance for correlation ID $correlationId to start provisioning"
                do {
                    $instance = Get-Instance -CorrelationId $correlationId @authParams -ErrorAction Stop -Verbose:$VerbosePreference
                    if ($instance) {
                        Write-Verbose "Instance for correlation ID $correlationId has started provisioning"
                        $provisioningStarted = $true
                    }
                    if (-not $provisioningStarted) {
                        Write-Verbose "Instance for correlation ID $correlationId has not started provisioning"
                        Start-Sleep -Seconds $secondsToWaitForProvisioningStart
                    }
                } while (-not $provisioningStarted -and $stopwatch.elapsed -lt $timeSpan)

                if (-not $provisioningStarted) {
                    throw "Instance for correlation ID $correlationId failed to provision after $timeSpan"
                }

                # Wait for provision to finish
                $provisioningFinished = $false
                $secondsToWaitForProvisioningFinished = 30
                $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/instance/$($instance.Id)/status"
                Write-Verbose "Waiting for instance for correlation ID $correlationId to finish provisioning"
                do {
                    $parameters = @{
                        Headers = $global:hopperHeaders
                        Uri = $uri
                    }
                    
                    $instanceStatus = Invoke-RestMethodWithRetries -Parameters $parameters -ErrorAction Stop 
                    # Wait until the instance: fails to Provision (11), Is Ready (2), Stopped (3), Deleting (8)
                    $provisioningFinished = @(11, 2, 3, 8) -contains $instanceStatus.Id
                    if (-not $provisioningFinished) {
                        Write-Verbose "Instance for correlation ID $correlationId has not finished provisioning (Instance status: $($instanceStatus.Id))"
                        Start-Sleep -Seconds $secondsToWaitForProvisioningFinished
                    }
                    else {
                        Write-Verbose "Instance for correlation ID $correlationId has finished provisioning (Instance status: $($instanceStatus.Id))"
                        if ($instanceStatus.Id -ne 2) { # Is Ready
                            Remove-Instance -Id $instance.Id @authParams -Verbose:$VerbosePreference
                            throw "Instance for correlation ID $correlationId failed to provision ($instanceStatus.Id)"
                        }
                    }

                } while (-not $provisioningFinished)

                # Ensure all instance metadata is correct
                $instance = Get-Instance -CorrelationId $correlationId @authParams -ErrorAction Stop -Verbose:$VerbosePreference 

                Write-Verbose "Retrieving credentials for instance with correlation ID $correlationId"

                $instanceCredentials = Get-InstanceCredentials -Id $instance.Id @authParams -ErrorAction Stop -Verbose:$VerbosePreference

                Write-Verbose "Credentials for instance with correlation ID $correlationId retrieved"

                $instance | Add-Member -Name Credentials -MemberType NoteProperty -Value $instanceCredentials
                $instance | Add-Member -Name RetryCount -MemberType NoteProperty -Value $retryCount

                $instance

                Write-Verbose "Lease for instance $($instance.Id) ends at: $($instance.LeaseEnd)"
            }

            if ($PassThru)
            {
                $invokeCommandResult
            }
        }
    }
}
