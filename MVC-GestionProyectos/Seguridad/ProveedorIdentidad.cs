using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using MVC_GestionProyectos.Models;

namespace MVC_GestionProyectos.Seguridad
{
    public class ProveedorIdentidad:IIdentity
    {
        public string Name
        {
            get { return String.Format("{0} {1}", nombre, apellido); }
        }
        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }
        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public IIdentity Identity { get; set; }
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string foto { get; set; }
        public List<Habilidad> Habilidades { get; set; }
        public List<Proyecto_Tarea_Grupo> Proyecto_Tarea_Grupo { get; set; }

        public int contTareas { get; set; }

        public List<Mensaje> Mensaje { get; set; } 

        public ProveedorIdentidad(IIdentity identity)
        {
            Identity = identity;
            var usuario = (UsuarioProveedorIdentidad)Membership.GetUser(identity.Name, false);

            nombre = usuario.nombre;
            apellido = usuario.apellido;
            email = usuario.email;
            foto = usuario.foto;
            id = usuario.id;
            Habilidades = usuario.Habilidades;
            Proyecto_Tarea_Grupo = usuario.Proyecto_Tarea_Grupo;
            //Roles = usuario.Roles;

            Mensaje = usuario.Mensaje;
            contTareas = usuario.contTareas;
        }


    }
}