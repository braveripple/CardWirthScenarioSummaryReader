@{

    ModuleVersion = '1.0.0'
    Description = 'This is an unofficial tool to get CardWirth scenario information from the Summary file in a directory or archive file.'
    PowerShellVersion = '5.1'
    GUID = '5567929f-9a31-4aba-973d-d1aaf762f11b'
    
    Author = 'braveripple'
    CompanyName = 'braveripple'
    Copyright = '(c) 2021 braveripple.'

    NestedModules = @(
        '.\lib\netstandard2.0\CardWirthScenarioSummaryReader.dll',
        'CardWirthScenarioSummaryReader.psm1'
    )
    RequiredAssemblies = @(
        '.\lib\netstandard2.0\CardWirthScenarioSummaryReaderTool.dll',
        '.\lib\net\Microsoft.Deployment.Compression.dll',
        '.\lib\net\Microsoft.Deployment.Compression.Cab.dll',
        '.\lib\netstandard2.0\ICSharpCode.SharpZipLib.dll'
    )
    CmdletsToExport = @(
        'Get-CardWirthScenario',
        'Get-CardWirthScenarioList',
        'Test-CardWirthScenario'
    )
    AliasesToExport = @(
        'gcw',
        'lscw'
    )
    TypesToProcess = @('CardWirthScenarioSummaryReader.types.ps1xml')
    FormatsToProcess = @('CardWirthScenarioSummaryReader.format.ps1xml')

    PrivateData = @{
        PSData = @{
            Tags = @('CardWirth', 'PSEdition_Core', 'PSEdition_Desktop', 'Windows')
            LicenseUri = 'https://github.com/braveripple/CardWirthScenarioSummaryReader/blob/master/LICENSE'
            ProjectUri = 'https://github.com/braveripple/CardWirthScenarioSummaryReader'
            # IconUri = ''
            ReleaseNotes = 'https://github.com/braveripple/CardWirthScenarioSummaryReader/releases'
        }
    }
}

