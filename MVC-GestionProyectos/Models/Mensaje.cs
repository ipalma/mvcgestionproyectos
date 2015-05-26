using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_GestionProyectos.Models
{
    public class Mensaje
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "idRemitente")]
        public int idRem { get; set; }
        [Required]
        [Display(Name = "fotodelRemitente")]
        public string fotoRem { get; set; }
        [Required]
        [Display(Name = "idDestinatario")]
        public int idDest { get; set; }
        [Required]
        [Display(Name = "fecha")]
        public System.DateTime fecha { get; set; }
        [Required]
        [Display(Name = "contenido")]
        public string contenido { get; set; }
        [Required]
        [Display(Name = "nombreDelRemitente")]
        public string nombreRem { get; set; }
        [Required]
        [Display(Name = "emailDelRemitente")]
        public string emailRem { get; set; }

        public int estado { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
        public virtual EstadoMensaje EstadoMensaje { get; set; }
    }
}