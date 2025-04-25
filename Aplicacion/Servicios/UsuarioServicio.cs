using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Aplicacion.DTOs;
using ProyectoMantenimiento.Aplicacion.Servicios;

namespace Aplicacion.Servicios
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

        public async Task<SignInResult> LoginAsync(LoginDto loginDto) =>
            await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, true, false);

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

        public async Task LogoutAsync() =>
            await _signInManager.SignOutAsync();
    }
}
