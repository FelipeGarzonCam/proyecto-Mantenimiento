using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ProyectoMantenimiento.Dominio.Entidades;

namespace ProyectoMantenimiento.Persistencia
{
    public class SistemaQuickTableContext : DbContext
    {
        static string connectionString =
            "Server=localhost;" +
            "Database=ProyectoMantenimiento;" +
            "Trusted_Connection=True;"; //chinga tu madre

        public SistemaQuickTableContext() : base(connectionString)
        {
        }
    }
}