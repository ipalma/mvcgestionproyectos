﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_GestionProyectos.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: /ErrorPage/
        public ActionResult NotFound()
        {
            return View();
        }
	}
}