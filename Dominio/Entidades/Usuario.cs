using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Dominio.Entidades
{
    public class Usuario : IdentityUser
    {
        public string LogoUrl { get; set; }
    }
}
