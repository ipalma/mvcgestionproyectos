using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVC_GestionProyectos.Models;
using MVC_GestionProyectos.Seguridad;
using MVC_GestionProyectos.Servicios;
using Repositorio;

namespace MVC_GestionProyectos.Controllers
{
    public class MensajeController : BaseController
    {
        private IServicios<Mensaje> _servicioMensaje;
        private IServicios<Usuario> _servicioUsuario;


        public MensajeController(IServicios<Mensaje> _servicioMensaje, IServicios<Usuario> _servicioUsuario)
        {
            this._servicioMensaje = _servicioMensaje;
            this._servicioUsuario = _servicioUsuario;
        }

        // GET: Mensaje
        public ActionResult Index()
        {
            var usr = User as PrincipalPersonalizado;
            //var uA = usr.MiCustomIdentity.Proyecto_Tarea_Grupo.Select(o => o.idProyecto).ToList();
            var lista = _servicioMensaje.Get();
            //var lista2 = (from l in lista from u in uA where l.Proyecto_Tarea_Grupo.Select(o => o.idProyecto).ToList().Contains(u) select l).ToList();
            //var lista3 = lista2.Distinct();

            return View(lista);
        }

        // GET: Mensaje/Details/5
        public ActionResult Details(int id)
        {
            var lista2 = _servicioMensaje.Get(id);
            return View((IEnumerable<Mensaje>) lista2);
        }

        // GET: Mensaje/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mensaje/Create
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

        // GET: Mensaje/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mensaje/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Mensaje/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mensaje/Delete/5
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


        public ActionResult Alta()
        {
            ViewBag.idDest = new SelectList(_servicioUsuario.Get(), "id", "nombre");
            
            return PartialView(new Mensaje());
        }

        [HttpPost]
        public void Alta(Mensaje model)
        {
            var usr = User as PrincipalPersonalizado;

            model.idRem = usr.MiCustomIdentity.id;
            model.emailRem = usr.MiCustomIdentity.email;
            model.fotoRem = usr.MiCustomIdentity.foto;
            model.fecha = DateTime.Now;
            model.nombreRem = usr.MiCustomIdentity.nombre;
            model.estado = 1;

            //var idDestino = model.idDest;
            //var miGrupo = _servicioProyecto_Tarea_Grupo.Get().ToList();
            //var idDestFinal = miGrupo.Find(o => o.idUsuario == idDestino);
            //model.idDest = idDestFinal.idUsuario;

             _servicioMensaje.Add(model);


        }
       
    }
}
