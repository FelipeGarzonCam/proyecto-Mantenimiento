using ProyectoMantenimiento.Aplicacion.DTOs;
using ProyectoMantenimiento.Dominio.Entidades;
using ProyectoMantenimiento.Persistencia;
using System.Threading.Tasks;
using System.Linq;

namespace ProyectoMantenimiento.Aplicacion.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly AppDbContext _db;

        public UsuarioServicio(AppDbContext db)
        {
            _db = db;
        }

        public Task<bool> LoginAsync(LoginDto loginDto)
        {
            bool existe = _db.Usuarios
                .Any(u => u.UserName == loginDto.UserName
                       && u.Password == loginDto.Password);
            return Task.FromResult(existe);
        }

        public Task<bool> RegistrarAsync(RegistroDto registroDto)
        {
            var usuario = new Usuario
            {
                UserName = registroDto.UserName,
                Password = registroDto.Password
            };

            _db.Usuarios.Add(usuario);
            _db.SaveChanges();

            return Task.FromResult(true);
        }

        public Task LogoutAsync()
        {
            // Implementa la lógica de cierre de sesión según tus necesidades
            return Task.CompletedTask;
        }
    }
}
