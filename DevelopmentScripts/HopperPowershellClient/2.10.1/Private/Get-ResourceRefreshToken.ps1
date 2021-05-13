function Get-ResourceRefreshToken {
    param
    (
        [parameter(Mandatory = $true)]
        [string] $UserId,

        [parameter(Mandatory = $true)]
        [DateTime] $TokenExpirationTime,

        [parameter(Mandatory = $true)] 
        [DateTime] $ResourceExpirationTime,

        [parameter(Mandatory = $true)] 
        [string] $ResourceId
    )
           
    $payload = @{
        userId               = $UserId
        tokenExpirationTime    = $TokenExpirationTime
        resourceExpirationTime = $ResourceExpirationTime;
        resourceId             = $ResourceId;
    } 

    $payloadjson = $payload | ConvertTo-Json -Compress
    $hash = ([System.BitConverter]::ToString((New-Object -TypeName System.Security.Cryptography.SHA512CryptoServiceProvider).ComputeHash((New-Object -TypeName System.Text.UTF8Encoding).GetBytes($payloadjson)))).Replace("-", "")
    $serializedPayloadBase64 = [Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes($payloadjson)).Split('=')[0].Replace('+', '-').Replace('/', '_')
    $hashBase64 = [Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes($hash)).Split('=')[0].Replace('+', '-').Replace('/', '_')
    $token = $serializedPayloadBase64 + "." + $hashBase64
    return $token
}