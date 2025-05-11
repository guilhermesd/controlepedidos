using Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Tests.Integracao.Infrastructure.Migrations
{
    public class MigrationIntegrationTests
    {
        //[Fact]
        //public async Task Migration_DeveCriarBancoDeDados_ComTabelasEsperadas()
        //{
        //    // Necessário para inicializar o SQLite
        //    Batteries_V2.Init();

        //    var connection = new SqliteConnection("DataSource=:memory:");
        //    connection.Open();

        //    var options = new DbContextOptionsBuilder<AppDbContext>()
        //        .UseSqlite(connection)
        //        .Options;

        //    using var context = new AppDbContext(options);
        //    await context.Database.MigrateAsync();

        //    // Asserts de verificação de tabelas
        //    var tabelas = await context.Database.ExecuteSqlRawAsync("SELECT name FROM sqlite_master WHERE type='table'");
        //    Assert.True(tabelas > 0);
        //}
    }
}
