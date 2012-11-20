using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace Zaza.Classes
{
  public class WebsiteStructure
  {
    public enum WebsiteModule
    {
      Default, Users, Supplier

    }
    public enum WebsitePage
    {
      Default, Users, SupplierList, SupplierEdit
    }

    #region Classes

    public class WebsiteModuleMetadata
    {
      public WebsiteModule WebsiteModule;
      public string Action;
      public string Controller;
      public string Title;
      public string Area;
      public Boolean RenderAsDropDown = false;
      public Boolean IsCurrentModule = false;

      public WebsiteModuleMetadata(WebsiteModule myModule, string title, string area, string controller, string action, Boolean renderAsDropDown = false)
      {
        this.WebsiteModule = myModule;
        this.Title = title;
        this.Action = action;
        this.Controller = controller;
        this.Area = area;
        this.RenderAsDropDown = renderAsDropDown;
      }

      //Me.WebsiteModule = myModule

    }

    public class WebsitePageMetadata
    {
      public WebsitePage WebsitePage;
      public string Action;
      public string Controller;
      public string Area;
      public string Title;
      public Boolean RenderSubmenu;
      public IEnumerable<WebsitePageMetadata> SubMenuMetadata;
      public RouteValueDictionary RouteValues;
      public object RouteValueSources;
      public Boolean MustRender = true;
      public string HighlightedMenuAction
      {
        get;
        set;
      }
      public string HighlightedMenuController
      {
        get;
        set;
      }

      public WebsitePageMetadata(WebsitePage page, string title, string area, string controller, string action, Boolean renderSubmenu = false, IEnumerable<WebsitePageMetadata> subMenuMetadata = null,
                                 object routeValueSources = null, string highlightedMenuController = null, string highlightedMenuAction = null)
      {
        this.WebsitePage = page;
        this.Action = action;
        this.Controller = controller;
        this.Area = area;
        this.Title = title;
        this.RenderSubmenu = renderSubmenu;
        this.SubMenuMetadata = subMenuMetadata;
        this.RouteValueSources = routeValueSources;
        this.HighlightedMenuController = highlightedMenuController;
        this.HighlightedMenuAction = highlightedMenuAction;

      }
    }


    public class WebsiteModulePages
    {
      public WebsiteModule WebsiteModule;

      public List<WebsitePage> WebsitePages = new List<WebsitePage>();
      public WebsiteModulePages(WebsiteModule myModule)
      {
        this.WebsiteModule = myModule;
      }
    }

    public class WebsiteModulePagesMetadata
    {
      public WebsiteModuleMetadata WebsiteModuleMetadata;

      public List<WebsitePageMetadata> WebsitePagesMetadata = new List<WebsitePageMetadata>();
      public WebsiteModulePagesMetadata(WebsiteModuleMetadata myModuleMetadata)
      {
        this.WebsiteModuleMetadata = myModuleMetadata;
      }
    }


    #endregion

    #region Methods
    private void SetIdentityAllowedPages(List<WebsitePage> allowedPages)
    {
      // clean the allowed pages
      if (ZazaIdentity.Current.AllowedModulePages != null)
      {
        ZazaIdentity.Current.AllowedModulePages.Clear();
      }



      List<WebsiteModulePages> websiteStructure = GetWebsiteStructure();
      List<WebsiteModuleMetadata> modulesMetadata = GetWebsiteModulesMetadata();
      List<WebsitePageMetadata> pagesMetadata = GetWebsitePagesMetadata();

      foreach (WebsiteModulePages modulePages in websiteStructure)
      {
        // get the allowed pages for the current module
        dynamic pages = modulePages.WebsitePages.Intersect(allowedPages).ToList();

        if (pages.Count > 0)
        {
          // find the module metadata
          WebsiteModulePagesMetadata moduleMetadata = null;
          foreach (WebsiteModuleMetadata myModuleMetadata in modulesMetadata)
          {
            if (myModuleMetadata.WebsiteModule == modulePages.WebsiteModule)
            {
              moduleMetadata = new WebsiteModulePagesMetadata(myModuleMetadata);

              // add the pages metadata
              foreach (WebsitePage allowedPage in pages)
              {
                foreach (WebsitePageMetadata myPageMetadata in pagesMetadata)
                {
                  if (allowedPage == myPageMetadata.WebsitePage)
                  {
                    moduleMetadata.WebsitePagesMetadata.Add(myPageMetadata);
                    break; // TODO: might not be correct. Was : Exit For
                  }
                }
              }

              break; // TODO: might not be correct. Was : Exit For
            }
          }

          // add the found allowed module and pages to the identity object
          if (ZazaIdentity.Current.AllowedModulePages != null)
            ZazaIdentity.Current.AllowedModulePages.Add(moduleMetadata);
        }
      }
    }



    #endregion

    #region Metadata and Structure
    public List<WebsiteModuleMetadata> GetWebsiteModulesMetadata()
    {
      var websiteModulesMetadata = new List<WebsiteModuleMetadata>();
      websiteModulesMetadata.Add(new WebsiteModuleMetadata(WebsiteModule.Users, "Users", "Admin", "Users", "List"));
      websiteModulesMetadata.Add(new WebsiteModuleMetadata(WebsiteModule.Supplier, "SupplierList", "Admin", "Supplier", "List"));
      return websiteModulesMetadata;
    }


    public List<WebsitePageMetadata> GetWebsitePagesMetadata()
    {
      var websitePagesMetadata = new List<WebsitePageMetadata>();

      websitePagesMetadata.Add(new WebsitePageMetadata(WebsitePage.Users, "List", "Admin", "Users", "List"));
      websitePagesMetadata.Add(new WebsitePageMetadata(WebsitePage.SupplierList, "List", "Admin", "Supplier", "List"));
      websitePagesMetadata.Add(new WebsitePageMetadata(WebsitePage.SupplierEdit, "Edit", "Admin", "Supplier", "Edit"));
      return websitePagesMetadata;
    }

    public void BuildAllowedPages()
    {
      var allowedPages = new List<WebsitePage>();
      allowedPages.Add(WebsitePage.Users);
      allowedPages.Add(WebsitePage.SupplierList);
      allowedPages.Add(WebsitePage.SupplierEdit);
      SetIdentityAllowedPages(allowedPages);
    }


    public void BuildAdminAllowedPages()
    {

      var allowedPages = new List<WebsitePage> { WebsitePage.Users, WebsitePage.SupplierList, WebsitePage.SupplierEdit};
      SetIdentityAllowedPages(allowedPages);
    }

    public void BuildSuperUserAllowedPages()
    {

      List<WebsitePage> allowedPages = new List<WebsitePage>();

      // create a subset of the website structure containing only allowed pages
      allowedPages.Add(WebsitePage.Users);
      allowedPages.Add(WebsitePage.SupplierList);
      allowedPages.Add(WebsitePage.SupplierEdit);
      SetIdentityAllowedPages(allowedPages);
    }

    public List<WebsiteModulePages> GetWebsiteStructure()
    {
      var websiteStructure = new List<WebsiteModulePages>();
      WebsiteModulePages modulePages = null;


      modulePages = new WebsiteModulePages(WebsiteModule.Users);
      modulePages.WebsitePages.Add(WebsitePage.Users);
      websiteStructure.Add(modulePages);


      modulePages = new WebsiteModulePages(WebsiteModule.Supplier);
      modulePages.WebsitePages.Add(WebsitePage.SupplierList);
      modulePages.WebsitePages.Add(WebsitePage.SupplierEdit);
      websiteStructure.Add(modulePages);
      return websiteStructure;
    }



    #endregion

  }
}