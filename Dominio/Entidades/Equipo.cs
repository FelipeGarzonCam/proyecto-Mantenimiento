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

        // -------- Tarjeta maestra --------
        [Required, StringLength(30)]
        public string CodigoInterno { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(60)]
        public string Marca { get; set; }

        [StringLength(60)]
        public string Modelo { get; set; }

        [StringLength(60)]
        public string NumeroSerie { get; set; }

        public DateTime? FechaAdquisicion { get; set; }

        [StringLength(80)]
        public string Ubicacion { get; set; }

        [Range(0, 1000)]
        public double? PotenciaHP { get; set; }

        [Range(0, 100000)]
        public double? Capacidad { get; set; }   // kg, lts, etc.

        [Range(0, 10000)]
        public double? PesoKg { get; set; }

        [StringLength(20)]
        public string Criticidad { get; set; }   // Crítico/Urgente/Normal
        // ---------------------------------
        public string Descripcion { get; set; }

        public int? Cantidad { get; set; }

        // Relación con mantenimientos (si existe entidad Mantenimiento)
        public virtual ICollection<Mantenimiento> Mantenimientos { get; set; }
    }
}
