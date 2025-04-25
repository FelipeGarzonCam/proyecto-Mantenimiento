using Microsoft.AspNetCore.Mvc;
using ProyectoMantenimiento.Aplicacion.DTOs;
using ProyectoMantenimiento.Aplicacion.Servicios;
using System.Threading.Tasks;

namespace ProyectoMantenimiento.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public LoginController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        // GET: /Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginDto modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var resultado = await _usuarioServicio.LoginAsync(modelo);
            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Usuario o contraseña inválidos");
            return View(modelo);
        }

        // GET: /Login/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _usuarioServicio.LogoutAsync();
            return RedirectToAction("Index");
        }
    }
}