Set-Variable -Name "IPFS_VERSION" -Value "v0.4.10"
#WGET "https://ipfs.io/ipns/dist.ipfs.io/go-ipfs/$($IPFS_VERSION)/go-ipfs_$($IPFS_VERSION)_windows-amd64.zip" -OutFile ipfs-amd64.zip 
#WGET "https://ipfs.io/ipns/dist.ipfs.io/go-ipfs/$($IPFS_VERSION)/go-ipfs_$($IPFS_VERSION)_windows-386.zip" -OutFile ipfs-386.zip 
Add-Type -AssemblyName System.IO.Compression.FileSystem
function Unzip
{
    param([string]$zipfile, [string]$outpath)

    [System.IO.Compression.ZipFile]::ExtractToDirectory($zipfile, $outpath)
}
$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
Unzip "$($PSScriptRoot)\ipfs-amd64.zip" "$($PSScriptRoot)\ipfs-amd64"
Unzip "$($PSScriptRoot)\ipfs-386.zip" "$($PSScriptRoot)\ipfs-386"
