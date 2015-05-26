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
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using MVC_GestionProyectos.Models;
using MVC_GestionProyectos.Seguridad;
using MVC_GestionProyectos.Servicios;
using MVC_GestionProyectos.Utils;


namespace MVC_GestionProyectos.Controllers
{
    [AllowAnonymous]
    public class LoginUsuarioController : Controller
    {

        private IServicios<Usuario> _servicio;

        public LoginUsuarioController(IServicios<Usuario> servicio)
        {
            _servicio = servicio;
        }


        // GET: Usuario
        public ActionResult Login()
        {

            if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
            {
                return View(new LoginUsuario()
                {
                    email = Request.Cookies["UserName"].Value,
                    password = Request.Cookies["Password"].Value
                });
            }
            else
            {
                return View(new LoginUsuario());
            }
        }

        public ActionResult LoginError()
        {
            return View(new LoginUsuario());
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(String email, String password)
        //{
        //    var usuario = _servicio.GetLogin(email, password);

        //    if (usuario != null)
        //    {
        //       Session["usuarioLogeado"] = usuario;
        //        return RedirectToAction("Index", "Home"); 
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login"); 
        //    }
        //}

        [HttpPost]
        public ActionResult Login(LoginUsuario model)
        {
            var check = Request["recuerdame"];
            Login_Click(model, check);
            
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.email, model.password))
                {
                    FormsAuthentication.RedirectFromLoginPage(model.email, false);
                    return null;
                }
                else
                {
                    ModelState.AddModelError("", "Email y/o contaseña incorrectos");
                    return View("Login");
                }

            }
            else
            {
                return View("Login");
            }
  }
        protected void Login_Click(LoginUsuario model, String ck)
        {
            if (ck == "recuerdame")
            {
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
            }
            else
            {
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

            }
            Response.Cookies["UserName"].Value = model.email.Trim();
            Response.Cookies["Password"].Value = model.password.Trim();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}