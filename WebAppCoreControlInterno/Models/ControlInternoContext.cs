using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAppCoreControlInterno.Models
{
    public partial class ControlInternoContext : DbContext
    {
        public ControlInternoContext()
        {
        }

        public ControlInternoContext(DbContextOptions<ControlInternoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Antena> Antenas { get; set; }
        public virtual DbSet<Base> Bases { get; set; }
        public virtual DbSet<Cargo> Cargos { get; set; }
        public virtual DbSet<Elemento> Elementos { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<Historial> Historials { get; set; }
        public virtual DbSet<Lector> Lectors { get; set; }
        public virtual DbSet<Lectura> Lecturas { get; set; }
        public virtual DbSet<Movimiento> Movimientos { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<SubSector> SubSectors { get; set; }
        public virtual DbSet<Sucursal> Sucursals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost; Database=ControlInterno; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Antena>(entity =>
            {
                entity.HasKey(e => e.IdAntena);

                entity.ToTable("ANTENA");

                entity.Property(e => e.Custom1).HasMaxLength(50);

                entity.Property(e => e.Custom2).HasMaxLength(50);

                entity.Property(e => e.Custom3).HasMaxLength(50);

                entity.Property(e => e.FkIdLector).HasColumnName("FK_IdLector");

                entity.Property(e => e.FkIdSector).HasColumnName("FK_IdSector");

                entity.Property(e => e.FkIdSubSector).HasColumnName("FK_IdSubSector");

                entity.HasOne(d => d.FkIdLectorNavigation)
                    .WithMany(p => p.Antenas)
                    .HasForeignKey(d => d.FkIdLector)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ANTENA_LECTOR");

                entity.HasOne(d => d.FkIdSectorNavigation)
                    .WithMany(p => p.Antenas)
                    .HasForeignKey(d => d.FkIdSector)
                    .HasConstraintName("FK_ANTENA_SECTOR");

                entity.HasOne(d => d.FkIdSubSectorNavigation)
                    .WithMany(p => p.Antenas)
                    .HasForeignKey(d => d.FkIdSubSector)
                    .HasConstraintName("FK_ANTENA_SUB_SECTOR");
            });

            modelBuilder.Entity<Base>(entity =>
            {
                entity.HasKey(e => e.IdBase);

                entity.ToTable("BASE");

                entity.Property(e => e.Custom1).HasMaxLength(50);

                entity.Property(e => e.Custom2).HasMaxLength(50);

                entity.Property(e => e.Custom3).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.FkIdCategoria).HasColumnName("FK_IdCategoria");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sku)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SKU");
            });

            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.HasKey(e => e.IdCargo);

                entity.ToTable("CARGO");

                entity.HasIndex(e => e.Cargo1, "UK_Cargo")
                    .IsUnique();

                entity.Property(e => e.Cargo1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Cargo");
            });

            modelBuilder.Entity<Elemento>(entity =>
            {
                entity.HasKey(e => e.IdElemento);

                entity.ToTable("ELEMENTO");

                entity.Property(e => e.Custom1).HasMaxLength(50);

                entity.Property(e => e.Custom2).HasMaxLength(50);

                entity.Property(e => e.Custom3).HasMaxLength(50);

                entity.Property(e => e.Dimension).HasMaxLength(50);

                entity.Property(e => e.Epc)
                    .HasMaxLength(50)
                    .HasColumnName("EPC");

                entity.Property(e => e.FechaIngreso).HasColumnType("date");

                entity.Property(e => e.FechaUltimaLectura).HasColumnType("date");

                entity.Property(e => e.FkIdElementoBase).HasColumnName("FK_IdElementoBase");

                entity.Property(e => e.FkIdEmpleadoEncargado).HasColumnName("FK_IdEmpleado_Encargado");

                entity.Property(e => e.FkIdEstado).HasColumnName("FK_IdEstado");

                entity.Property(e => e.FkIdSector).HasColumnName("FK_IdSector");

                entity.Property(e => e.FkIdSubSector).HasColumnName("FK_IdSubSector");

                entity.Property(e => e.FkIdSucursal).HasColumnName("FK_IdSucursal");

                entity.Property(e => e.Fotografia).HasMaxLength(50);

                entity.Property(e => e.InformacionExtra).HasMaxLength(50);

                entity.Property(e => e.NroParte).HasMaxLength(50);

                entity.Property(e => e.NroSerie).HasMaxLength(50);

                entity.HasOne(d => d.FkIdElementoBaseNavigation)
                    .WithMany(p => p.Elementos)
                    .HasForeignKey(d => d.FkIdElementoBase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ELEMENTO_BASE");

                entity.HasOne(d => d.FkIdEmpleadoEncargadoNavigation)
                    .WithMany(p => p.Elementos)
                    .HasForeignKey(d => d.FkIdEmpleadoEncargado)
                    .HasConstraintName("FK_ELEMENTO_EMPLEADO");

                entity.HasOne(d => d.FkIdEstadoNavigation)
                    .WithMany(p => p.Elementos)
                    .HasForeignKey(d => d.FkIdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ELEMENTO_ESTADO");

                entity.HasOne(d => d.FkIdSectorNavigation)
                    .WithMany(p => p.Elementos)
                    .HasForeignKey(d => d.FkIdSector)
                    .HasConstraintName("FK_ELEMENTO_SECTOR");

                entity.HasOne(d => d.FkIdSubSectorNavigation)
                    .WithMany(p => p.Elementos)
                    .HasForeignKey(d => d.FkIdSubSector)
                    .HasConstraintName("FK_ELEMENTO_SUB_SECTOR");

                entity.HasOne(d => d.FkIdSucursalNavigation)
                    .WithMany(p => p.Elementos)
                    .HasForeignKey(d => d.FkIdSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ELEMENTO_SUCURSAL");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado);

                entity.ToTable("EMPLEADO");

                entity.Property(e => e.Apellido1)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Apellido2).HasMaxLength(50);

                entity.Property(e => e.Contrasena).HasMaxLength(20);

                entity.Property(e => e.Custom1).HasMaxLength(50);

                entity.Property(e => e.Custom2).HasMaxLength(50);

                entity.Property(e => e.Custom3).HasMaxLength(50);

                entity.Property(e => e.Epc)
                    .HasMaxLength(50)
                    .HasColumnName("EPC");

                entity.Property(e => e.FkIdCargo).HasColumnName("FK_IdCargo");

                entity.Property(e => e.Fotografia).HasMaxLength(100);

                entity.Property(e => e.Nombre1)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nombre2).HasMaxLength(50);

                entity.Property(e => e.Rut)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("RUT");

                entity.HasOne(d => d.FkIdCargoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.FkIdCargo)
                    .HasConstraintName("FK_EMPLEADO_CARGO");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("ESTADO");

                entity.Property(e => e.Estado1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Estado");
            });

            modelBuilder.Entity<Historial>(entity =>
            {
                entity.HasKey(e => e.IdHistorial);

                entity.ToTable("HISTORIAL");

                entity.Property(e => e.Custom1).HasMaxLength(50);

                entity.Property(e => e.Custom2).HasMaxLength(50);

                entity.Property(e => e.Custom3).HasMaxLength(50);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.FkIdMovimiento).HasColumnName("FK_IdMovimiento");

                entity.HasOne(d => d.FkIdMovimientoNavigation)
                    .WithMany(p => p.Historials)
                    .HasForeignKey(d => d.FkIdMovimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HISTORIAL_MOVIMIENTO");
            });

            modelBuilder.Entity<Lector>(entity =>
            {
                entity.HasKey(e => e.IdLector)
                    .HasName("PK_LECTOR_1");

                entity.ToTable("LECTOR");

                entity.Property(e => e.Custom1).HasMaxLength(50);

                entity.Property(e => e.Custom2).HasMaxLength(50);

                entity.Property(e => e.Custom3).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.FkIdSucursal).HasColumnName("FK_IdSucursal");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Mac)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Marca).HasMaxLength(50);

                entity.Property(e => e.Modelo).HasMaxLength(50);

                entity.Property(e => e.NroParte).HasMaxLength(50);

                entity.Property(e => e.NroSerie).HasMaxLength(50);

                entity.HasOne(d => d.FkIdSucursalNavigation)
                    .WithMany(p => p.Lectors)
                    .HasForeignKey(d => d.FkIdSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LECTOR_SUCURSAL");
            });

            modelBuilder.Entity<Lectura>(entity =>
            {
                entity.HasKey(e => e.IdLectura)
                    .HasName("PK_LECTOR");

                entity.ToTable("LECTURA");

                entity.Property(e => e.Custom1).HasMaxLength(50);

                entity.Property(e => e.Custom2).HasMaxLength(50);

                entity.Property(e => e.Custom3).HasMaxLength(50);

                entity.Property(e => e.Epc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("EPC");

                entity.Property(e => e.FkIdElemento).HasColumnName("FK_IdElemento");

                entity.Property(e => e.FkIdLector).HasColumnName("FK_IdLector");

                entity.Property(e => e.PrimeraVista).HasColumnType("date");

                entity.Property(e => e.Rssi).HasColumnName("RSSI");

                entity.Property(e => e.UltimaVista).HasColumnType("date");

                entity.HasOne(d => d.FkIdElementoNavigation)
                    .WithMany(p => p.Lecturas)
                    .HasForeignKey(d => d.FkIdElemento)
                    .HasConstraintName("FK_LECTURA_ELEMENTO");

                entity.HasOne(d => d.FkIdLectorNavigation)
                    .WithMany(p => p.Lecturas)
                    .HasForeignKey(d => d.FkIdLector)
                    .HasConstraintName("FK_LECTURA_LECTOR");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.IdMovimiento);

                entity.ToTable("MOVIMIENTO");

                entity.Property(e => e.Epc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("EPC");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.FkIdElemento).HasColumnName("FK_IdElemento");

                entity.Property(e => e.FkIdSector).HasColumnName("FK_IdSector");

                entity.Property(e => e.FkIdSubSector).HasColumnName("FK_IdSubSector");

                entity.Property(e => e.FkIdSucursal).HasColumnName("FK_IdSucursal");

                entity.HasOne(d => d.FkIdElementoNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.FkIdElemento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOVIMIENTO_ELEMENTO");

                entity.HasOne(d => d.FkIdSectorNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.FkIdSector)
                    .HasConstraintName("FK_MOVIMIENTO_SECTOR");

                entity.HasOne(d => d.FkIdSubSectorNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.FkIdSubSector)
                    .HasConstraintName("FK_MOVIMIENTO_SUB_SECTOR");

                entity.HasOne(d => d.FkIdSucursalNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.FkIdSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOVIMIENTO_SUCURSAL");
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.HasKey(e => e.IdSector);

                entity.ToTable("SECTOR");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.FkIdSucursal).HasColumnName("FK_IdSucursal");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.FkIdSucursalNavigation)
                    .WithMany(p => p.Sectors)
                    .HasForeignKey(d => d.FkIdSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SECTOR_SUCURSAL");
            });

            modelBuilder.Entity<SubSector>(entity =>
            {
                entity.HasKey(e => e.IdSubSector);

                entity.ToTable("SUB_SECTOR");

                entity.Property(e => e.Custom1).HasMaxLength(50);

                entity.Property(e => e.Custom2).HasMaxLength(50);

                entity.Property(e => e.Custom3).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.Epc)
                    .HasMaxLength(50)
                    .HasColumnName("EPC");

                entity.Property(e => e.FkIdSector).HasColumnName("FK_IdSector");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.FkIdSectorNavigation)
                    .WithMany(p => p.SubSectors)
                    .HasForeignKey(d => d.FkIdSector)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SUB_SECTOR_SECTOR");
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.IdSucursal);

                entity.ToTable("SUCURSAL");

                entity.Property(e => e.Comuna).HasMaxLength(50);

                entity.Property(e => e.Custom1).HasMaxLength(50);

                entity.Property(e => e.Custom2).HasMaxLength(50);

                entity.Property(e => e.Custom3).HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Epc)
                    .HasMaxLength(10)
                    .HasColumnName("EPC")
                    .IsFixedLength(true);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
