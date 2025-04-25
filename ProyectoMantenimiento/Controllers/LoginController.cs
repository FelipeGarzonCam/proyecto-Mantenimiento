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
        public async Task<IActionResult> Register(RegistroDto modelo, IFormFile LogoFile)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ShowRegisterModal"] = true;
                return View("Index", new LoginDto());
            }

            // Handle file upload if there is one
            if (LogoFile != null && LogoFile.Length > 0)
            {
                // Create uploads directory if it doesn't exist
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "logos");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate unique filename
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + LogoFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await LogoFile.CopyToAsync(fileStream);
                }

                // Update model with the relative path
                modelo.LogoUrl = "/uploads/logos/" + uniqueFileName;
            }
            else
            {
                // Set default logo path
                modelo.LogoUrl = "/images/default-logo.png";
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