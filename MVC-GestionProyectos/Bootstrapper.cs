using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MVC_GestionProyectos.Controllers;
using MVC_GestionProyectos.Models;
using MVC_GestionProyectos.Servicios;
using MVC_GestionProyectos.Servicios.MiServicios;
using Unity.Mvc4;

namespace MVC_GestionProyectos
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        container.RegisterType<IServicios<Usuario>, ServicioUsuario>();
        container.RegisterType<IServicios<Tarea>, ServicioTarea>();
        container.RegisterType<IServicios<LoginUsuario>, ServicioLoginUsuario>();
        container.RegisterType<IServicios<Proyecto>, ServicioProyecto>();
        container.RegisterType<IServicios<Proyecto_Tarea_Grupo>, ServicioListadoProyectos>();
        container.RegisterType<IServicios<Prioridad>, ServicioPrioridad>();
        container.RegisterType<IServicios<EstadoTarea>, ServicioEstadoTarea>();
        container.RegisterType<IServicios<Habilidad>, ServicioHabilidad>();
        container.RegisterType<IServicios<Grupo>, ServicioGrupos>();
        container.RegisterType<IServicios<EstadoProyecto>, ServicioEstadoProyecto>();
        container.RegisterType<IServicios<Mensaje>, ServicioMensaje>();
    }
  }
}