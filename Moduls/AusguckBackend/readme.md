dotnet tool install --global dotnet-ef

dotnet ef dbcontext scaffold "Host=192.168.178.143;Database=Rumpf;Username=AusguckTester;Password=dsafadsfef25ojnkoajn9oiujbasounsaejlkwsxx" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Models --context-dir Data --context RumpfDbContext --data-annotations --use-database-names