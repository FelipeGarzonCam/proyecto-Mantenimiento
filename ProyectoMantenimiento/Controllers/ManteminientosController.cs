using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoMantenimiento.Dominio.Entidades;
using ProyectoMantenimiento.Persistencia;
using System.Data.Entity;


namespace ProyectoMantenimiento.Controllers
{
    public class MantenimientosController : Controller
    {
        private readonly AppDbContext _ctx;

        public MantenimientosController(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        
        // GET: /Mantenimientos/Create?equipoId=5
        public IActionResult Create(int equipoId)
        {
            // Prepara la lista de tipos para el dropdown
            ViewBag.Tipos = new SelectList(
                new[] { "Correctivo", "Preventivo", "Predictivo" });
            // Inicializa el modelo con el EquipoId recibido
            var model = new Mantenimiento { EquipoId = equipoId };
            return View(model);
        }

        // POST: /Mantenimientos/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Mantenimiento m)
        {
            ModelState.Remove(nameof(Mantenimiento.Equipo));
            if (!ModelState.IsValid)
            {
                // Si hay errores, recarga la lista y vuelve a la vista
                ViewBag.Tipos = new SelectList(
                    new[] { "Correctivo", "Preventivo", "Predictivo" });
                return View(m);
            }
            m.Fecha = DateTime.Now;
            _ctx.Mantenimientos.Add(m);
            _ctx.SaveChanges();

            // Al guardar, redirige a Detalles de Equipo
            return RedirectToAction("Details", "Equipos", new { id = m.EquipoId });
        }
        // GET: /Mantenimientos/Details/108
        public IActionResult Details(int id)
        {
            var mant = _ctx.Mantenimientos
                           .Include(m => m.Equipo)
                           .FirstOrDefault(m => m.MantenimientoId == id);

            if (mant == null) return NotFound();

            return View(mant);
        }

    }
}
