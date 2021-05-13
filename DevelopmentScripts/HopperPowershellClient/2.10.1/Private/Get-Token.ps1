function Get-Token()
{
  [CmdletBinding()]
  param(
      [parameter(Mandatory)]
      [string]$ApiKey,

      [parameter(Mandatory)]
      [string]$ApiUsername
  )

  $payload = @{
      identities = @({user_id => $ApiUsername});
      connection = "trusted-app";
      email = $ApiUsername;
      nickname = $ApiUsername;
      given_name = $ApiUsername;
      family_name = $ApiUsername
  }

  $token = New-JWT -Algorithm 'HS256' -type 'JWT' -SecretKey $ApiKey -payload $payload -ValidforSeconds 3600
  $token
}