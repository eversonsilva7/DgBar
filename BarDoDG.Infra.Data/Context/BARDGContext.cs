using BarDoDG.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarDoDG.Infra.Data.Context
{
    public partial class BARDGContext : DbContext
    {
        public BARDGContext()
        {
        }

        public BARDGContext(DbContextOptions<BARDGContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Comanda> Comanda { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemComprado> ItemComprado { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Comanda>(entity =>
            {
                entity.HasKey(e => e.IdComanda)
                    .HasName("PK_Camanda");

                entity.Property(e => e.DataAbertura).HasColumnType("datetime");

                entity.Property(e => e.DataFechamento).HasColumnType("datetime");

                entity.Property(e => e.Desconto).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ValorTotal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ValorTotalComDesconto).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Comanda)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comanda_Cliente");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.IdItem);

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Valor).HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<ItemComprado>(entity =>
            {
                entity.HasKey(e => e.IdItemComprado);

                entity.HasOne(d => d.IdComandaNavigation)
                    .WithMany(p => p.ItemComprado)
                    .HasForeignKey(d => d.IdComanda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemComprado_Comanda");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.ItemComprado)
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemComprado_Item");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
