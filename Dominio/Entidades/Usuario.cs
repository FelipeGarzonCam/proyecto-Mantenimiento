using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoMantenimiento.Dominio.Entidades
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }                      // PK por convención :contentReference[oaicite:4]{index=4}

        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }             // Nombre de usuario :contentReference[oaicite:5]{index=5}

        [Required]
        public string Password { get; set; }             // Contraseña en texto plano :contentReference[oaicite:6]{index=6}
    }
}