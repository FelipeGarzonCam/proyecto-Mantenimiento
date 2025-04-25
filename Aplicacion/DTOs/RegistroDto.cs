using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMantenimiento.Aplicacion.DTOs
{
    public class RegistroDto
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres.", MinimumLength = 6)]
        public string Password { get; set; }

        // Agregar esta propiedad:
        public string LogoUrl { get; set; }
    }
}
