using ProyectoMantenimiento.Aplicacion.DTOs;
using System.Threading.Tasks;

namespace ProyectoMantenimiento.Aplicacion.Servicios
{
    public interface IUsuarioServicio
    {
        Task<bool> LoginAsync(LoginDto loginDto);
        Task<bool> RegistrarAsync(RegistroDto registroDto);
        Task LogoutAsync();
    }
}
