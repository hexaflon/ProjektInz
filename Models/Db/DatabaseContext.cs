using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestTest.Models.Db;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kategoria> Kategorie { get; set; }

    public virtual DbSet<Odpowiedzi> Odpowiedzi { get; set; }

    public virtual DbSet<Pytanie> Pytania { get; set; }

    public virtual DbSet<TypPytanium> TypPytan { get; set; }

    public virtual DbSet<User> Users { get; set; }
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Database;Trusted_Connection=True;");
    */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kategoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__kategori__3214EC072585D7C7");

            entity.ToTable("kategoria");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nazwa).HasColumnType("text");
        });

        modelBuilder.Entity<Odpowiedzi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__odpowied__3214EC07C2F224BC");

            entity.ToTable("odpowiedzi");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CzyPoprawna).HasColumnName("Czy_Poprawna");
            entity.Property(e => e.IdPytania).HasColumnName("Id_Pytania");
            entity.Property(e => e.Odpowiedz).HasColumnType("text");

            entity.HasOne(d => d.IdPytaniaNavigation).WithMany(p => p.Odpowiedzis)
                .HasForeignKey(d => d.IdPytania)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__odpowiedz__Id_Py__31EC6D26");
        });

        modelBuilder.Entity<Pytanie>(entity =>
        {
            entity.ToTable("pytanie");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IdKategorii).HasColumnName("Id_Kategorii");
            entity.Property(e => e.IdTypPytania).HasColumnName("Id_Typ_Pytania");
            entity.Property(e => e.Tresc).HasColumnType("text");

            entity.HasOne(d => d.IdKategoriiNavigation).WithMany(p => p.Pytanies)
                .HasForeignKey(d => d.IdKategorii)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kategoria_FK");

            entity.HasOne(d => d.IdTypPytaniaNavigation).WithMany(p => p.Pytanies)
                .HasForeignKey(d => d.IdTypPytania)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("typ_pytania_FK");
        });

        modelBuilder.Entity<TypPytanium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__typ_pyta__3214EC07009AEBF2");

            entity.ToTable("typ_pytania");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nazwa).HasColumnType("text");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3214EC0710C7D647");

            entity.ToTable("user");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.Password).HasColumnType("text");
            entity.Property(e => e.Username).HasColumnType("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
