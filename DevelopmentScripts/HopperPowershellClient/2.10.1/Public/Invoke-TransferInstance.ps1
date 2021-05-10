function Invoke-TransferInstance
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
        [string] $Id,

        [Parameter(Mandatory)]
        [string] $OwnerUsername,

        [Parameter(Mandatory)]
        [string] $TransferUsername,

        [Parameter(Mandatory)]
        [string] $EcosystemName,

        [Parameter(Mandatory)]
        [string] $EcosystemCode,

        [Parameter(Mandatory)]
        [string] $EcosystemRoleName,

        [Parameter()]
        [switch] $DeleteIfTransferUserNotFound
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

            $authParams = @{
                ApiUrl = $ApiUrl
                ApiKey = $ApiKey
                ApiUsername = $ApiUsername
                Referrer = $Referrer
            }

            $instance = Get-Instance -Id $Id @authParams -ErrorAction Stop -Verbose:$VerbosePreference

            if ($null -eq $instance)
            {
                Write-Verbose "Instance with Id $Id does not exist, skipping the rest of the transfer logic"
                return
            }

            $loginProviderName = "kCura ADFS"
            $userFilter = @{
                Username = $TransferUsername;
                LoginProviderName = $loginProviderName;
            } | ConvertTo-Json

            try {
                $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/user/find"

                Write-Verbose "Retrieving ID for user $TransferUsername"

                
                $parameters = @{
                    Method = 'Post'
                    Headers = $global:hopperHeaders
                    ContentType = 'application/json'
                    Uri = $uri
                    Body = $userFilter
                }

                $transferUser = Invoke-RestMethodWithRetries -Parameters $parameters -ErrorAction Stop -Verbose:$VerbosePreference

                Write-Verbose "User $TransferUsername was found with ID $($transferUser.Id)"
            } catch [System.Net.WebException] {
                if ($_.Exception.Response.StatusCode.value__ -ne 404) {
                throw $_
                }
                #if not found, the API returns a 404 and Invoke-RestMethod THROWS.
            }

            if ($transferUser) {
                #found the user to transfer to!
                $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/admin/users/$($transferUser.Id)"

                Write-Verbose "Retrieving roles for user with ID $($transferUser.Id)"

                $parameters = @{
                    Method = 'Get'
                    Headers = $global:hopperHeaders
                    ContentType = 'application/json'
                    Uri = $uri
                }
                
                $roles = Invoke-RestMethodWithRetries -Parameters $parameters -ErrorAction Stop -Verbose:$VerbosePreference

                Write-Verbose "Found roles for user with ID $($transferUser.Id)`:"
                $roles.UserRoles | Select-Object RoleName, EcosystemName | Write-Verbose

                #get ecosystem

                $ecosystemFilter = @{
                    Name = $EcosystemName;
                    Code = $EcosystemCode;
                } | ConvertTo-Json

                $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/ecosystem/find"

                Write-Verbose "Retrieving ID for ecosystem named $EcosystemName"

                $parameters = @{
                    Method = 'Post'
                    Headers = $global:hopperHeaders
                    ContentType = 'application/json'
                    Uri = $uri
                    Body =  $ecosystemFilter
                }
                
                $ecosystem = Invoke-RestMethodWithRetries -Parameters $parameters -ErrorAction Stop -Verbose:$VerbosePreference

                Write-Verbose "Found ecosystem named $EcosystemName with ID $($ecosystem.Id)"

                $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/ecosystem/$($ecosystem.Id)/roles"

                Write-Verbose "Retrieving roles for ecosystem with ID $($ecosystem.Id)"

                $ecosystemRoles = Invoke-RestMethod -Uri $uri -ContentType 'application/json' -Method 'Get' -Headers $global:hopperHeaders -ErrorAction Stop -Verbose:$VerbosePreference

                Write-Verbose "Found roles for ecosystem with ID $($ecosystem.Id)`:"
                $ecosystemRoles | Select-Object RoleName | Write-Verbose

                $targetRole = $ecosystemRoles | Where-Object {$_.EcosystemName -eq $EcosystemName -and $_.RoleName -eq $EcosystemRoleName}
                if (-not $targetRole) {
                    throw "Role '$EcosystemRoleName' not found in ecosystem '$EcosystemName'"
                }

                $userRole = $roles.UserRoles | Where-Object { $_.RoleName -eq $EcosystemRoleName -and $_.EcosystemName -eq $EcosystemName }
                if (-not $userRole) {
                    Write-Verbose "User $TransferUsername does not have $EcosystemRoleName in ecosystem $EcosystemName"
                    $userFilter = @{
                        Username = $TransferUsername;
                        LoginProviderName = "kCura ADFS";
                    } | ConvertTo-Json

                    Write-Verbose "Granting role $EcosystemRoleName to user $TransferUsername in ecosystem $EcosystemName"

                    $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/admin/userrole/add/$($transferUser.Id)/$($targetRole.RoleId)"
                   
                    $parameters = @{
                        Method = 'Post'
                        Headers = $global:hopperHeaders
                        ContentType = 'application/json'
                        Uri = $uri
                    }
                    
                    Invoke-RestMethodWithRetries -Parameters $parameters -ErrorAction Stop -Verbose:$VerbosePreference
                    Write-Verbose "Role $EcosystemRoleName granted to user $TransferUsername in ecosystem $EcosystemName"
                }

                $uri = Join-Uri -Uri $global:hopperApiUrl -ChildPath "/user/$global:hopperUserId/instance/$Id/transfer"
                $parameters = @{
                    Method = 'Post'
                    Headers = $global:hopperHeaders
                    ContentType = 'application/json'
                    Uri = $uri
                    Body = ($transferUser.Id | ConvertTo-Json)
                }

                Write-Verbose "Transferring instance with ID $Id to user $TransferUsername"

                Invoke-RestMethod @parameters -Verbose:$VerbosePreference

                Write-Verbose "Instance with ID $Id transferred to user $TransferUsername"
            }
            elseif ($DeleteIfTransferUserNotFound ){
                Write-Verbose "DeleteIfTransferUserNotFound flag is set and user $TransferUsername was not found"
                Write-Verbose "Deleting instance with ID $Id"
                Remove-Instance -Id $Id -Username $OwnerUsername -ApiKey $ApiKey -Verbose:$VerbosePreference
                Write-Verbose "Instance with ID $Id was deleted"
            }
        }
    }
}