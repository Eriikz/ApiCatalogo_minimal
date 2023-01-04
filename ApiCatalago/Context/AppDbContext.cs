using ApiCatalago.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalago.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Produto>? Produtos { get; set; }
        public DbSet<Categoria>? Categoria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //fluent api
            modelBuilder.Entity<Categoria>().HasKey(c => c.CategoriaId);
            modelBuilder.Entity<Categoria>().Property(c => c.Nome).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Categoria>().Property(c => c.Descricao).HasMaxLength(150).IsRequired();

            modelBuilder.Entity<Produto>().HasKey(c => c.ProdutoId);
            modelBuilder.Entity<Produto>().Property(c => c.Nome).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Produto>().Property(c => c.Descricao).HasMaxLength(150);
            modelBuilder.Entity<Produto>().Property(c => c.imagem).HasMaxLength(100);
            modelBuilder.Entity<Produto>().Property(c => c.Preco).HasPrecision(14, 2);

            modelBuilder.Entity<Produto>()
                .HasOne<Categoria>(c => c.Categoria)
                .WithMany(p => p.produtos)
                .HasForeignKey(c => c.CategoriaId);
        }
    }
}
