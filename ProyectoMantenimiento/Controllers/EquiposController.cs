using System.Linq;
using System.Data.Entity;
using ProyectoMantenimiento.Persistencia;
using ProyectoMantenimiento.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using ProyectoMantenimiento.Dominio.ViewModels;
using System.Reflection.Metadata;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Doc = iText.Layout.Document;

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
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Equipo equipo)
        {
            if (!ModelState.IsValid) return View(equipo);

            _ctx.Equipos.Add(equipo);
            _ctx.SaveChanges();

            // Redirigimos a la vista para crear mantenimiento del mismo equipo
            return RedirectToAction("Create", "Mantenimientos", new { equipoId = equipo.EquipoId });
        }

        // GET: /Equipos/Details/5
        public IActionResult Details(int id)
        {
            // 1) Carga el equipo con sus mantenimientos
            var equipo = _ctx.Equipos
                             .Include(e => e.Mantenimientos)
                             .FirstOrDefault(e => e.EquipoId == id);
            if (equipo == null) return NotFound();

            // 2) Obtiene la lista ordenada de todos los IDs
            var allIds = _ctx.Equipos
                             .OrderBy(e => e.EquipoId)
                             .Select(e => e.EquipoId)
                             .ToList();

            // 3) Calcula la posición actual
            var idx = allIds.IndexOf(id);

            // 4) Determina PreviousId y NextId o deja null
            ViewBag.PreviousId = idx > 0 ? allIds[idx - 1] : (int?)null;
            ViewBag.NextId = idx < allIds.Count - 1 ? allIds[idx + 1] : (int?)null;

            return View(equipo);
        }

        [HttpGet]
        public IActionResult ExportPdf(int id)
        {
            var equipo = _ctx.Equipos
                             .Include(e => e.Mantenimientos)
                             .FirstOrDefault(e => e.EquipoId == id);
            if (equipo == null) return NotFound();

            using var ms = new MemoryStream();
            var writer = new PdfWriter(ms);                                     // ejemplo iText 7 :contentReference[oaicite:4]{index=4}
            var pdf = new PdfDocument(writer);                               // ctor con writer :contentReference[oaicite:5]{index=5}
            using var doc = new Doc(pdf);                                    // aliasado

            doc.Add(new Paragraph("Hoja de vida del equipo").SetBold().SetFontSize(16));
            doc.Add(new Paragraph($"Código interno: {equipo.CodigoInterno}"));
            doc.Add(new Paragraph($"Nombre: {equipo.Nombre}"));
            doc.Add(new Paragraph($"Marca / Modelo: {equipo.Marca} / {equipo.Modelo}"));
            doc.Add(new Paragraph($"Serie: {equipo.NumeroSerie}"));
            doc.Add(new Paragraph($"Ubicación: {equipo.Ubicacion}"));
            doc.Add(new Paragraph($"Criticidad: {equipo.Criticidad}"));
            doc.Add(new Paragraph(" "));

            var table = new Table(6).UseAllAvailableWidth();
            table.AddHeaderCell("Fecha").AddHeaderCell("Tipo").AddHeaderCell("OT")
                 .AddHeaderCell("Trabajo").AddHeaderCell("Costo").AddHeaderCell("Obs.");

            foreach (var m in equipo.Mantenimientos.OrderByDescending(x => x.Fecha))
            {
                table.AddCell(m.Fecha.ToString("dd-MM-yyyy"));
                table.AddCell(m.Tipo);
                table.AddCell(m.OtNumero ?? "");
                table.AddCell(m.TrabajoRealizado ?? "");
                table.AddCell(m.CostoTotal?.ToString("C0") ?? "");
                table.AddCell(m.Descripcion ?? "");
            }
            doc.Add(table);
            doc.Close();                                                        // método correcto :contentReference[oaicite:6]{index=6}

            return File(ms.ToArray(), "application/pdf",
                        $"HojaVida_{equipo.CodigoInterno}.pdf");
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