using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MiddlewareAPI.Models
{
    public partial class RPAdatabaseContext : DbContext
    {
        public RPAdatabaseContext()
        {
        }

        public RPAdatabaseContext(DbContextOptions<RPAdatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BotsTable> BotsTables { get; set; }
        public virtual DbSet<PlatformBotTable> PlatformBotTables { get; set; }
        public virtual DbSet<PlatformTable> PlatformTables { get; set; }
        public virtual DbSet<RefreshTokenTable> RefreshTokenTables { get; set; }
        public virtual DbSet<TriggerTable> TriggerTables { get; set; }
        public virtual DbSet<UserPlatformTable> UserPlatformTables { get; set; }
        public virtual DbSet<UserTable> UserTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-Q84TE5T7\\NAELINSQL;Database=RPAdatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BotsTable>(entity =>
            {
                entity.Property(e => e.BotId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Bot)
                    .WithOne(p => p.BotsTable)
                    .HasForeignKey<BotsTable>(d => d.BotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BotsTable_PlatformBotTable");

                entity.HasOne(d => d.BotNavigation)
                    .WithOne(p => p.BotsTable)
                    .HasForeignKey<BotsTable>(d => d.BotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BotsTable_TriggerTable");
            });

            modelBuilder.Entity<PlatformBotTable>(entity =>
            {
                entity.Property(e => e.BotId).ValueGeneratedNever();

                entity.HasOne(d => d.Platform)
                    .WithMany(p => p.PlatformBotTables)
                    .HasForeignKey(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlatformBotTable_PlatformTable");
            });

            modelBuilder.Entity<PlatformTable>(entity =>
            {
                entity.Property(e => e.PlatformId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Platform)
                    .WithOne(p => p.PlatformTable)
                    .HasForeignKey<PlatformTable>(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlatformTable_UserPlatformTable");
            });

            modelBuilder.Entity<RefreshTokenTable>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokenTables)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshTokenTable_UserTable");
            });

            modelBuilder.Entity<TriggerTable>(entity =>
            {
                entity.Property(e => e.BotId).ValueGeneratedNever();
            });

            modelBuilder.Entity<UserPlatformTable>(entity =>
            {
                entity.Property(e => e.PlatformId).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPlatformTables)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPlatformTable_UserTable");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
