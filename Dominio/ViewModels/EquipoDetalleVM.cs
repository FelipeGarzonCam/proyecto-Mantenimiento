using ProyectoMantenimiento.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMantenimiento.Dominio.ViewModels
{
    public class EquipoDetalleVM
    {
        public Equipo Equipo { get; set; }            // Datos básicos
        public HojaVidaEquipos HojaVida { get; set; } // Hoja de vida

        public IEnumerable<Mantenimiento> ListaMantenimientos { get; set; }
    }


}
