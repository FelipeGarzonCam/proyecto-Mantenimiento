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
              ViewData["ShowRegisterModal"] = true;
              return View("Index");
            }
            var resultado = await _usuarioServicio.RegistrarAsync(modelo);
            if (resultado.Succeeded)
            {
              TempData["RegisterSuccess"] = "Registro exitoso";
              return RedirectToAction("Index");
            }
            foreach (var err in resultado.Errors)
            ModelState.AddModelError(string.Empty, err.Description);
            ViewData["ShowRegisterModal"] = true;
            return View("Index");
          }
    }
}