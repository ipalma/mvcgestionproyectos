using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MVC_GestionProyectos.Models;
using MVC_GestionProyectos.Utils;
using MVC_GestionProyectos.Servicios;

namespace MVC_GestionProyectos.Controllers
{
    [Authorize]
    public class ProyectoController : BaseController
    {
        private IServicios<Proyecto> _servicio;

        public ProyectoController(IServicios<Proyecto> servicio)
        {
            this._servicio = servicio;
        }

        #region index del proyecto

        // GET: Proyecto
        public ActionResult Index()
        {
            var lista = _servicio.Get();
            return View(lista);
        }

        #endregion
        
        #region alta de proyecto

        // GET: Usuario/Alta
        public ActionResult Alta()
        {
            var _servicioPrioridad = DependencyResolver.Current.GetService<IServicios<Prioridad>>();
            var PrioridadProyecto = _servicioPrioridad.Get();
            ViewBag.Prioridad = PrioridadProyecto;

            var _servicioEstado = DependencyResolver.Current.GetService<IServicios<EstadoProyecto>>();
            var EstadoProy = _servicioEstado.Get();
            ViewBag.EstadoProyecto = EstadoProy;

            var proyecto = new Proyecto();
            proyecto.fechaCreacion = DateTime.Now;

            return View(proyecto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Alta(Proyecto model)
        {
            var _servicioPTG = DependencyResolver.Current.GetService<IServicios<Proyecto_Tarea_Grupo>>();

            var identificadorUsuario = GetPrincipal().MiCustomIdentity.id;
            int identificadorProyecto = 0;
            var identificadorGrupo = 1;

            var estado = Request["id_estado_alta"];
            int idestado = 0;
            bool idest = Int32.TryParse(estado, out idestado);

            var prioridad = Request["id_prioridad_alta"];
            int idprioridad = 0;
            bool idprio = Int32.TryParse(prioridad, out idprioridad);

            model.idEstado = idestado;
            model.idPrioridad = idprioridad;





            if (ModelState.IsValid)
            {

                await _servicio.Add(model);


                var url = "?nombre=" + model.nombre;
                var proyecto = _servicio.Get(url).ToArray<Proyecto>();
                var idproyecto = proyecto[0];
                identificadorProyecto = proyecto[0].id;

                Proyecto_Tarea_Grupo PTG = new Proyecto_Tarea_Grupo()
                {
                    idUsuario = identificadorUsuario,
                    idGrupo = identificadorGrupo,
                    idProyecto = identificadorProyecto,
                    jefeGrupo = identificadorUsuario
                };

                await _servicioPTG.Add(PTG);


                return RedirectToAction("Edit", idproyecto);
            }

            return View(model);


        }

        #endregion

        #region busqueda de proyecto

        // GET: Buscar
        public ActionResult Busqueda()
        {
            return View();
        }

        // Post: Proyecto/Nombre/*
        //[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buscar(String nombre)
        {
            var url = "?nombre=" + nombre;
            var lista = _servicio.Get(url);
            return View(lista);
        }

        #endregion

        #region Métodos Create(en desuso)
        // POST: Proyecto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region detalles del proyecto
        // GET: Proyecto/Details/5
        public ActionResult Details(int id)
        {

            var proyecto = _servicio.Get(id);
            return View(proyecto);
        }
        #endregion

        #region editar proyecto
        // GET: Proyecto/Edit/5
        public ActionResult Edit(int id)
        {
            var _servicioU = DependencyResolver.Current.GetService<IServicios<Usuario>>();
            var ProyUsu = _servicioU.Get();
            var _servicioG = DependencyResolver.Current.GetService<IServicios<Grupo>>();
            var GrupoProyUsu = _servicioG.Get();
            ViewBag.GrupoProyecto = GrupoProyUsu;
            
            var _servicioPrioridad = DependencyResolver.Current.GetService<IServicios<Prioridad>>();
            var PrioridadProyecto = _servicioPrioridad.Get();
            ViewBag.Prioridad = PrioridadProyecto;

            var _servicioEstado = DependencyResolver.Current.GetService<IServicios<EstadoProyecto>>();
            var EstadoProy = _servicioEstado.Get();
            ViewBag.EstadoProyecto = EstadoProy;


            var proyecto = _servicio.Get(id);
            List<int> valores = new List<int>();
            foreach (var usuario in proyecto.usuarios)
            {
                valores.Add(usuario.id);
            }


            var listaUsuariosProyecto = ProyUsu.Where(o => !(valores.Contains(o.id))).ToList();

            ViewBag.Usuarios = listaUsuariosProyecto;

 

            return View(proyecto);
        }

        // POST: Proyecto/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Proyecto collection)
        {
            var _servicioPTG = DependencyResolver.Current.GetService<IServicios<Proyecto_Tarea_Grupo>>();

            var idUsuarios = Request["id_usuarios[]"];
            var idGrupos = Request["id_grupos"];
            var PTG = new Proyecto_Tarea_Grupo();

            var estado = Request["id_estado"];
            int idestado = 0;
            bool idest = Int32.TryParse(estado, out idestado);

            var prioridad = Request["id_prioridad"];
            int idprioridad = 0;
            bool idprio = Int32.TryParse(prioridad, out idprioridad);

            collection.idEstado = idestado;
            collection.idPrioridad = idprioridad;

            if (idUsuarios != null && idGrupos != null)
            {
                var idusuarios = idUsuarios.Split(',');
                var idgrupos = idGrupos.Split(',');

                String[] nuevoArray = new String[idUsuarios.Length];
                var i = 0;

                foreach (var idGrupo in idgrupos)
                {
                    if (idGrupo != "0")
                    {
                        if (i < nuevoArray.Length)
                        {
                            nuevoArray[i] = idGrupo;
                            i++;
                        }
                    }
                }

                try
                {
                    if (nuevoArray.Length != 0)
                    {
                        for (var j = 0; j < nuevoArray.Length; j++)
                        {
                            int valor2 = 0;
                            bool res2 = Int32.TryParse(idusuarios[j], out valor2);
                            PTG.idUsuario = valor2;
                            PTG.idProyecto = id;
                            int valor1 = 0;
                            bool res1 = Int32.TryParse(nuevoArray[j], out valor1);
                            PTG.idGrupo = valor1;
                            await _servicioPTG.Add(PTG);
                        }
                    }
                    await _servicio.Update(id, collection);
                    return RedirectToAction("Details", new { id = id});
                }
                catch (Exception)
                {
                    return RedirectToAction("Details", new { id = id });
                }
            }
            else
            {
                try
                {
                    await _servicio.Update(id, collection);
                    return RedirectToAction("Details", new { id = id });
                }
                catch (Exception)
                {
                    return RedirectToAction("Details", new { id = id });
                }

            }

        }
        #endregion

        #region eliminar proyecto
        // GET: Proyecto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Proyecto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}
