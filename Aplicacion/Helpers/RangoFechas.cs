using ProyectoMantenimiento.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProyectoMantenimiento.Aplicacion.Helpers
{
    //public enum Periodicidad { Dia, Semana, Mes, Semestre, Anual }

    public static class RangoFechas
    {
        public static (DateTime inicio, DateTime fin) Calcular(Periodicidad p)
        {
            var hoy = DateTime.Today;
            switch (p)
            {
                case Periodicidad.Semana:
                    // ISO 8601: lunes como inicio de semana
                    int delta = DayOfWeek.Monday - hoy.DayOfWeek;
                    var inicioSemana = hoy.AddDays(delta < 0 ? delta + 7 : delta);  // primer lunes ≤ hoy :contentReference[oaicite:0]{index=0}
                    return (inicioSemana, inicioSemana.AddDays(7));

                case Periodicidad.Mes:
                    var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);
                    return (inicioMes, inicioMes.AddMonths(1));

                case Periodicidad.Semestre:
                    int sem = (hoy.Month - 1) / 6;
                    var inicioSem = new DateTime(hoy.Year, sem * 6 + 1, 1);
                    return (inicioSem, inicioSem.AddMonths(6));

                case Periodicidad.Anual:
                    var inicioAnio = new DateTime(hoy.Year, 1, 1);
                    return (inicioAnio, inicioAnio.AddYears(1));

                default: // Día
                    return (hoy, hoy.AddDays(1));
            }
        }
    }
}
