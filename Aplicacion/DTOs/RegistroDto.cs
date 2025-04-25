using System.ComponentModel.DataAnnotations;

namespace ProyectoMantenimiento.Aplicacion.DTOs
{
    public class RegistroDto
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }       
    }
}