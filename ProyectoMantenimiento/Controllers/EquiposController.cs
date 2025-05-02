using System.Linq;
using System.Data.Entity;
using ProyectoMantenimiento.Persistencia;
using ProyectoMantenimiento.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoMantenimiento.Controllers
{
    public class EquiposController : Controller
    {
        private readonly AppDbContext _ctx = new AppDbContext();

        // GET: /Equipos
        public IActionResult Index()
        {
            var lista = _ctx.Equipos.ToList();        // IEnumerable<Equipo>
            return View(lista);
        }

        // GET: /Equipos/Create
        public IActionResult Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Equipo equipo)
        {
            // 1) Registrar valores vinculados para comprobar binding
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Equipo recibido: Nombre={equipo.Nombre}, Descripcion={equipo.Descripcion}, Cantidad={equipo.Cantidad}");

            // 2) Si ModelState falla, volcamos todos los errores al ViewBag
            if (!ModelState.IsValid)
            {
                var errores = ModelState
                    .SelectMany(ms => ms.Value.Errors.Select(err => err.ErrorMessage))
                    .ToList();
                ViewBag.ErroresBinding = errores;
                return View(equipo);
            }

            try
            {
                _ctx.Equipos.Add(equipo);
                _ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 3) Si hay excepción, la guardamos en ViewBag
                ViewBag.ErrorAlGuardar = ex.GetBaseException().Message;
                return View(equipo);
            }
        }




        // GET: /Equipos/Details/5
        public IActionResult Details(int id)
        {
            var equipo = _ctx.Equipos.Include(e => e.Mantenimientos)
                                      .FirstOrDefault(e => e.EquipoId == id);
            if (equipo == null) return NotFound();      // Core helper
            return View(equipo);                         // modelo = Equipo
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _ctx.Dispose();
            base.Dispose(disposing);
        }
    }
}