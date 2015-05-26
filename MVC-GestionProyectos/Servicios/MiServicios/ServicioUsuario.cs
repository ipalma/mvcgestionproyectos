using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_GestionProyectos.Models;

namespace MVC_GestionProyectos.Servicios.MiServicios
{
    public class ServicioUsuario : Servicios<Usuario>
    {
        public ServicioUsuario()
        {
            UrlBase = "http://webapiproyectos.azurewebsites.net/api/Usuario";
        }
    }
}