using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MVC_GestionProyectos.Models;
using MVC_GestionProyectos.Servicios;

namespace MVC_GestionProyectos.Controllers
{
    [Authorize]
    public class ProyectosUsuarioController : BaseController
    {
        private IServicios<Proyecto_Tarea_Grupo> _servicios;
        private IServicios<Usuario> _serviciosUsuario; 

        public ProyectosUsuarioController(IServicios<Proyecto_Tarea_Grupo> servicios, IServicios<Usuario> serviciosUsuario)
        {
            _servicios = servicios;
            _serviciosUsuario = serviciosUsuario;
        }

        #region Morralla

        // GET: ListaProyectos
        /*public ActionResult ListadoProyectos()
        {
            var lista = _servicios.Get();
            return View(lista);
        }*/

        //public ActionResult ListaProyectosUsuario()
        //{
        //    var id = GetPrincipal().MiCustomIdentity.id;
        //    Session["idUsuarioActual"] = id;
        //    var parametros = new Dictionary<String, String>();
        //    parametros.Add("UsuarioNumero", id.ToString());
        //    var proyectos = _servicios.Get(parametros);
        //    return View(proyectos);            
        //}

        #endregion

        #region Vista Mis Proyectos

        public ActionResult ListaProyectosUsuario()
        {
            var id = GetPrincipal().MiCustomIdentity.id;
            Session["idUsuarioActual"] = id;
            var parametros = "?args=" + id;
            var proyectos = _servicios.Get(parametros);
            return View(proyectos);
        }

        #endregion

        #region Listado de Usuarios Participantes del Proyecto

        public ActionResult ListadoUsuarios(int id)
        {
            var usuarios = DependencyResolver.Current.GetService<IServicios<Usuario>>();
            var url = "?args=" + id;

            var listUsuarios = usuarios.Get(url);
            return View(listUsuarios);
        }

        #endregion

        #region Vista Parcial de Proyectos de la cabecera

        [AllowAnonymous]
        public ActionResult ProyectosHeader()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var id = GetPrincipal().MiCustomIdentity.id;
                Session["idUsuarioActual"] = id;
                var parametros = "?args=" + id;
                var proyectos = _servicios.Get(parametros);


                return PartialView("_header", proyectos);

            }
            else
            {
                var proyectos = _servicios.Get();

                return PartialView("_header", proyectos);
            }

        }

        #endregion



        public async Task<ActionResult> EliminarDelListado(int idUsu, int idProy)
        {

            await _servicios.DeleteFunct(idUsu, idProy);

            return RedirectToAction("Edit", "Proyecto", new { id = idProy });
        }
    }
}