using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVC_GestionProyectos.Models;
using MVC_GestionProyectos.Seguridad;
using MVC_GestionProyectos.Utils;
using MVC_GestionProyectos.Servicios;


namespace MVC_GestionProyectos.Controllers
{

    public class UsuarioController : BaseController
    {
        private IServicios<Usuario> _servicio;
        private IServicios<Habilidad> _servicioHabilidad;
        private IServicios<Proyecto_Tarea_Grupo> _servicioListadoProyectos; 

        public UsuarioController(IServicios<Usuario> servicio, IServicios<Habilidad> servicioHabilidad,
            IServicios<Proyecto_Tarea_Grupo> servicioListadoProyectos)
        {
            this._servicio = servicio;
            this._servicioHabilidad = servicioHabilidad;
            this._servicioListadoProyectos = servicioListadoProyectos;
        }

        // GET: Usuario
        [Authorize]
        public ActionResult Index()
        {
            var usr = User as PrincipalPersonalizado;
            var uA = usr.MiCustomIdentity.Proyecto_Tarea_Grupo.Select(o => o.idProyecto).ToList();
            var lista = _servicio.Get();
            var lista2 = (from l in lista from u in uA where l.Proyecto_Tarea_Grupo.Select(o => o.idProyecto).ToList().Contains(u) select l).ToList();
            var lista3 = lista2.Distinct();

            return View(lista3);

            //var _servicio = DependencyResolver.Current.GetService<IServicios<Usuario>>();
            //return View(_servicio.Get());

        }
        
        // GET: Usuario/Details/5
         [Authorize]
        public ActionResult Details(int id)
        {
            var _servicioP = DependencyResolver.Current.GetService<IServicios<Proyecto_Tarea_Grupo>>();
            var parametros = "?args=" + id;
            var usuProy = _servicioP.Get(parametros);
            ViewBag.MisProyectos = usuProy;

            var usu = _servicio.Get(id);
            return View(usu);
        }

        // GET: Usuario/Habilidad/php
         [Authorize]
        public ActionResult Habilidad(String id)
        {
            var url = "?Habilidad=" + id;
            var lista = _servicio.Get(url);
            return View(lista);
        }

        // GET: Buscar
         [Authorize]
        public ActionResult Busqueda()
        {
            return View();
        }

        // Post: Usuario/Nombre/php
         [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(String parametro, String dato)
        {
            var url = "?"+parametro+"=" + dato;
            var lista = _servicio.Get(url);

            var usr = User as PrincipalPersonalizado;
            var uA = usr.MiCustomIdentity.Proyecto_Tarea_Grupo.Select(o => o.idProyecto).ToList();

            var lista2 = (from l in lista from u in uA where l.Proyecto_Tarea_Grupo.Select(o => o.idProyecto).ToList().Contains(u) select l).ToList();
            var lista3 = lista2.Distinct();

            return View(lista3);
        }
            
        // GET: Usuario/Alta
         
        public ActionResult Alta()
        {
            var _servicioH = DependencyResolver.Current.GetService<IServicios<Habilidad>>();
            var aux =_servicioH.Get();
            ViewBag.ListadoHabilidades = aux;
            return View(new Usuario());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Alta(Usuario model, HttpPostedFileBase foto)
        {
            var todasHabi = _servicioHabilidad.Get();

            var fotoHiden = Request["fotoHiden"];
            var urlBase = Server.MapPath("~/img/FotoPerfilesUsuario");
            string ext = "";
            if (foto != null)
            {
                ext = foto.FileName.Substring(foto.FileName.LastIndexOf("."));
                foto.SaveAs(urlBase + "/" + model.email + ext);
            }

            #region cargamos las habilidades
            var HabStr = Request["Habilidad"]; //id de las habilidades...
            if (!String.IsNullOrEmpty(HabStr))
            {
                var HabLst = Request["Habilidad"].Split(',').Distinct().ToList();
                foreach (var h in HabLst)
                {
                    foreach (var H in todasHabi)
                    {
                        if (Convert.ToInt32(h) == H.id)
                        {
                            model.Habilidad.Add(todasHabi.Find(o => o.id == H.id));
                        }
                    }
                }
            }
            #endregion


            #region cargamos las nuevas Habilidades
            var nuevaHabStr = Request["nuevaHabilidad"];//nombre de las habilidades...
            if (!String.IsNullOrEmpty(nuevaHabStr))
            {
                var nuevaHabLst = Request["nuevaHabilidad"].Split(',').Distinct().ToList();
                List<Habilidad> ListHabNuevas = new List<Habilidad>();
                foreach (var n in nuevaHabLst)
                {
                    await _servicioHabilidad.Add(new Habilidad()
                    {
                        id = 0,
                        nombre = n
                    });

                }
                //listHabNuevas = listHabNuevas.Distinct().ToList();
                //foreach (var lN in listHabNuevas)
                //{
                //    await _servicioHabilidad.Add(lN);
                //}
                todasHabi = _servicioHabilidad.Get();
                foreach (var l in ListHabNuevas)
                {
                    if (todasHabi.Select(o => o.nombre).Contains(l.nombre))
                    {
                        model.Habilidad.Add(todasHabi.Single(o => o.nombre == l.nombre));
                    }
                }
                //collection.Habilidad.Add(_servicioH.Get((_servicioH.Get().Max(o => o.id))));
                //collection.Habilidad = collection.Habilidad.Distinct().ToList();
            }
            #endregion


            if (string.IsNullOrEmpty(model.foto))
            {
                model.foto = fotoHiden;
            }

            else
            {
                model.foto = model.email + ext;
            }

            await _servicio.Add(model);
            return RedirectToAction("Index");

        }

        // GET: Usuario/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var aux = _servicioHabilidad.Get();
            ViewBag.idHab = aux;

            var usuario = _servicio.Get(id);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Usuario collection, HttpPostedFileBase foto)
        {
            var todasHab = _servicioHabilidad.Get();
            var todasHabNombres = todasHab.Select(o => o.nombre).ToList();
            
            #region cargamos las habilidades
            var HabStr = Request["Habilidad"]; //id de las habilidades...
            if (!String.IsNullOrEmpty(HabStr))
            {
                var HabLst = Request["Habilidad"].Split(',').Distinct().ToList();
                foreach (var h in HabLst)
                {
                    foreach (var H in todasHab)
                    {
                        if (Convert.ToInt32(h) == H.id)
                        {
                            collection.Habilidad.Add(todasHab.Find(o => o.id == H.id));
                        }
                    }
                }
            }
            #endregion

            #region cargamos las nuevas Habilidades
            var nuevaHabStr = Request["nuevaHabilidad"];//nombre de las habilidades...
            if (!String.IsNullOrEmpty(nuevaHabStr))
            {
                var nuevaHabLst = Request["nuevaHabilidad"].Split(',').Distinct().ToList();
                List<Habilidad> listHabNuevas = new List<Habilidad>();
                foreach (var n in nuevaHabLst)
                {
                    listHabNuevas.Add(new Habilidad()
                    {
                        id = 0,
                        nombre = n
                    });
                }
                listHabNuevas = listHabNuevas.Distinct().ToList();
                foreach(var LN in listHabNuevas){
                    await _servicioHabilidad.Add(LN);
                }
                todasHab = _servicioHabilidad.Get();
                foreach(var l in listHabNuevas){
                    if(todasHab.Select(o => o.nombre).Contains(l.nombre)){
                        collection.Habilidad.Add(todasHab.Single(o => o.nombre == l.nombre));
                    }
                }
                //collection.Habilidad.Add(_servicioH.Get((_servicioH.Get().Max(o => o.id))));
                //collection.Habilidad = collection.Habilidad.Distinct().ToList();
            }
            #endregion



            var fotoHiden = Request["fotoHiden"];
            var urlBase = Server.MapPath("~/img/FotoPerfilesUsuario");

             var ext = "";
            
            if (foto != null)
             {
                 ext = foto.FileName.Substring(foto.FileName.LastIndexOf("."));
                 foto.SaveAs(urlBase + "/" + collection.email + ext);
             }

            try
            {
                
                if (string.IsNullOrEmpty(collection.foto))
                {
                    collection.foto = fotoHiden;
                }
                else
                {
                    collection.foto = collection.email + ext;
                }
                await _servicio.Update(id, collection);
                return RedirectToAction("Details", "Usuario", new { id = id });
            }
            catch
            {
                return RedirectToAction("Details", "Usuario", new { id = id });
            }
        }

        // GET: Usuario/Delete/5
         [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            await BorrarUsuario(id);
            return RedirectToAction("Index");
        }
        // POST: Usuario/Delete/5
         [Authorize]
        [HttpPost]
        public async Task<ActionResult> BorrarUsuario(int id)
        {
            try
            {
                await _servicio.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //public ActionResult HabilidadesDelUsuario(int id)
        //{

        //}
    }
}
