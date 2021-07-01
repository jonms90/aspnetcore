#!/usr/bin/env pwsh
#requires -version 4

Set-StrictMode -Version 2
$ErrorActionPreference = 'Stop'

function Test-Template($templateName, $templateArgs, $templateNupkg, $isSPA) {
    $tmpDir = "$PSScriptRoot/$templateName"
    Remove-Item -Path $tmpDir -Recurse -ErrorAction Ignore
    Push-Location ..
    try {
        dotnet pack
    }
    finally {
        Pop-Location
    }

    Run-DotnetNew "--install", "$PSScriptRoot/../../../artifacts/packages/Debug/Shipping/$templateNupkg"

    New-Item -ErrorAction Ignore -Path $tmpDir -ItemType Directory
    Push-Location $tmpDir
    try {
        Run-DotnetNew $templateArgs, "--no-restore"

        if ($templateArgs -match 'F#') {
            $extension = "fsproj"
        }
        else {
            $extension = "csproj"
        }

        $proj = "$tmpDir/$templateName.$extension"
        $projContent = Get-Content -Path $proj -Raw
        $projContent = $projContent -replace ('<Project Sdk="Microsoft.NET.Sdk.Web">', "<Project Sdk=""Microsoft.NET.Sdk.Web"">
  <Import Project=""$PSScriptRoot/../test/bin/Debug/net6.0/TestTemplates/Directory.Build.props"" />
  <Import Project=""$PSScriptRoot/../test/bin/Debug/net6.0/TestTemplates/Directory.Build.targets"" />
  <PropertyGroup>
    <DisablePackageReferenceRestrictions>true</DisablePackageReferenceRestrictions>
  </PropertyGroup>")
        $projContent | Set-Content $proj
        dotnet.exe ef migrations add mvc
        dotnet.exe publish --configuration Release
        Set-Location .\bin\Release\net6.0\publish
        Invoke-Expression "./$templateName.exe"
    }
    finally {
        Pop-Location
    }
}

function Run-DotnetNew($arguments) {
    $expression = "dotnet new $arguments"
    Invoke-Expression $expression
}
