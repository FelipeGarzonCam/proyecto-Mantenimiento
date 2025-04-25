using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class RegistroDto
    {
        [Required] public string UserName { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required, DataType(DataType.Password)] public string Password { get; set; }
        [Compare("Password"), DataType(DataType.Password)] public string ConfirmPassword { get; set; }
        // Logo opcional
        public string LogoUrl { get; set; }
    }
}
