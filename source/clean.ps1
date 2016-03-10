$ErrorActionPreference = 'Stop'
$VerboseActionPreference = 'Continue'

Write-Host 'Cleaning AgileDB ...'

Write-Host 'Cleaning AgileDB database'

$sqlInstall = join-path $PSScriptRoot -ChildPath "..\sql\install.bat"
& $sqlInstall

Write-Host "Cleaning AgileDB folders"

Write-Host "`t Reading app config"

$configPath = join-path $PSScriptRoot -ChildPath "\app-config\config.json"

If (!(Test-Path $configPath)){
    Write-Host "Config file not found, exiting clean script."
}

$config = (Get-Content $configPath) -join "`n" | ConvertFrom-Json

If($config.GitRepositoriesFolder -eq $null){
    Write-Host "`t Applying default git repo folder"
    $config.GitRepositoriesFolder = join-path $env:TEMP -ChildPath dlm-git-repos
    Write-Host "`t", $config.GitRepositoriesFolder
}

If (Test-Path $config.GitRepositoriesFolder){
    Write-Host "`t Removing content of", $config.GitRepositoriesFolder
    Remove-Item -Path $config.GitRepositoriesFolder -Recurse -Force
} else {
    Write-Host "`t Path does not exist nothing to clean"
}

If($config.SnapshotsFolder -eq $null){
    Write-Host "`t Applying default snapshot folder"
    $config.SnapshotsFolder = join-path $env:TEMP -ChildPath dlm-snapshots
    Write-Host "`t", $config.SnapshotsFolder
}

If (Test-Path $config.SnapshotsFolder){
    Write-Host "`t Removing content of", $config.SnapshotsFolder
    Remove-Item -Path $config.SnapshotsFolder -Recurse -Force
} else {
    Write-Host "`t Path does not exist nothing to clean"
}