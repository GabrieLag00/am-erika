using System;
using Microsoft.EntityFrameworkCore;

namespace ArqBackReact.Server.Models
{
    public partial class NetdbContext : DbContext
    {
        public NetdbContext()
        {
        }

        public NetdbContext(DbContextOptions<NetdbContext> options)
            : base(options)
        {
        }

        // DbSets for each model
        public DbSet<Student> Students { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Administrativo> Administrativos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configuración de conexión si no está configurado
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la tabla Student (ya existente)
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .HasColumnName("correo");
                entity.Property(e => e.Edad).HasColumnName("edad");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .HasColumnName("nombre");
            });

            // Configuración de Colaborador
            modelBuilder.Entity<Colaborador>(entity =>
            {
                entity.ToTable("colaborador");
                entity.HasKey(e => e.IdColaborador);
                entity.Property(e => e.IdColaborador).HasColumnName("idColaborador");
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100).HasColumnName("nombre");
                entity.Property(e => e.Edad).HasColumnName("edad");
                entity.Property(e => e.Birthday).HasColumnName("birthday");
                entity.Property(e => e.IsProfesor).HasColumnName("isProfesor");
                entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion").HasDefaultValueSql("GETDATE()");
            });


            // Configuración de Profesor
            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.ToTable("profesor"); 
                entity.HasKey(e => e.IdProfesor); 
                entity.Property(e => e.IdProfesor).HasColumnName("idProfesor"); 
                entity.Property(e => e.FKColaborador).HasColumnName("FKColaborador"); 
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(100).HasColumnName("correo"); 
                entity.Property(e => e.Departamento).IsRequired().HasMaxLength(100).HasColumnName("departamento");
                entity.HasOne(d => d.Colaborador).WithOne(p => p.Profesor).HasForeignKey<Profesor>(d => d.FKColaborador).OnDelete(DeleteBehavior.Cascade);
            });
                

            modelBuilder.Entity<Administrativo>(entity =>
                {
                    entity.ToTable("administrativo"); 
                    entity.HasKey(e => e.IdAdministrativo); 
                    entity.Property(e => e.IdAdministrativo).HasColumnName("idAdministrativo"); 
                    entity.Property(e => e.FKColaborador).HasColumnName("FKColaborador"); 
                    entity.Property(e => e.Correo).IsRequired().HasMaxLength(100).HasColumnName("correo"); 
                    entity.Property(e => e.Puesto).IsRequired().HasMaxLength(100).HasColumnName("puesto"); 
                    entity.Property(e => e.Nomina).HasColumnName("nomina");
                    entity.HasOne(d => d.Colaborador).WithOne(p => p.Administrativo).HasForeignKey<Administrativo>(d => d.FKColaborador).OnDelete(DeleteBehavior.Cascade);
                });
                


        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
