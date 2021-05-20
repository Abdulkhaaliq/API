using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MiddlewareAPI.DataContext;

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

        public virtual DbSet<BotTable> BotTables { get; set; }
        public virtual DbSet<PlatformBotTable> PlatformBotTables { get; set; }
        public virtual DbSet<PlatformTable> PlatformTables { get; set; }
        public virtual DbSet<RefreshTokenTable> RefreshTokenTables { get; set; }
        public virtual DbSet<UserPlatformTable> UserPlatformTables { get; set; }
        public virtual DbSet<UserTable> UserTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-R5TUG1FC\\SQLEXPRESS;Database=RPAdatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<PlatformBotTable>(entity =>
            {
                entity.HasKey(e => new { e.BotId, e.PlatformId })
                    .HasName("PK_PlatformBotTable_1");

                entity.HasOne(d => d.Bot)
                    .WithMany(p => p.PlatformBotTables)
                    .HasForeignKey(d => d.BotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlatformBotTable_BotTable");

                entity.HasOne(d => d.Platform)
                    .WithMany(p => p.PlatformBotTables)
                    .HasForeignKey(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlatformBotTable_PlatformTable");
            });

            modelBuilder.Entity<RefreshTokenTable>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokenTables)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshTokenTable_UserTable");
            });

            modelBuilder.Entity<UserPlatformTable>(entity =>
            {
                entity.Property(e => e.PlatformId).ValueGeneratedNever();

                entity.HasOne(d => d.Platform)
                    .WithOne(p => p.UserPlatformTable)
                    .HasForeignKey<UserPlatformTable>(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPlatformTable_PlatformTable");

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
