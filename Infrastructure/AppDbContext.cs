using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Configuração do Value Object CPF
            modelBuilder.Entity<Cliente>()
                .OwnsOne(c => c.Cpf, cpf =>
                {
                    cpf.Property(p => p.Numero).HasColumnName("Cpf");
                });
        }
    }
}