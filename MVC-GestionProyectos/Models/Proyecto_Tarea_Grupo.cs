using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_GestionProyectos.Models
{
    public class Proyecto_Tarea_Grupo
    {
        public int idUsuario { get; set; }
        public String NombreUsuario { get; set; }
        public int idProyecto { get; set; }
        public int idGrupo { get; set; }
        //public String NombreGrupo { get; set; }
        public Nullable<int> jefeGrupo { get; set; }
        //public String NombreJefe {get; set; }
        public String Proyecto { get; set; }
        public virtual ICollection<Tarea> Tarea { get; set; }
        public virtual ICollection<Proyecto_Tarea_Grupo> UsuariosDelGrupo { get; set; }

    }
}