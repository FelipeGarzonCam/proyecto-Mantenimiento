using ProyectoMantenimiento.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using ProyectoMantenimiento.Persistencia;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoMantenimiento.Controllers
{
    public class MantenimientosController : Controller
    {
        private readonly AppDbContext _ctx = new AppDbContext();
        // lista estática de opciones
        private static readonly string[] _tipos =
            { "Preventivo", "Correctivo", "Predictivo" };

        // GET: /Mantenimientos/Create?equipoId=5
        public IActionResult Create(int equipoId)
        {
            var modelo = new Mantenimiento
            {
                EquipoId = equipoId,
                Fecha = DateTime.Today
            };
            ViewBag.Tipos = new SelectList(_tipos);    // ← aquí nunca nulo
            return View(modelo);
        }

        // POST: /Mantenimientos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Mantenimiento m)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Tipos = new SelectList(_tipos);  // ← recarga en caso de fallo
                return View(m);
            }

            _ctx.Mantenimientos.Add(m);
            _ctx.SaveChanges();
            return RedirectToAction("Details", "Equipos", new { id = m.EquipoId });
        }
    }
}
