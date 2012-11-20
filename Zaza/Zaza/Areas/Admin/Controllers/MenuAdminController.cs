using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Zaza.Classes;

namespace Zaza.Areas.Admin.Controllers
{
  public class MenuAdminController : ZazaController
  {

    [ChildActionOnly()]
    public ActionResult MainAdminMenu()
    {
      List<WebsiteStructure.WebsiteModuleMetadata> modules = new List<WebsiteStructure.WebsiteModuleMetadata>();
      dynamic route = this.ControllerContext.ParentActionViewContext.RouteData.Values;
      string action = Convert.ToString(route["action"]).ToLower();
      string controller = Convert.ToString(route["controller"]).ToLower();
      bool activeModuleFound = false;
      WebsiteStructure.WebsiteModule? currentModule = null;
      WebsiteStructure site = new WebsiteStructure();


      if (User.IsInRole("Superuser"))
      {
        site.BuildSuperUserAllowedPages();
      }
      else
      {
        site.BuildAdminAllowedPages();
      }

      // create a copy of the allowed modules
      foreach (var item in ZazaIdentity.Current.AllowedModulePages)
      {
        var metadata = item.WebsiteModuleMetadata;
        var stuff = item.WebsitePagesMetadata;
        modules.Add(new WebsiteStructure.WebsiteModuleMetadata(metadata.WebsiteModule, metadata.Title, metadata.Area,
                                                               metadata.Controller, metadata.Action,
                                                               metadata.RenderAsDropDown));
      }

      // find the current page to set the active main menu
      List<WebsiteStructure.WebsiteModulePages> websiteStructure1 = (new WebsiteStructure()).GetWebsiteStructure();
      WebsiteStructure.WebsitePage currentPage =
        ((ZazaController)this.ControllerContext.ParentActionViewContext.Controller).CurrentPageAction;

      foreach (WebsiteStructure.WebsiteModulePages siteModule in websiteStructure1)
      {
        foreach (var sitePage in siteModule.WebsitePages)
        {
          if (currentPage == sitePage)
          {
            currentModule = siteModule.WebsiteModule;
            activeModuleFound = true;
            break;
          }
        }

        if (activeModuleFound)
          break;
      }

      foreach (var item in modules)
      {
        if (item.WebsiteModule == currentModule)
          item.IsCurrentModule = true;
      }

      return PartialView(modules);
    }

    [ChildActionOnly()]
    public ActionResult SubMenu(string currentModule = "")
    {
      IEnumerable<WebsiteStructure.WebsitePageMetadata> pages = null;
      dynamic route = this.ControllerContext.ParentActionViewContext.RouteData.Values;
      if (((Route)this.ControllerContext.ParentActionViewContext.RouteData.Route).DataTokens["area"] == null)
      {
        return PartialView(null);
      }
      string area = Convert.ToString(((Route)this.ControllerContext.ParentActionViewContext.RouteData.Route).DataTokens["area"]).ToLower();
      string action = Convert.ToString(route["action"]).ToLower();
      string controller = Convert.ToString(route["controller"]).ToLower();

      // Find the current page
      ViewData["HighlightedMenuAction"] = action;
      ViewData["HighlightedMenuController"] = controller;
      ViewData["currentAction"] = action;

      foreach (var modulePages in ZazaIdentity.Current.AllowedModulePages)
      {
        if (modulePages.WebsiteModuleMetadata.Title == currentModule)
        {
          foreach (var pageMetadata in modulePages.WebsitePagesMetadata)
          {
            if (pageMetadata.RenderSubmenu)
            {
              if (pageMetadata.SubMenuMetadata == null)
              {
                pages = modulePages.WebsitePagesMetadata;
                if (pages.Count() == 1)
                {
                  pages = null;
                }
                else
                {
                  foreach (var page in pages)
                  {
                    if (page.RouteValues == null)
                      page.RouteValues = new RouteValueDictionary();
                  }
                }
              }
              else
              {
                foreach (var subMenuItem in pageMetadata.SubMenuMetadata)
                {
                  subMenuItem.MustRender = true;
                  subMenuItem.RouteValues = new RouteValueDictionary();
                  object cc = subMenuItem.Action;
                  // biuld the route values
                  if (subMenuItem.RouteValueSources != null)
                  {
                    // get the properties of the subMenuItem.RouteValueSources dynamic object
                    dynamic properties = subMenuItem.RouteValueSources.GetType().GetProperties();

                    foreach (System.Reflection.PropertyInfo propertyInfo in properties)
                    {
                      // extract each property name and value
                      string routeKey = propertyInfo.Name;
                      dynamic routeSourceKey = propertyInfo.GetValue(subMenuItem.RouteValueSources, null);

                      string value = null;

                      if (route.ContainsKey(routeSourceKey))
                      {
                        value = route(routeSourceKey).ToString();
                      }
                      else
                      {
                        if (Request.QueryString[routeSourceKey] != null)
                          value = Request.QueryString[routeSourceKey];
                      }

                      if (!string.IsNullOrEmpty(value))
                      {
                        subMenuItem.RouteValues.Add(routeKey, value);
                      }
                      else
                      {
                        if (!subMenuItem.Controller.ToLower().Equals(controller) ||
                            !subMenuItem.Action.ToLower().Equals(action))
                        {
                          subMenuItem.MustRender = false;
                        }
                      }
                    }
                  }
                }

                var currentAllowedAction = modulePages.WebsitePagesMetadata.Select(wpm => wpm.Action).ToList();
                pages = pageMetadata.SubMenuMetadata.Where(sbm => currentAllowedAction.Contains(sbm.Action)).ToList();

              }
            }
          }
        }
      }

      return PartialView(pages);
    }

  }
}


