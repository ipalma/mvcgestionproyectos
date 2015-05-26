using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_GestionProyectos.Models;
using MVC_GestionProyectos.Servicios;

namespace MVC_GestionProyectos.Controllers
{
    [Authorize]
    public class ListadoProyectosController : Controller
    {
        private IServicios<Proyecto_Tarea_Grupo> _serviciosPtg;
        private IServicios<Proyecto> _serviciosProyecto;
        private IServicios<Usuario> _serviciosUsuario;

        public ListadoProyectosController(IServicios<Proyecto_Tarea_Grupo> serviciosPtg, IServicios<Proyecto> serviciosProyecto, IServicios<Usuario> serviciosUsuario)
        {
            _serviciosPtg = serviciosPtg;
            _serviciosProyecto = serviciosProyecto;
            _serviciosUsuario = serviciosUsuario;
        }

        // GET: ListaProyectos
       /* public ActionResult ListadoProyectos()
        {
            var lista = _servicios.Get();
            return View(lista);
        }

        /*public ActionResult ListadoProyectos(int id)
        {
            var lista = _servicios.Get(id);
            return View(lista);
        }*/
    }
}