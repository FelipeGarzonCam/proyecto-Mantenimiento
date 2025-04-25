using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ProyectoMantenimiento.Dominio.Entidades
{
    public class Usuario : IdentityUser
    {
        // Propiedad existente
        public string LogoUrl { get; set; }

        // Propiedades adicionales que podrías necesitar
        public string Nombre { get; set; }
        public string Apellido { get; set; }   

        
    }
}