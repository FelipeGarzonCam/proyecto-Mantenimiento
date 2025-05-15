using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoMantenimiento.Dominio.Entidades
{
    public class Mantenimiento
    {
        [Key] public int MantenimientoId { get; set; }

        [ForeignKey(nameof(Equipo))]
        public int EquipoId { get; set; }
        public virtual Equipo Equipo { get; set; }

        [Required] public DateTime Fecha { get; set; } = DateTime.Today;

        [Required, StringLength(20)]
        public string Tipo { get; set; }          // Correctivo / Preventivo / Predictivo

        [StringLength(25)]
        public string OtNumero { get; set; }      // Nº de orden de trabajo

        [StringLength(500)]
        public string TrabajoRealizado { get; set; }

        [Column(TypeName = "money")]
        public decimal? CostoTotal { get; set; }  // SQL money es apropiado para costos :contentReference[oaicite:1]{index=1}

        [StringLength(500)]
        public string Descripcion { get; set; }
    }
}
