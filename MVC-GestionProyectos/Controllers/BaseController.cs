using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_GestionProyectos.Seguridad;

namespace MVC_GestionProyectos.Controllers
{
   
    public class BaseController : Controller
    {
        protected PrincipalPersonalizado GetPrincipal()
        {
            var p = User as PrincipalPersonalizado;
            return p;


        }
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{

        //    if (filterContext.ActionDescriptor.ActionName.Equals("Edit") && filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.Equals("Usuario"))
        //    {

        //       var d = GetPrincipal().MiCustomIdentity.Proyecto_Tarea_Grupo.Where(o => o.idProyecto == 11);
        //     if(!d.Any())
        //            filterContext.Result = new RedirectResult("/Login/Index");
        //    }
            
        //    base.OnActionExecuting(filterContext);
        //}

         
	}
}