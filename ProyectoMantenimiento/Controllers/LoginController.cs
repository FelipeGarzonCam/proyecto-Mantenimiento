using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ProyectoMantenimiento.Aplicacion.DTOs;
using ProyectoMantenimiento.Aplicacion.Servicios;

namespace ProyectoMantenimiento.Interfaz.Controllers
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
            // Muestra la vista Views/Login/Index.cshtml
            return View();
        }

        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginDto modelo)
        {
            if (!ModelState.IsValid)
            {
                // Si faltan datos o no pasan las validaciones, volvemos a mostrar la vista con los errores
                return View(modelo);
            }

            // Intentamos autenticar al usuario
            var resultado = await _usuarioServicio.LoginAsync(modelo);
            if (resultado.Succeeded)
            {
                // Si el login fue exitoso, redirigimos al Home
                return RedirectToAction("Index", "Home");
            }

            // Si falla, añadimos un error genérico y regresamos la vista
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
