using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ProyectoMantenimiento.Dominio.Entidades;

namespace ProyectoMantenimiento.Persistencia
{
    public class AppDbContext : DbContext
    {
        // Mantener la conexión hardcodeada como predeterminada
        private static readonly string defaultConnectionString =
            "Server=localhost;" +
            "Database=ProyectoMantenimiento;" +
            "Trusted_Connection=True;";

        // Constructor sin parámetros para usar la conexión predeterminada
        public AppDbContext() : base(defaultConnectionString)
        {
        }

        // Constructor que acepta una cadena de conexión, validando que no sea nula
        public AppDbContext(string connectionString) : base(
            string.IsNullOrWhiteSpace(connectionString) ? defaultConnectionString : connectionString)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Mantenimiento> Mantenimientos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Quita la pluralización automática (Equipo, Mantenimiento, etc.)
            modelBuilder.Conventions
                        .Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}