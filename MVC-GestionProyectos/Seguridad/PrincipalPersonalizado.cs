﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MVC_GestionProyectos.Seguridad
{
    public class PrincipalPersonalizado:IPrincipal
    {
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
            //return MiCustomIdentity.Roles.Contains(role);
        }

        public IIdentity Identity { get; private set; }

        public ProveedorIdentidad MiCustomIdentity
        {
            get { return (ProveedorIdentidad)Identity; }
            set { Identity = value; }
        }
        public PrincipalPersonalizado(ProveedorIdentidad identity)
        {
            Identity = identity;
        }
    }
}