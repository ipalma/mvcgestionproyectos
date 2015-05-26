using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_GestionProyectos.Models
{
    public class Proyecto
    {
        public int id { get; set; }

        [DisplayName("Nombre")]
        public string nombre { get; set; }

        [DisplayName("Fecha Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime fechaInicio { get; set; }

        [DisplayName("Fecha Fin")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime fechaFin { get; set; }

        [DisplayName("Descripcion")]
        public string descripcion { get; set; }

        [DisplayName("Fecha Creacion")]
        [DisplayFormat(DataFormatString = "{0:d}")]

        public System.DateTime fechaCreacion { get; set; }
        public int idEstado { get; set; }
        public int idPrioridad { get; set; }
        public virtual ICollection<Proyecto_Tarea_Grupo> UsuariosDelGrupo { get; set; }

        //public IEnumerable<String> usuariosEmail { get; set; }
        //public IEnumerable<String> usuariosFoto { get; set; }

        public IEnumerable<Usuario> usuarios { get; set; }
        public IEnumerable<Tarea> Tarea_Proyecto { get; set; }

        public int numeroTareas { get; set; }

    }
}