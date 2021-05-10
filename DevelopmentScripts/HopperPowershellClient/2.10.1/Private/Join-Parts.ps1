#source: https://stackoverflow.com/questions/9593535/best-way-to-join-parts-with-a-separator-in-powershell
function Join-Parts
{
    [CmdletBinding()]
    param
    (
        $Parts = $null,
        $Separator = ''
    )

    ($Parts | ? { $_ } | % { ([string]$_).trim($Separator) } | ? { $_ } ) -join $Separator
}