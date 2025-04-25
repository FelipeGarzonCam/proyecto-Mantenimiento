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

        // Aquí puedes agregar tus DbSet para otras entidades
    }
}