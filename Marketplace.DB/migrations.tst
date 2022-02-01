Add-Migration Initial -Context DataContext -OutputDir Data/DataDb
Update-Database -Context DataContext
Add-Migration PersistedGrantDbMigration -Context PersistedGrantDbContext -OutputDir Data/PersistedGrantDb
Update-Database -Context PersistedGrantDbContext
Add-Migration ConfigurationDbMigration -Context ConfigurationDbContext -OutputDir Data/ConfigurationDb
Update-Database -Context ConfigurationDbContext

Update-Database -Context DataContext
Update-Database -Context PersistedGrantDbContext
Update-Database -Context ConfigurationDbContext

Drop-Database -Context DataContext

//AdministratorAll
//AdmShop2
//12qw!Q
Успех:
return Ok() ← Код статуса HTTP 200
return Created() ← Код статуса HTTP 201
return NoContent(); ← Код статуса HTTP 204
Ошибка клиента:
return BadRequest(); ← Код статуса HTTP 400
return Unauthorized(); ← Код статуса HTTP 401
return NotFound(); ← Код статуса HTTP 404

Add-Migration PersistedGrantDbMigration -StartupProject Marketplace.Web -Context PersistedGrantDbContext -OutputDir Marketplace.DB/Data/PersistedGrantDb

dotnet ef --project Marketplace.DB --startup-project Marketplace.Web migrations add PersistedGrantDbMigration -c PersistedGrantDbContext

dotnet ef -r Marketplace.DB --startup-project Marketplace.Web migrations add PersistedGrantDbMigration -c PersistedGrantDbContext
