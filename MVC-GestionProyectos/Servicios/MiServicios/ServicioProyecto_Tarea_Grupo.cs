using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_GestionProyectos.Models;

namespace MVC_GestionProyectos.Servicios.MiServicios
{
    public class ServicioProyecto_Tarea_Grupo : Servicios<Proyecto_Tarea_Grupo>

{
        public ServicioProyecto_Tarea_Grupo()
        {
            UrlBase = "http://webapiproyectos.azurewebsites.net/api/Proyecto_Tarea_Grupo";
        }
}
}