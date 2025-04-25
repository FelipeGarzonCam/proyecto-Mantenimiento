using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ProyectoMantenimiento.Persistencia
{
    public class SistemaQuickTableContext : DbContext
    {
        public static string ConnectionString =
            "Server=localhost;" +
            "Database=ProyectoMantenimientoDB;" +
            "Trusted_Connection=True;";

        public SistemaQuickTableContext(DbContextOptions<SistemaQuickTableContext> options)
            : base(options) { }

        // DbSet para usuarios
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}