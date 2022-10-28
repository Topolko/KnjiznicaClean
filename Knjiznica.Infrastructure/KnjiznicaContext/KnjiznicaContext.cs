using Knjiznica.Core.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Knjiznica.Infrastructure.DBContext
{
    public partial class KnjiznicaContext : DbContext
    {
        public KnjiznicaContext()
        {
        }

        public KnjiznicaContext(DbContextOptions<KnjiznicaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autor> Autors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; } = null!;
        public virtual DbSet<Rent> Rents { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.AutorId, "IX_AutorId");

                entity.HasIndex(e => e.GenreId, "IX_GenreId");

                entity.HasOne(d => d.Autor)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AutorId)
                    .HasConstraintName("FK_dbo.Books_dbo.Autors_AutorId");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_dbo.Books_dbo.Genres_GenreId");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<Rent>(entity =>
            {
                entity.HasIndex(e => e.BookId, "IX_BookId");

                entity.HasIndex(e => e.MemberId, "IX_MemberId");

                entity.Property(e => e.DateRented).HasColumnType("datetime");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Rents)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_dbo.Rents_dbo.Books_BookId");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Rents)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_dbo.Rents_dbo.Members_MemberId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

