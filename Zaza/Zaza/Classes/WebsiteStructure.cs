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
      Default, Users

    }
    public enum WebsitePage
    {
      Default, Users
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
        this.Action = action;
        this.Controller = title;
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
      public new List<WebsitePageMetadata> WebsitePagesMetadata;
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
      ZazaIdentity.Current.AllowedModulePages.Clear();

      List<WebsiteModulePages> websiteStructure = GetWebsiteStructure();
      List<WebsiteModuleMetadata> modulesMetadata = GetWebsiteModulesMetadata();
      List<WebsitePageMetadata> pagesMetadata = GetWebsitePagesMetadata();

      foreach (WebsiteModulePages modulePages in websiteStructure)
      {
        // get the allowed pages for the current module
        var pages = modulePages.WebsitePages.Intersect(allowedPages).ToList();

        if (pages.Count > 0)
        {
          // find the module metadata
          WebsiteModulePagesMetadata moduleMetadata = null;
          foreach (var myModuleMetadata in modulesMetadata)
          {
            if (myModuleMetadata.WebsiteModule == modulePages.WebsiteModule)
            {
              moduleMetadata = new WebsiteModulePagesMetadata(myModuleMetadata);

              // add the pages metadata
              foreach (var allowedPage in pages)
              {
                foreach (var myPageMetadata in pagesMetadata)
                {
                  if (allowedPage == myPageMetadata.WebsitePage)
                  {
                    moduleMetadata.WebsitePagesMetadata.Add(myPageMetadata);

                  }
                }
              }

            }
          }

          // add the found allowed module and pages to the identity object
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
      return websiteModulesMetadata;
    }


    public List<WebsitePageMetadata> GetWebsitePagesMetadata()
    {
      List<WebsitePageMetadata> websitePagesMetadata = new List<WebsitePageMetadata>();
      websitePagesMetadata.Add(new WebsitePageMetadata(WebsitePage.Users, "Home", "", "Home", "Index"));
      return websitePagesMetadata;
    }

    public void BuildAllowedPages()
    {
      List<WebsitePage> allowedPages = new List<WebsitePage>();
      // create a subset of the website structure containing only allowed pages
      allowedPages.Add(WebsitePage.Users);
      // set the allowed pages with metadata to the current identity
      SetIdentityAllowedPages(allowedPages);
    }


    public void BuildAdminAllowedPages()
    {

      List<WebsitePage> allowedPages = new List<WebsitePage>();
      allowedPages.Add(WebsitePage.Users);
    }

    public void BuildSuperuserAllowedPages()
    {

      List<WebsitePage> allowedPages = new List<WebsitePage>();

      // create a subset of the website structure containing only allowed pages
      allowedPages.Add(WebsitePage.Users);
    }

    public List<WebsiteModulePages> GetWebsiteStructure()
    {
      List<WebsiteModulePages> websiteStructure = new List<WebsiteModulePages>();
      WebsiteModulePages modulePages = default(WebsiteModulePages);
      modulePages = new WebsiteModulePages(WebsiteModule.Users);
      modulePages.WebsitePages.Add(WebsitePage.Users);
      websiteStructure.Add(modulePages);
      return websiteStructure;
    }



    #endregion

  }
}