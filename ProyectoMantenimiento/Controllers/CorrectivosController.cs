using Microsoft.AspNetCore.Mvc;
using ProyectoMantenimiento.Persistencia;
using ProyectoMantenimiento.Aplicacion.Helpers;
using Rotativa.AspNetCore;
using System;
using System.Linq;
using System.Collections.Generic;
using ProyectoMantenimiento.Dominio.Entidades;
using ProyectoMantenimiento.Dominio.Enums;


namespace ProyectoMantenimiento.Controllers
{
    public class CorrectivosController : Controller
    {
        private readonly AppDbContext _ctx = new();

        // Devuelve la lista filtrada por rango
        private IEnumerable<Mantenimiento> ObtenerLista(string periodo)
        {
            // Validar y convertir el periodo a enum Periodicidad
            if (!Enum.TryParse(periodo, true, out Periodicidad rango))
                rango = Periodicidad.Dia;

            var (ini, fin) = RangoFechas.Calcular(rango);

            // Filtrar usando la propiedad real "Fecha" y el string "Correctivo"
            return _ctx.Mantenimientos
                       .Where(m => m.Tipo == "Correctivo"
                                && m.Fecha >= ini
                                && m.Fecha < fin)
                       .OrderByDescending(m => m.Fecha)
                       .ToList();
        }

        // Mostrar la tabla en pantalla
        public IActionResult Index(string periodo = "Dia")
        {
            // Convertir a enum Periodicidad
            if (!Enum.TryParse(periodo, true, out Periodicidad rango))
                rango = Periodicidad.Dia;

            var (ini, fin) = RangoFechas.Calcular(rango);
            var lista = _ctx.Mantenimientos
                           .Where(m => m.Tipo == "Correctivo"
                                    && m.Fecha >= ini
                                    && m.Fecha < fin)
                           .OrderByDescending(m => m.Fecha)
                           .ToList();

            ViewBag.Periodo = rango;
            return View(lista);
        }

        // Generar el PDF con Rotativa
        public IActionResult Print(string periodo = "Dia")
        {
            var datos = ObtenerLista(periodo);
            return new ViewAsPdf("Print", datos)
            {
                FileName = $"Correctivo_{periodo}_{DateTime.Now:yyyyMMdd}.pdf",
                CustomSwitches = "--enable-local-file-access"
            };
        }
    }
}
