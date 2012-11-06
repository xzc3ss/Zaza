using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Zaza
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
     name: "Default",
     url: "{controller}/{action}/{id}",
     defaults: new
     {
       controller = "Home",
       action = "Index",
       id = UrlParameter.Optional
     }, namespaces: new[] { "Zaza.Controllers" }
 );
      //routes.MapRoute(
      //    name: "Default",
      //    url: "{controller}/{action}/{id}",
      //    defaults: new
      //    {
      //      controller = "Home",
      //      action = "Index",
      //      id = UrlParameter.Optional
      //    },
      //    constraints: null,
      //    namespaces: new[] { "ZAza.NewAutoccasion.FrontEnd" });
      //       routes.MapRoute("HomeRoute", "{controller}/{action}", _
      //                New With {.controller = "Home", .action = "Index"},
      //                New With {.controller = New IncludeValuesConstraint(New String() {"Home"})})

      //routes.MapTranslatedRoute("TranslatedRoute", "{controller}/{action}/{id}",
      //                          New With {.controller = "Default", .action = "Index", .id = UrlParameter.Optional},
      //                          New With {.controller = translationProvider, .action = translationProvider}, False)
    }
  }
}