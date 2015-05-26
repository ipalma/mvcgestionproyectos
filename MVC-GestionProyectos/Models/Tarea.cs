using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_GestionProyectos.Models
{
    public class Tarea
    {
        public int id { get; set; }
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [DisplayName("Creado")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime fechaCreacion { get; set; }
        [DisplayName("Fecha asignación")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> fechaAsignacion { get; set; }
        [DisplayName("Tiempo estimado")]
        public Nullable<int> tiempoEstimado { get; set; }
        [DisplayName("Descripción")]
        public string descripcion { get; set; }
        [DisplayName("Asignado a")]
        public Nullable<int> idUsuarioAsignado { get; set; }
        [DisplayName("Proyecto")]
        public int idProyecto { get; set; }
        [DisplayName("Grupo")]
        public Nullable<int> idGrupo { get; set; }
        [DisplayName("Estado")]
        public int idEstado { get; set; }
        [DisplayName("Prioridad")]
        public int idPrioridad { get; set; }
        [DisplayName("Comentarios")]
        public string comentarios { get; set; }
        [DisplayName("Creado por")]
        public string idUsuarioCreador { get; set; }
        [DisplayName("Asignado a")]
        public string nombreUsuarioAsignado { get; set; }
        [DisplayName("Proyecto")]
        public string nombreProyecto { get; set; }
        [DisplayName("Grupo")]
        public string nombreGrupo { get; set; }
        [DisplayName("Estado")]
        public string nombreEstado { get; set; }
        [DisplayName("Prioridad")]
        public string nombrePrioridad { get; set; }
    }
}