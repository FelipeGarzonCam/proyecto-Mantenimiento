using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProyectoMantenimiento.Dominio.Entidades;


namespace ProyectoMantenimiento.Dominio.Entidades
{
    [Table("Mantenimiento")]
    public class Mantenimiento
    {
        [Key]
        public int MantenimientoId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [StringLength(250)]
        public string Descripcion { get; set; }

        [Required, StringLength(50)]
        public string Tipo { get; set; } // Preventivo, Correctivo

        // FK → Equipo
        public int EquipoId { get; set; }
        [ForeignKey("EquipoId")]
        public virtual Equipo Equipo { get; set; }
    }
}
