<#
.SYNOPSIS
    Builds the mod locally without deploying.
.DESCRIPTION
    Finds the .csproj in the src/ folder and runs a Release build.
    DeployPath is left empty so output stays in the default bin/Release folder.
.EXAMPLE
    .\scripts\build_local.ps1
#>
[CmdletBinding()]
param()

$Root  = Resolve-Path (Join-Path $PSScriptRoot '..')
$csproj = Get-ChildItem -Path (Join-Path $Root 'src') -Filter '*.csproj' | Select-Object -First 1

if (-not $csproj) {
    Write-Error 'No .csproj file found in src/.'
    exit 1
}

dotnet build $csproj.FullName -c Release /p:DeployPath=
if ($LASTEXITCODE -ne 0) {
    Write-Error 'Build failed.'
    exit $LASTEXITCODE
}
Write-Host 'Build succeeded.'
