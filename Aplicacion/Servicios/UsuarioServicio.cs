using Microsoft.AspNetCore.Identity;
using ProyectoMantenimiento.Aplicacion.DTOs;
using ProyectoMantenimiento.Dominio.Entidades;
using System.Threading.Tasks;

namespace ProyectoMantenimiento.Aplicacion.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        public UsuarioServicio(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<SignInResult> LoginAsync(LoginDto loginDto)
        {
            return await _signInManager.PasswordSignInAsync(
                loginDto.UserName,
                loginDto.Password,
                isPersistent: true,
                lockoutOnFailure: false);
        }

        public async Task<IdentityResult> RegistrarAsync(RegistroDto registroDto)
        {
            var usuario = new Usuario
            {
                UserName = registroDto.UserName,
                Email = registroDto.Email,
                LogoUrl = registroDto.LogoUrl
            };

            return await _userManager.CreateAsync(usuario, registroDto.Password);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}