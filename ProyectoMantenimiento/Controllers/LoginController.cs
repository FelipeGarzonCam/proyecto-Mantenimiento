using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ProyectoMantenimiento.Aplicacion.DTOs;
using ProyectoMantenimiento.Aplicacion.Servicios;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProyectoMantenimiento.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioServicio _usuarioServicio;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LoginController(IUsuarioServicio usuarioServicio, IWebHostEnvironment webHostEnvironment)
        {
            _usuarioServicio = usuarioServicio;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: /Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool loginExitoso = await _usuarioServicio.LoginAsync(model);

            if (loginExitoso)
            {
                HttpContext.Session.SetString("Usuario", model.UserName);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            return View(model);
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
                ViewData["ShowRegisterModal"] = true;
                return View("Index");
            }

            bool registroExitoso = await _usuarioServicio.RegistrarAsync(modelo);

            if (registroExitoso)
            {
                TempData["RegisterSuccess"] = "Registro exitoso";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al registrar el usuario.");
            ViewData["ShowRegisterModal"] = true;
            return View("Index");
        }

    }
}