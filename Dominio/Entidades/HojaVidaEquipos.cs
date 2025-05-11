using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoMantenimiento.Dominio.Entidades
{
    [Table("HojaVidaEquipos")]
    public class HojaVidaEquipos
    {
        [Key]
        public int HojaVidaId { get; set; }

        [ForeignKey(nameof(Equipo))]
        public int EquipoId { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string Marca { get; set; }

        [StringLength(100)]
        public string Modelo { get; set; }

        [StringLength(100)]
        public string NumeroSerie { get; set; }

        [StringLength(120)]
        public string Ubicacion { get; set; }

        [StringLength(80)]
        public string Responsable { get; set; }

        /*  NAVIGATION  */
        public virtual Equipo Equipo { get; set; }
    }
}
