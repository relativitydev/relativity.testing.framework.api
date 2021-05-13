function New-JWT
    {

    [OutputType([String])]
    [CmdletBinding(SupportsShouldProcess, ConfirmImpact='Medium')]
    Param(
      [Parameter(Mandatory = $True)]
      [ValidateSet("HS256", "HS384", "HS512")]
      [string] $Algorithm,

      [Parameter()]
      [string] $Type = $null,

      [Parameter(Mandatory = $True)]
      [string] $SecretKey,

      [Parameter()]
      [hashtable]$Header = @{alg = $Algorithm; typ = $Type},

      [Parameter(Mandatory = $True)]
      [hashtable] $Payload,

      [Parameter(Mandatory = $False)]
      [int]$ValidforSeconds = 30
    )

    process
    {
        if ($Force -or $PSCmdlet.ShouldProcess("ShouldProcess?")) {
          $exp = [int][double]::parse((Get-Date -Date $((Get-Date).addseconds($ValidforSeconds).ToUniversalTime()) -UFormat %s)) # Grab Unix Epoch Timestamp and add desired expiration.
          $Payload.exp = $exp

          $headerjson = $Header | ConvertTo-Json -Compress
          $payloadjson = $Payload | ConvertTo-Json -Compress

          $headerjsonbase64 = [Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes($headerjson)).Split('=')[0].Replace('+', '-').Replace('/', '_')
          $payloadjsonbase64 = [Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes($payloadjson)).Split('=')[0].Replace('+', '-').Replace('/', '_')

          $ToBeSigned = $headerjsonbase64 + "." + $payloadjsonbase64

          $SigningAlgorithm = switch ($Algorithm) {
              "HS256" {New-Object System.Security.Cryptography.HMACSHA256}
              "HS384" {New-Object System.Security.Cryptography.HMACSHA384}
              "HS512" {New-Object System.Security.Cryptography.HMACSHA512}
          }

          $SigningAlgorithm.Key = [System.Text.Encoding]::UTF8.GetBytes($SecretKey)
          $Signature = [Convert]::ToBase64String($SigningAlgorithm.ComputeHash([System.Text.Encoding]::UTF8.GetBytes($ToBeSigned))).Split('=')[0].Replace('+', '-').Replace('/', '_')

          $token = "$headerjsonbase64.$payloadjsonbase64.$Signature"
          $token
        }
      }
}