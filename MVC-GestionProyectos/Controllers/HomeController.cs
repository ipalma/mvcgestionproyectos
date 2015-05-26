using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using MVC_GestionProyectos.Models;
using MVC_GestionProyectos.Servicios;

namespace MVC_GestionProyectos.Controllers
{

    [Authorize]
    public class HomeController : BaseController
    {
        private IServicios<Usuario> _servicio;
        private IServicios<Proyecto_Tarea_Grupo> _servicioListadoProyectos;
        private IServicios<Proyecto> _servicioProyecto;

        public HomeController(IServicios<Usuario> servicio,
            IServicios<Proyecto_Tarea_Grupo> servicioListadoProyectos, IServicios<Proyecto> servicioProyecto)
        {
            this._servicio = servicio;
            this._servicioListadoProyectos = servicioListadoProyectos;
            this._servicioProyecto = servicioProyecto;
        }

        // GET: Home
        public ActionResult Index()
        {
            //var ide = GetPrincipal().MiCustomIdentity;
            
            //var servicioP = DependencyResolver.Current.GetService<IServicios<Proyecto_Tarea_Grupo>>();
            ViewBag.BuscarProyectos = _servicioProyecto.Get();

            return View();
        }
        
        //public int BuscarProyectos(int id)
        //{
        //    var servicioP = DependencyResolver.Current.GetService<IServicios<Proyecto>>();

        //    var proy = servicioP.Get(id);

        //    return PartialView(proy);

        //}
        //[Authorize]
        //public ActionResult ProyectosCompartidos(int id)
        //{
        //    var _servicioP = DependencyResolver.Current.GetService<IServicios<Proyecto_Tarea_Grupo>>();
        //    var parametros = "?args=" + id;
        //    var usuProy = _servicioP.Get(parametros);
        //    ViewBag.MisProyectos = usuProy;

        //    var usu = _servicio.Get(id);
        //    return View(usu);
        //}

    }
}