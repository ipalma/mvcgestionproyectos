using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_GestionProyectos.Models
{
    public class VistaDatosUsuario
    {
        public int idUsuario { get; set; }
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string foto { get; set; }
        public int idGrupo { get; set; }
        public string grupo { get; set; }
        public int idProyecto { get; set; }
        public string proyecto { get; set; }
        public Nullable<int> jefeGrupo { get; set; }
    }
}