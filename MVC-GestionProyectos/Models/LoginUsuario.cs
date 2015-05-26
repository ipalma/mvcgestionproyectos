using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_GestionProyectos.Models
{
    public class LoginUsuario
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name="Email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "password")]
        public string password { get; set; }
    }
}