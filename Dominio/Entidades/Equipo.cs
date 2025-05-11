using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoMantenimiento.Dominio.Entidades
{
  
    public class Equipo
    {
        public Equipo()
        {
            // Así nunca será null al hacer el binding
            Mantenimientos = new List<Mantenimiento>();
        }
        [Key]
        public int EquipoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int? Cantidad { get; set; }

        // Relación con mantenimientos (si existe entidad Mantenimiento)
        public virtual ICollection<Mantenimiento> Mantenimientos { get; set; }
    }
}
