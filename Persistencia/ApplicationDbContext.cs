using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoMantenimiento.Dominio.Entidades;

namespace ProyectoMantenimiento.Persistencia
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuraciones adicionales de las entidades si es necesario
            builder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.LogoUrl).HasMaxLength(255).IsRequired(false);
            });

            // Aquí puedes añadir más configuraciones de mapeo si necesitas
        }

        // Definir DbSets para otras entidades relacionadas con tu sistema
        // Ejemplo: public DbSet<OtraEntidad> OtrasEntidades { get; set; }
    }
}