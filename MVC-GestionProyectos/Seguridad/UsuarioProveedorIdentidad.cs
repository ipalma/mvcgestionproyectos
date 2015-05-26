using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using MVC_GestionProyectos.Models;

namespace MVC_GestionProyectos.Seguridad
{
    public class UsuarioProveedorIdentidad : MembershipUser
    {
        //public String[] Roles { get; set; }
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string foto { get; set; }
        public List<Habilidad> Habilidades { get; set; }
        public List<Proyecto_Tarea_Grupo> Proyecto_Tarea_Grupo { get; set; }
        public int contTareas { get; set; }
        public List<Mensaje> Mensaje { get; set; } 
        
        public UsuarioProveedorIdentidad(Usuario usuario)
        {
            nombre = usuario.nombre;
            apellido = usuario.apellido;
            email = usuario.email;
            foto = usuario.foto;
            id = usuario.id;
            Habilidades = usuario.Habilidad;
            Proyecto_Tarea_Grupo = usuario.Proyecto_Tarea_Grupo;
            Mensaje = usuario.Mensaje;
            contTareas = contTareas2();

            //Roles = usuario.Rol.Select(o => o.nombre).ToArray();
            // contTareas = usuario.Proyecto_Tarea_Grupo.Select(o => o.Tarea).Count();
        }

        public int contTareas2()
        {
            var cont = 0;
            foreach (var x in Proyecto_Tarea_Grupo)
            {

                foreach (var t in x.Tarea)
                {
                    if ((t.idUsuarioAsignado == id) || (t.nombreUsuarioAsignado == nombre))
                    {
                       cont++;
                    }
                   
                }
               
                
            }
            return cont;
        }
    }
}