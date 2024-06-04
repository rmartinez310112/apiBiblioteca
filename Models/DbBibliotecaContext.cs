using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Models;

public partial class DbBibliotecaContext : DbContext
{
    public DbBibliotecaContext()
    {
    }

    public DbBibliotecaContext(DbContextOptions<DbBibliotecaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioBiblioteca> UsuarioBibliotecas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A107EB9AC2F");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.IdLibro).HasName("PK__Libro__3E0B49ADC4FEF787");

            entity.ToTable("Libro");

            entity.Property(e => e.Autor)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.oCategoria).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Libro__IdCategor__52593CB8");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.IdPrestamo).HasName("PK__Prestamo__6FF194C07F1A735C");

            entity.ToTable("Prestamo");

            entity.Property(e => e.EstadoPrestamo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaDevolucion).HasColumnType("datetime");
            entity.Property(e => e.FechaPrestamo)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.oLibro).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdLibro)
                .HasConstraintName("FK__Prestamo__IdLibr__6383C8BA");

            entity.HasOne(d => d.oUsuarioBiblioteca).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdUsuarioBiblioteca)
                .HasConstraintName("FK__Prestamo__IdUsua__628FA481");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97BF302388");

            entity.ToTable("Usuario");

            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UsuarioBiblioteca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsuarioB__3214EC073EFAA399");

            entity.ToTable("UsuarioBiblioteca");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
