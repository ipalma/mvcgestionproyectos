using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using MVC_GestionProyectos.Models;
using MVC_GestionProyectos.Utils;

namespace MVC_GestionProyectos.Servicios.MiServicios
{
    public class ServicioListadoProyectos: Servicios<Proyecto_Tarea_Grupo>
    {

        public ServicioListadoProyectos()
        {
            UrlBase = "http://webapiproyectos.azurewebsites.net/api/Proyecto_Tarea_Grupo";
        }


    }
}