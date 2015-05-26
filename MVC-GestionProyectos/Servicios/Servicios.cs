using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using MVC_GestionProyectos.Controllers;
using MVC_GestionProyectos.Models;
using MVC_GestionProyectos.Utils;

namespace MVC_GestionProyectos.Servicios
{
    public class Servicios<TModelo> : IServicios<TModelo>
    {
        private String _urlBase;

        public String UrlBase
        {
            get { return _urlBase; }
            set { _urlBase = value; }
        }

        public async Task Add(TModelo modelo)
        {
            var serializado = Serializacion<TModelo>.Serializar(modelo);
 
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var contenido = new StringContent(serializado);
                    contenido.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await client.PostAsync(new Uri(UrlBase), contenido);
                }

            }
        }

        public async Task Update(int id, TModelo modelo)
        {
            var serializado = Serializacion<TModelo>.Serializar(modelo);
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var contenido = new StringContent(serializado);
                    contenido.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    UrlBase += "/"+id; 
                    await client.PutAsync(new Uri(UrlBase), contenido);
                }
            }
        }

        public async Task Delete(int id)
        {
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    await client.DeleteAsync(new Uri(UrlBase + "/" + id));
                }
            }
        }

        public async Task DeleteFunct(int idUsu, int idProy)
        {
            var datos = "?idUsuario=" + idUsu + "&idProyecto=" + idProy;

            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    await client.DeleteAsync(new Uri(UrlBase+datos));
                }
            }
        }

        public int GetMax()
        {
            int identificador;
            var cl = WebRequest.Create(UrlBase + "/GetMax");
            cl.Method = "GET";
            var res = cl.GetResponse();
            using (var stream = res.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    identificador = Serializacion<int>.Deserializar(resultado);
                }
            }
            return identificador;
        }

        public List<TModelo> Get()
        {
            List<TModelo> lista;
            var cl = WebRequest.Create(UrlBase);
            cl.Credentials = new NetworkCredential();

            cl.Method = "GET";
            var res = cl.GetResponse();
            using (var stream = res.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    lista = Serializacion<List<TModelo>>.Deserializar(resultado);
                }
            }
            return lista;
        }

        public List<TModelo> Get(String id)
        {
            List<TModelo> lista;
            var cl = WebRequest.Create(UrlBase + id);
            cl.Credentials = new NetworkCredential();

            cl.Method = "GET";
            var res = cl.GetResponse();
            using (var stream = res.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    lista = Serializacion<List<TModelo>>.Deserializar(resultado);
                }
            }
            return lista;
        }

        //metodo = metodo de la clase que quieres utilixar para la busqueda
        //param = es la cadena que envias para que realice la busqueda 
        public List<TModelo> Get(string metodo, string param)
        {
            List<TModelo> lista;
            var cl = WebRequest.Create(UrlBase + "/" + metodo + "/" + param);
            cl.Method = "GET";
            var res = cl.GetResponse();
            using (var stream = res.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    lista = Serializacion<List<TModelo>>.Deserializar(resultado);
                }
            }
            return lista;
        }
        public TModelo GetLogin(string email, string password)
        {
            TModelo UsuarioUnico;
            var cl = WebRequest.Create(UrlBase + "?email=" + email + "&password=" + password);
            cl.Method = "GET";

            var res = cl.GetResponse();
            
            using (var stream = res.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    UsuarioUnico = Serializacion<TModelo>.Deserializar(resultado);
                }
            }

            return UsuarioUnico;
        }
        public Usuario GetUserEmail(string email)
        {
            Usuario usuarioNuevo;
            var cl = WebRequest.Create("http://webapiproyectos.azurewebsites.net/api/Usuario?email=" + email);
            cl.Method = "GET";
            var res = cl.GetResponse();
            using (var stream = res.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    usuarioNuevo = Serializacion<List<Usuario>>.Deserializar(resultado).FirstOrDefault();
                }
            }
            return usuarioNuevo;
        }
        public bool GetLoginBool(string email, string password)
        {
            TModelo UsuarioUnico;
            bool aux;
            //var cl = WebRequest.Create(UrlBase + "?email=" + email + "&password=" + password);
            var cl = WebRequest.Create("http://webapiproyectos.azurewebsites.net/api/Usuario"+ "?email=" + email + "&password=" + password);
            cl.Method = "GET";
            var res = cl.GetResponse();
            using (var stream = res.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    UsuarioUnico = Serializacion<TModelo>.Deserializar(resultado);
                }
            }
            if (UsuarioUnico != null)
            {
                aux =  true;
            }
            else
            {
                aux = false;
            }
            return aux;
        }

        public TModelo Get(int id)
        {
            TModelo lista;
            var cl = WebRequest.Create(UrlBase + "/" + id);
            cl.Method = "GET";
            var res = cl.GetResponse();
            using (var stream = res.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    lista = Serializacion<TModelo>.Deserializar(resultado);
                }
            }
            return lista;
        }


        public Tarea GetUsesrTarea(string id)
        {
            throw new NotImplementedException();
        }

        public Tarea GetUsesrTareaModel(TModelo model)
        {
            throw new NotImplementedException();
        }

        public List<TModelo> Get(Dictionary<string, string> parametros)
        {
            List<TModelo> lista;

            var query = "?";
            int n = 0;
            foreach (var parametro in parametros.Keys)
            {
                query += parametro + "=" + parametros[parametro];

                n++;
                if (n < parametros.Count)
                    query += "&";
            }



            var cl = WebRequest.Create(UrlBase + query);
            cl.Method = "GET";
            var res = cl.GetResponse();
            using (var stream = res.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    lista = Serializacion<List<TModelo>>.Deserializar(resultado);
                }
            }
            return lista;
        }
    }
}