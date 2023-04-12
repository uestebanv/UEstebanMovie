using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class UestebanCineContext : DbContext
{
    public UestebanCineContext()
    {
    }

    public UestebanCineContext(DbContextOptions<UestebanCineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cine> Cines { get; set; }

    public virtual DbSet<Zona> Zonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= UEstebanCine; User ID=sa; TrustServerCertificate=True; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cine>(entity =>
        {
            entity.HasKey(e => e.IdCine).HasName("PK__Cine__394B724B1C0C325B");

            entity.ToTable("Cine");

            entity.Property(e => e.Direccion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Latitud).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Longitud).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdZonaNavigation).WithMany(p => p.Cines)
                .HasForeignKey(d => d.IdZona)
                .HasConstraintName("FK__Cine__IdZona__1A14E395");
        });

        modelBuilder.Entity<Zona>(entity =>
        {
            entity.HasKey(e => e.IdZona).HasName("PK__Zona__F631C12DA1B9CFAA");

            entity.ToTable("Zona");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
