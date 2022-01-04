﻿Add-Migration Initial -Context DataContext -OutputDir Data/DataDb
Update-Database -Context DataContext
Add-Migration PersistedGrantDbMigration -Context PersistedGrantDbContext -OutputDir Data/PersistedGrantDb
Update-Database -Context PersistedGrantDbContext
Add-Migration ConfigurationDbMigration -Context ConfigurationDbContext -OutputDir Data/ConfigurationDb
Update-Database -Context ConfigurationDbContext

Drop-Database -Context DataContext

//Test1@gmail.com
//12qw!Q


Add-Migration PersistedGrantDbMigration -StartupProject Marketplace.Web -Context PersistedGrantDbContext -OutputDir Marketplace.DB/Data/PersistedGrantDb

dotnet ef --project Marketplace.DB --startup-project Marketplace.Web migrations add PersistedGrantDbMigration -c PersistedGrantDbContext

dotnet ef -r Marketplace.DB --startup-project Marketplace.Web migrations add PersistedGrantDbMigration -c PersistedGrantDbContext

