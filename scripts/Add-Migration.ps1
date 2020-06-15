Param(
    # The name of the migration.    
    [Parameter(Mandatory = $true)]
    [ValidateNotNullOrEmpty()]
    [string] $MigrationName
)
[string] $DataProjectFolderRelativePath = ./Get-DataProjectFolderRelativePath.ps1
dotnet ef migrations add $MigrationName --project $DataProjectFolderRelativePath
