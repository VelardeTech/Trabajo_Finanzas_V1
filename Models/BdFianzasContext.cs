using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Trabajo_Finanzas_V1.Models;

public partial class BdFianzasContext : DbContext
{
    public BdFianzasContext()
    {
    }

    public BdFianzasContext(DbContextOptions<BdFianzasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Configuracione> Configuraciones { get; set; }

    public virtual DbSet<OfertasVehiculare> OfertasVehiculares { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) { }
    }
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseMySql("server=localhost;port=3306;database=BD_Fianzas;uid=Noru;password=E_noru1108", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("clientes");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(32)
                .HasColumnName("contrasena");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .HasColumnName("dni");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(9)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Configuracione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("configuraciones");

            entity.HasIndex(e => e.ClienteId, "cliente_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Moneda)
                .HasColumnType("enum('Soles','Dólares')")
                .HasColumnName("moneda");
            entity.Property(e => e.PlazoGracia)
                .HasDefaultValueSql("'0'")
                .HasColumnName("plazo_gracia");
            entity.Property(e => e.TipoTasa)
                .HasColumnType("enum('Nominal','Efectiva')")
                .HasColumnName("tipo_tasa");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Configuraciones)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("configuraciones_ibfk_1");
        });

        modelBuilder.Entity<OfertasVehiculare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ofertas_vehiculares");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Marca)
                .HasMaxLength(100)
                .HasColumnName("marca");
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .HasColumnName("modelo");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.Tir)
                .HasPrecision(5, 2)
                .HasColumnName("tir");
            entity.Property(e => e.Van)
                .HasPrecision(10, 2)
                .HasColumnName("van");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pagos");

            entity.HasIndex(e => e.PrestamoId, "prestamo_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'Pendiente'")
                .HasColumnType("enum('Pendiente','Pagado')")
                .HasColumnName("estado");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.MontoCuota)
                .HasPrecision(10, 2)
                .HasColumnName("monto_cuota");
            entity.Property(e => e.NumeroCuota).HasColumnName("numero_cuota");
            entity.Property(e => e.PrestamoId).HasColumnName("prestamo_id");

            entity.HasOne(d => d.Prestamo).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.PrestamoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pagos_ibfk_1");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("prestamos");

            entity.HasIndex(e => e.ClienteId, "cliente_id");

            entity.HasIndex(e => e.OfertaVehicularId, "oferta_vehicular_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Moneda)
                .HasColumnType("enum('Soles','Dólares')")
                .HasColumnName("moneda");
            entity.Property(e => e.OfertaVehicularId).HasColumnName("oferta_vehicular_id");
            entity.Property(e => e.PeriodoGracia)
                .HasDefaultValueSql("'0'")
                .HasColumnName("periodo_gracia");
            entity.Property(e => e.Plazo).HasColumnName("plazo");
            entity.Property(e => e.TasaInteres)
                .HasPrecision(5, 2)
                .HasColumnName("tasa_interes");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestamos_ibfk_2");

            entity.HasOne(d => d.OfertaVehicular).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.OfertaVehicularId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestamos_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
