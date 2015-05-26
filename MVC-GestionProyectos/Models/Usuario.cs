using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_GestionProyectos.Models
{
    public class Usuario
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        public string apellido { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        public string foto { get; set; }
        public List<Habilidad> Habilidad { get; set; }
        public List<Proyecto_Tarea_Grupo> Proyecto_Tarea_Grupo { get; set; }
        public List<Tarea> Tarea_Usuario { get; set; }
        public int numeroTareas { get; set; }
        public List<Mensaje> Mensaje { get; set; }
    }
}