using Microsoft.AspNetCore.Identity;
using ProyectoMantenimiento.Aplicacion.DTOs;
using System.Threading.Tasks;

namespace ProyectoMantenimiento.Aplicacion.Servicios
{
    public interface IUsuarioServicio
    {
        Task<SignInResult> LoginAsync(LoginDto loginDto);
        Task<IdentityResult> RegistrarAsync(RegistroDto registroDto);
        Task LogoutAsync();
    }
}
