﻿using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation.Mvc;

namespace UNiDAYSHomework
{
    public class MvcApplication : System.Web.HttpApplication
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name 
                "{controller}/{action}/{id}",                           // URL with parameters 
                new { controller = "AddUser", action = "Create", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            FluentValidationModelValidatorProvider.Configure();
        }
    }
}
