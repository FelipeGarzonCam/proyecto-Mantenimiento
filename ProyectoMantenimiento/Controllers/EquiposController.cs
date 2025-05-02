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

        // POST: /Equipos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Equipo equipo)
        {
            if (!ModelState.IsValid)
            {
                // For debugging: muestra todos los errores en la vista
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
                ModelState.AddModelError("", ex.Message);
                // Opcional: log ex.ToString()
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