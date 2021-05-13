@{

  # Script module or binary module file associated with this manifest.
  RootModule = 'HopperPowershellClient.psm1'

  # Version number of this module.
  ModuleVersion = '2.10.1'

  # ID used to uniquely identify this module
  GUID = 'd322f6f9-44ac-471e-a18f-896a363255dd'

  # Author of this module
  Author = 'Home Improvement'

  # Company or vendor of this module
  CompanyName = 'Relativity ODA LLC'

  # Copyright statement for this module
  Copyright = '(c) Relativity ODA LLC'

  # Description of the functionality provided by this module
  Description = 'A module to call the Hopper API'

  # Minimum version of the Windows PowerShell engine required by this module
  PowerShellVersion = '5.0'

  # Functions to export from this module
  FunctionsToExport = @(
    'Find-InstanceFromTemplate',
    'Get-HopperApiUrl'
    'Get-Instance',
    'Get-InstanceCredentials',
    'Get-Template',
    'Invoke-KeplerFileStream',
    'Invoke-KeplerMethod'
    'Invoke-TransferInstance',
    'New-Instance',
    'Remove-Instance',
    'Reset-InstanceLease',
    'Stop-Instance'
  )

  RequiredModules = @(
  )
}