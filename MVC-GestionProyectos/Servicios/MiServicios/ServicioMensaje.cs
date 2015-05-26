using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_GestionProyectos.Models;

namespace MVC_GestionProyectos.Servicios.MiServicios
{
    public class ServicioMensaje : Servicios<Mensaje>
    {
        public ServicioMensaje()
        {
            UrlBase = "http://webapiproyectos.azurewebsites.net/api/Mensaje";
        }
    }
}