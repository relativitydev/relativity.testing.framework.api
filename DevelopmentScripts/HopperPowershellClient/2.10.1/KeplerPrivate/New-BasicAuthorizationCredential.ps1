function New-BasicAuthorizationCredential()
{
    [CmdletBinding(SupportsShouldProcess, ConfirmImpact='Medium')]
    [OutputType([String])]
    Param(
        [PSCredential] $Credential
    )

    Process {
        if ($Force -or $PSCmdlet.ShouldProcess("ShouldProcess?")) {
            $bytes = [System.Text.Encoding]::UTF8.GetBytes("$($Credential.Username):$($Credential.GetNetworkCredential().Password)")
            [Convert]::ToBase64String($bytes)
        }
    }
}