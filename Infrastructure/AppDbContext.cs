using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
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

            modelBuilder.Entity<Pagamento>()
                .HasOne(p => p.Pedido)
                .WithMany(p => p.Pagamentos)
                .HasForeignKey(p => p.PedidoId);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)        
                .WithMany(c => c.Pedidos)      
                .HasForeignKey(p => p.ClienteId) 
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}