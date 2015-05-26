using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_GestionProyectos.Models;

namespace MVC_GestionProyectos.Servicios.MiServicios
{
    public class ServicioProyecto : Servicios<Proyecto>
    {
        public ServicioProyecto()
        {
            UrlBase = "http://webapiproyectos.azurewebsites.net/api/Proyecto";
        }
    }
}