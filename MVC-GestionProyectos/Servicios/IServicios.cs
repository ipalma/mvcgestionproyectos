using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using MVC_GestionProyectos.Models;

namespace MVC_GestionProyectos.Servicios
{
    public interface IServicios<TModelo>
    {
        Task Add(TModelo modelo);
        Task Update(int id, TModelo modelo);
        Task Delete(int id);
        Task DeleteFunct(int idUsu, int idProy);
        int GetMax();
        List<TModelo> Get();
        List<TModelo> Get(String paramUrl);
        List<TModelo> Get(String metodo, String param);
        TModelo GetLogin(String email, String password);
        bool GetLoginBool(string email, string password);

        Usuario GetUserEmail(string email);
        TModelo Get(int id);
        Tarea GetUsesrTarea(String id);
        Tarea GetUsesrTareaModel(TModelo model);
        List<TModelo> Get(Dictionary<String, String> parametros );
    }
}