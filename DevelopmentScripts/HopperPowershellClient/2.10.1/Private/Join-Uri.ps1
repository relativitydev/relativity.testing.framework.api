function Join-Uri
{
    [CmdletBinding()]
    param
    (
        [string]$Uri,
        [string]$ChildPath
    )

    $uri = Join-Parts -Parts $uri, $ChildPath -Separator '/'
    $uri
}