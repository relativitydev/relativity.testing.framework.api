function Invoke-WithRetry {
  [CmdletBinding()]
  param(
      [parameter(ValueFromPipeline,Mandatory)]
      [ScriptBlock]$Command,

      [parameter()]
      [int]$RetryDelay = 5,

      [parameter()]
      [int]$MaxRetries = 3,

      [parameter()]
      [string]$VerboseAction
  )

  if ($VerboseAction) {
    $VerbosePreference = $VerboseAction
  }

  $currentRetry = 0
  $success = $false
  $cmd = $Command.ToString()

  do {
      try {
          $result = Invoke-Command -ScriptBlock $Command
          $success = $true
          $result
      } # end try
      catch [System.Exception] {
          $currentRetry = $currentRetry + 1
          Write-Warning "Failed to execute command block, Exception Message: $($_.Exception.Message)"

          if ($currentRetry -gt $MaxRetries) {
              throw
          } # end if
          else {
              Write-Verbose "Waiting $RetryDelay second(s) before attempt #$currentRetry of command block ..."
              Start-Sleep -s $RetryDelay
          } # end else
      } # end catch
  } # end do
  while (!$success);
} # end function