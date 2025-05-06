using Microsoft.EntityFrameworkCore;
using prototipo1204.Models;

namespace prototipo1204.Data
{
    public class oceanidDBContext : DbContext
    {

        public oceanidDBContext(DbContextOptions<oceanidDBContext> options) : base(options) { }

        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Adm> Adms { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<ItemPedido> ItemPedido { get; set; }
        public DbSet<Pagamento> Pagamento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração das tabelas
            modelBuilder.Entity<Endereco>().ToTable("tbEndereco");
            modelBuilder.Entity<Adm>().ToTable("tbAdm");
            modelBuilder.Entity<Cliente>().ToTable("tbCliente");
            modelBuilder.Entity<Login>().ToTable("tbLogin");
            modelBuilder.Entity<Produto>().ToTable("tbProduto");
            modelBuilder.Entity<Categoria>().ToTable("tbCategoria");
            modelBuilder.Entity<Pedido>().ToTable("tbPedido");
            modelBuilder.Entity<ItemPedido>().ToTable("tbItemPedido");
            modelBuilder.Entity<Pagamento>().ToTable("tbPagamento");

            // Configuração das chaves primárias
            modelBuilder.Entity<Endereco>().HasKey(e => e.idEnd);
            modelBuilder.Entity<Adm>().HasKey(a => a.idAdm);
            modelBuilder.Entity<Cliente>().HasKey(c => c.idCliente);
            modelBuilder.Entity<Login>().HasKey(log => log.idLogin);
            modelBuilder.Entity<Produto>().HasKey(pr => pr.idProd);
            modelBuilder.Entity<Categoria>().HasKey(ca => ca.idCategoria);
            modelBuilder.Entity<Pedido>().HasKey(p => p.idPed);
            modelBuilder.Entity<ItemPedido>().HasKey(ip => ip.idProdutoPedido);
            modelBuilder.Entity<Pagamento>().HasKey(pg => pg.idPag);

            // Relacionamento 1:N entre Cliente e Endereco
            modelBuilder.Entity<Cliente>()
                    .HasOne(c => c.endereco)
                    .WithMany(e => e.clientes)
                    .HasForeignKey(c => c.idEnd)
                    .IsRequired(false);

            //----------------------APARTIR DAQUI PODE DAR MEEERRDAAA-------------------

            // Relacionamento Pedido-Endereco
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Endereco)
                .WithMany(e => e.Pedido)
                .HasForeignKey(p => p.idEnd)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Pedido-Pagamento
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Pagamento)
                .WithMany(pg => pg.Pedido)
                .HasForeignKey(p => p.idPag)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Pedido-Cliente
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedido)
                .HasForeignKey(p => p.idCliente)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento ItemPedido-Pedido
            modelBuilder.Entity<ItemPedido>()
                .HasOne(ip => ip.Pedido)
                .WithMany(p => p.ItemPedido)
                .HasForeignKey(ip => ip.idPedido)
                .OnDelete(DeleteBehavior.Restrict);

            // Corrigido: relacionamento ItemPedido-Produto
            modelBuilder.Entity<ItemPedido>()
                .HasOne(ip => ip.Produto)
                .WithMany(pr => pr.ItemPedido)
                .HasForeignKey(ip => ip.idProd)
                .OnDelete(DeleteBehavior.Restrict);

            //  Relacionamento Produto-Categoria
            modelBuilder.Entity<Produto>()
                .HasOne(pr => pr.Categoria)
                .WithMany(ca => ca.Produtos)
                .HasForeignKey(p => p.idCategoria)
                .OnDelete(DeleteBehavior.Restrict);

            //RELACIONAMENTO DOO LOOOGGGINN 
            modelBuilder.Entity<Login>()
                 .HasOne(log => log.cliente)
                 .WithMany(c => c.Login)
                 .HasForeignKey(log => log.idCliente)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Login>()
                .HasOne(log => log.adm)
                .WithMany(a => a.Login)
                .HasForeignKey(log => log.idAdm)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
