using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Prueba5.Models;
using System.Data.Entity;

namespace Prueba5
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Sample URL: /Search
            routes.MapRoute(
                "Search", "Busqueda/{query}", new { controller = "Home", action = "Resultado" }
                );

            routes.MapRoute(
               "Confirmacion", "Account/Confirmar/{parametroValidador}", new { controller = "Account", action = "Confirmar" }
               );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
          


        }

        protected void Application_Start()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TranSapoContext>());
            Backup.XMLBackup.Load();
            //Backup.XMLBackup.Backup();
            
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }


    }
}