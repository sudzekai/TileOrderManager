using DAL.EfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.EfCore.Data;

public partial class TileOrderManagerDbContext : DbContext
{
    public TileOrderManagerDbContext()
    {
    }

    public TileOrderManagerDbContext(DbContextOptions<TileOrderManagerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Tile> Tiles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("TILE_ORDER_MANAGER_DB_CONNECTION_STRING"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(20, 2)");

            entity.HasOne(d => d.Tile).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Tile");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Review_1");

            entity.ToTable("Review");

            entity.Property(e => e.Text)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Order).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Review_Order");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Review_User");
        });

        modelBuilder.Entity<Tile>(entity =>
        {
            entity.ToTable("Tile");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasDefaultValue("Не указано", "DF_Tile_Description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Client");

            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DialogType).HasDefaultValue(0, "DF_User_DialogType");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValue("Не указан", "DF_User_Email");
            entity.Property(e => e.FullName)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasDefaultValue("Не указан", "DF_User_FullName");
            entity.Property(e => e.LastMessageId).HasDefaultValue(0, "DF_User_LastMessageId");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasDefaultValue("Не указан", "DF_User_Phone");
            entity.Property(e => e.Role).HasDefaultValue(0, "DF_User_Role");
            entity.Property(e => e.Step).HasDefaultValue(0, "DF_User_Step");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Tile).WithMany(p => p.Users)
                .HasForeignKey(d => d.TileId)
                .HasConstraintName("FK_User_Tile");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
