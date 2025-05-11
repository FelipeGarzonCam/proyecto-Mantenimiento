using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoMantenimiento.Dominio.Entidades
{
    [Table("Mantenimientos")]
    public class Mantenimiento
    {
        [Key]
        public int MantenimientoId { get; set; }

        [ForeignKey(nameof(Equipo))]
        public int EquipoId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [StringLength(80)]
        public string Responsable { get; set; }

        public string Notas { get; set; }

        [Required(ErrorMessage = "Seleccione el tipo")]
        [StringLength(20)]
        public string Tipo { get; set; }


        /* NAVIGATION */
        public virtual Equipo Equipo { get; set; }
    }
}
