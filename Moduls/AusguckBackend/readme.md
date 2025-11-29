dotnet tool install --global dotnet-ef

dotnet ef dbcontext scaffold "Host=192.168.178.143;Database=Rumpf;Username=AusguckTester;Password=dsafadsfef25ojnkoajn9oiujbasounsaejlkwsxx" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Models --context-dir Data --context RumpfDbContext --data-annotations --use-database-names

Get-Content .\backend.env | ForEach-Object {
    $parts = $_ -split '=', 2
    [System.Environment]::SetEnvironmentVariable($parts[0], $parts[1])
}

dotnet run