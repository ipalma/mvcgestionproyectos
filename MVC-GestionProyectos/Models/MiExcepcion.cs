using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_GestionProyectos.Models
{
    public class MiExcepcion:Exception
    {
        public MiExcepcion(String msg) : base(msg)
        {
            
        }
    }
}