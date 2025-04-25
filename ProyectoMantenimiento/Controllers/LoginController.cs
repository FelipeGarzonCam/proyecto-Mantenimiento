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

            TempData["LoginError"] = "Usuario o contraseña inválidos";
            return RedirectToAction("Index");
        }

        // GET: /Login/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _usuarioServicio.LogoutAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistroDto modelo)
        {
            if (!ModelState.IsValid)
            {
                // Para que se abra el modal con los errores
                ViewData["ShowRegisterModal"] = true;
                return View("Index", new LoginDto());
            }

            var resultado = await _usuarioServicio.RegistrarAsync(modelo);
            if (resultado.Succeeded)
            {
                TempData["RegisterSuccess"] = "Usuario registrado correctamente.";
                return RedirectToAction("Index");
            }

            foreach (var error in resultado.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            ViewData["ShowRegisterModal"] = true;
            return View("Index", new LoginDto());
        }
    }
}