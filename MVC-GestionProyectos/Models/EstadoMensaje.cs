using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_GestionProyectos.Models
{
    using System;
    using System.Collections.Generic;

    public partial class EstadoMensaje
    {
        public EstadoMensaje()
        {
            this.Mensaje = new HashSet<Mensaje>();
        }

        public int Id { get; set; }
        public string nombre { get; set; }

        public virtual ICollection<Mensaje> Mensaje { get; set; }
    }
}