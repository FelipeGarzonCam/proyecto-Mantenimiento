using System.Threading.Tasks;
using Aplicacion.DTOs;
using Microsoft.AspNetCore.Identity;
using ProyectoMantenimiento.Aplicacion.DTOs;

namespace ProyectoMantenimiento.Aplicacion.Servicios
{
    public interface IUsuarioServicio
    {
        Task<SignInResult> LoginAsync(LoginDto loginDto);
        Task<IdentityResult> RegistrarAsync(RegistroDto registroDto);
        Task LogoutAsync();
    }
}
