using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_GestionProyectos.Models;
using MVC_GestionProyectos.Servicios;

namespace MVC_GestionProyectos.Controllers
{
    public static class OnlineUsers
    {
        static List<int> UsuariosConectados;
        static List<int> UsuariosConectadosDistinct;
        
        public static void UsuarioStart(int us)
        {
            UsuariosConectados.Add(us);
        }

        public static void UsuarioEnd(int idUs)
        {
            UsuariosConectadosDistinct = (List<int>) UsuariosConectados.Distinct();
            var u = UsuariosConectados.Contains(idUs);
            if (!u)
            {
                UsuariosConectados.Add(idUs);
            }
        }
        public static List<int> UsuariosOnline()
        {
            return UsuariosConectados;
        }
    }
}