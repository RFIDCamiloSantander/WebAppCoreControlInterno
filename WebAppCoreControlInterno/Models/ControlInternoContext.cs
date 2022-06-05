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

        public virtual DbSet<Cargo> Cargos { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=PC-KAMES; Database=ControlInterno; Trusted_Connection=False; User=sa; Password=Kames1313");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.HasKey(e => e.IdCargo);

                entity.ToTable("CARGO");

                entity.Property(e => e.Cargo1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Cargo");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PK_Empleado");

                entity.ToTable("EMPLEADO");

                entity.Property(e => e.Apellido1).HasMaxLength(50);

                entity.Property(e => e.Apellido2).HasMaxLength(50);

                entity.Property(e => e.Contrasena).HasMaxLength(50);

                entity.Property(e => e.Custom1).HasMaxLength(50);

                entity.Property(e => e.Custom2).HasMaxLength(50);

                entity.Property(e => e.Custom3).HasMaxLength(50);

                entity.Property(e => e.Epc).HasMaxLength(50);

                entity.Property(e => e.FkIdCargo).HasColumnName("FK_IdCargo");

                entity.Property(e => e.Fotografia).HasMaxLength(50);

                entity.Property(e => e.Nombre1).HasMaxLength(50);

                entity.Property(e => e.Nombre2).HasMaxLength(50);

                entity.Property(e => e.Rut).HasMaxLength(50);

                entity.HasOne(d => d.FkIdCargoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.FkIdCargo)
                    .HasConstraintName("FK_EMPLEADO_CARGO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
