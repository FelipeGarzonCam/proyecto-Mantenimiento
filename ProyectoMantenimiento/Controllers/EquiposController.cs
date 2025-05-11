using System.Linq;
using System.Data.Entity;
using ProyectoMantenimiento.Persistencia;
using ProyectoMantenimiento.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using ProyectoMantenimiento.Dominio.ViewModels;

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
            var equipo = _ctx.Equipos.Find(id);
            if (equipo == null) return NotFound();

            var hoja = _ctx.HojaVidaEquipos.FirstOrDefault(h => h.EquipoId == id);

            var mantenimientos = _ctx.Mantenimientos
                                      .Where(m => m.EquipoId == id)
                                      .OrderByDescending(m => m.Fecha)
                                      .ToList();

            var vm = new EquipoDetalleVM
            {
                Equipo = equipo,
                HojaVida = hoja,
                ListaMantenimientos = mantenimientos
            };

            return View(vm);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing) _ctx.Dispose();
            base.Dispose(disposing);
        }

        // GET: /Equipos/Edit/5
        public IActionResult Edit(int id)
        {
            var equipo = _ctx.Equipos.Find(id);
            return equipo == null ? NotFound() : View(equipo);
        }

        // POST: /Equipos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Equipo equipo)
        {
            if (!ModelState.IsValid)
                return View(equipo);

            _ctx.Entry(equipo).State = EntityState.Modified;
            _ctx.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}