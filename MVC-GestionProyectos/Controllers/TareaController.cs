using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVC_GestionProyectos.Models;
using MVC_GestionProyectos.Seguridad;
using MVC_GestionProyectos.Servicios;
using MVC_GestionProyectos.Servicios.MiServicios;

namespace MVC_GestionProyectos.Controllers
{
    [Authorize]
    public class TareaController : BaseController
    {

        private IServicios<Tarea> _servicio;
        private IServicios<Proyecto> _servicioProyecto;
        private IServicios<Usuario> _servicioUsuario;
        private IServicios<Prioridad> _servicioPrioridad;
        private IServicios<EstadoTarea> _servicioEstadoTarea;
        private IServicios<Proyecto_Tarea_Grupo> _servicioProyecto_Tarea_Grupo;

        
        public TareaController(IServicios<Tarea> _servicio, IServicios<Proyecto> _servicioProyecto, IServicios<Usuario> _servicioUsuario,
            IServicios<Prioridad> _servicioPrioridad, IServicios<EstadoTarea> _servicioEstadoTarea, IServicios<Proyecto_Tarea_Grupo> _servicioProyecto_Tarea_Grupo)
        {
            this._servicio = _servicio;
            this._servicioProyecto = _servicioProyecto;
            this._servicioUsuario = _servicioUsuario;
            this._servicioPrioridad = _servicioPrioridad;
            this._servicioEstadoTarea = _servicioEstadoTarea;
            this._servicioProyecto_Tarea_Grupo = _servicioProyecto_Tarea_Grupo;
        }


        // GET: Tarea
        public ActionResult Index(String id)
        {
            Session["idProyectoActual"] = id;
            var parametros = new Dictionary<String, String>();
            parametros.Add("idProyecto", id);
            var proyecto = _servicioProyecto.Get(int.Parse(id));
            var tareas = _servicio.Get(parametros);
            Session["nombreProyecto"] = proyecto.nombre;
            return View(tareas);
        }

        public ActionResult TodasLasTareas()
        {
            var lista = _servicio.Get();
            return View(lista);
        }

        public ActionResult MisTareas(int id)
        {
            var url = "?idUsuarioAsignado=" + id;
            var lista = _servicio.Get(url);
            return View(lista);
        }

        //Controlador para mostrar tareas en la barra del header --Daniel
        public ActionResult MisTareasHeader(int id)
        {
            var url = "?idUsuarioAsignado=" + id;
            var lista = _servicio.Get(url);
            return PartialView(lista);
        }
        

        public ActionResult Alta()
        {
            var parametros = new Dictionary<String, String>();
            parametros.Add("idProyecto", Session["idProyectoActual"].ToString());
            var usuariosProyecto = _servicioUsuario.Get(parametros);
            ViewBag.idUsuarioAsignado = new SelectList(usuariosProyecto, "id", "nombre");

            var parametros2 = new Dictionary<String, String>();
            parametros2.Add("idPrioridad", "nombre");
            var nombrePrioridades = _servicioPrioridad.Get();
            ViewBag.idPrioridad = new SelectList(nombrePrioridades, "id", "nombre");

            var parametros3 = new Dictionary<String, String>();
            parametros3.Add("idEstado", "nombre");
            var nombreEstados = _servicioEstadoTarea.Get();
            ViewBag.idEstado = new SelectList(nombreEstados, "id", "nombre");

            return View(new Tarea());
        }

        public ActionResult AltaTareaProyecto(String id)
        {
            var proyecto = _servicioProyecto.Get(int.Parse(id));
            Session["nombreProyecto"] = proyecto.nombre;
            Session["idProyectoActual"] = id;
            return RedirectToAction("ALta");

        }
        //para dar de alta una tarea en cualquier proyecto.
        public ActionResult AltaNueva()
        {
            var id = GetPrincipal().MiCustomIdentity.Proyecto_Tarea_Grupo.Select(o => o.idProyecto).Distinct();

            List<Proyecto> proyectos = new List<Proyecto>();
            if (id != null || id.Count() > 0 )
            {
                 foreach (var i in id)
                 {
                     Proyecto p = _servicioProyecto.Get(i);
                     proyectos.Add(p);       
                }
                ViewBag.idProyectosDeEsteUsuario = proyectos;
            }

            //var parametros = new Dictionary<String, String>();
            //parametros.Add("idProyecto", Session["idProyectoActual"].ToString());
            //var usuariosProyecto = _servicioUsuario.Get(parametros);
            //ViewBag.idUsuarioAsignado = new SelectList(usuariosProyecto, "id", "nombre");

            var parametros2 = new Dictionary<String, String>();
            parametros2.Add("idPrioridad", "nombre");
            var nombrePrioridades = _servicioPrioridad.Get();
            ViewBag.idPrioridad = new SelectList(nombrePrioridades, "id", "nombre");

            var parametros3 = new Dictionary<String, String>();
            parametros3.Add("idEstado", "nombre");
            var nombreEstados = _servicioEstadoTarea.Get();
            ViewBag.idEstado = new SelectList(nombreEstados, "id", "nombre");

            return View(new Tarea());
        }

        [HttpPost]
        public ActionResult Alta(Tarea model)
        {
            var idp = int.Parse(Session["idProyectoActual"].ToString());

            model.idProyecto = idp;
            model.fechaCreacion = DateTime.Now;
            model.fechaAsignacion = DateTime.Now;
            var miIdUsuarioAsignado= model.idUsuarioAsignado.Value;
            var miGrupo = _servicioProyecto_Tarea_Grupo.Get().ToList();
            var idgrupoNuevo = miGrupo.Find(o => o.idUsuario == miIdUsuarioAsignado && o.idProyecto == idp);
            model.idGrupo = idgrupoNuevo.idGrupo;
            model.idEstado = 1;
            _servicio.Add(model);

            return RedirectToAction("Index", new { id = idp });
        }

        public ActionResult Details(int id)
        {
            var tarea = _servicio.Get(id);
            ViewBag.nombreTarea = tarea.nombre;
            return View(tarea);

        }

        public ActionResult Edit(int id)
        {
            var tarea = _servicio.Get(id);

            //var parametros = new Dictionary<String, String>();
            //parametros.Add("idProyecto", Session["idProyectoActual"].ToString());
            //var usuariosProyecto = _servicioUsuario.Get(parametros);
            //ViewBag.idUsuarioAsignado = new SelectList(usuariosProyecto, "id", "nombre", tarea.idUsuarioAsignado);

            var parametros = new Dictionary<String, String>();
            parametros.Add("idProyecto", tarea.idProyecto.ToString());
            var usuariosProyecto = _servicioUsuario.Get(parametros);
            ViewBag.idUsuarioAsignado = new SelectList(usuariosProyecto, "id", "nombre", tarea.idUsuarioAsignado);



            var parametros2 = new Dictionary<String, String>();
            parametros2.Add("idPrioridad", "nombre");
            var nombrePrioridades = _servicioPrioridad.Get();
            ViewBag.idPrioridad = new SelectList(nombrePrioridades, "id", "nombre", tarea.idPrioridad);

            var parametros3 = new Dictionary<String, String>();
            parametros3.Add("idEstado", "nombre");
            var nombreEstados = _servicioEstadoTarea.Get();
            ViewBag.idEstado = new SelectList(nombreEstados, "id", "nombre", tarea.idEstado );


            ViewBag.nombreTarea = tarea.nombre;
            return View(tarea);
        }

        //[HttpPost]
        //public async Task<ActionResult> Edit(int id, Tarea collection)
        //{
        //    var idp = int.Parse(Session["idProyectoActual"].ToString());

        //    try
        //    {
        //        await _servicio.Update(id, collection);
        //        return RedirectToAction("Index", new { id = idp });
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Index", new { id = idp });
        //    }
        //}



        [HttpPost]
        public async Task<ActionResult> Edit(int id, Tarea collection)
        {
            var nuevoIdGrupo = _servicioProyecto_Tarea_Grupo.Get().ToList();
                //Select(o => o.idProyecto==collection.idProyecto && o.idUsuario == collection.idUsuarioAsignado).ToList();
            var x = nuevoIdGrupo.Find(o => o.idProyecto == collection.idProyecto && o.idUsuario == collection.idUsuarioAsignado);
            collection.idGrupo = x.idGrupo;


            //var idp = int.Parse(Session["idProyectoActual"].ToString());
            var idp = id;
            try
            {
                await _servicio.Update(id, collection);
                //return RedirectToAction("Details", new { id = GetPrincipal().MiCustomIdentity.id });
                return RedirectToAction("Details", new { id = idp });
            }
            catch
            {
                return RedirectToAction("Edit", new { id = idp });
            }
        }




        public async Task<ActionResult> Delete(int id)
        {
            var idp = int.Parse(Session["idProyectoActual"].ToString());
            await BorrarTarea(id);
            return RedirectToAction("Index", new { id = idp });
        }

        [HttpPost]
        public async Task<ActionResult> BorrarTarea(int id)
        {
            var idp = int.Parse(Session["idProyectoActual"].ToString());

            try
            {
                await _servicio.Delete(id);
                return RedirectToAction("Index", new { id = idp });
            }
            catch
            {
                return RedirectToAction("Index", new { id = idp });
            }
        }


    }
}
