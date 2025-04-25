
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ProyectoMantenimiento.Dominio.Entidades;

namespace ProyectoMantenimiento.Persistencia
{
    public class AppDbContext : DbContext
    {
        // Mantener la conexión hardcodeada para compatibilidad
        private static readonly string defaultConnectionString =
            "Server=localhost;" +
            "Database=ProyectoMantenimiento;" +
            "Trusted_Connection=True;";

        // Constructor sin parámetros para compatibilidad
        public AppDbContext() : base(defaultConnectionString)
        {
        }

        // Constructor que acepta una cadena de conexión
        public AppDbContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
