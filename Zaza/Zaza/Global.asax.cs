using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Zaza
{
  // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
  // visit http://go.microsoft.com/?LinkId=9394801

  public class MvcApplication : System.Web.HttpApplication
  {

    #region " WebPageLifecyle events"

    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();

      WebApiConfig.Register(GlobalConfiguration.Configuration);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
      //ModelMetadataProviders.Current = new MetadataProvider();
      AuthConfig.RegisterAuth();
    }

    public void Application_AuthenticateRequest(object sender, EventArgs e)
    {
      if (Request.IsAuthenticated)
      {
        // Create an array of role names
        var roleList = new ArrayList();
        if (User.Identity.Name == "superuser")
        {
          roleList.Add("Superuser");

        }


        //Convert the roleList ArrayList to a String array
        var roleListArray = (string[]) roleList.ToArray(typeof (string));

        //Add the roles to the User Principal
        HttpContext.Current.User = new GenericPrincipal(User.Identity, roleListArray);

      }
    }

    public void Application_BeginRequest(object sender, EventArgs e)
    {
      string sessionParamName = "ASPSESSID";
      try
      {
        if ((HttpContext.Current.Request.Form[sessionParamName]) != null)
        {
          string sessionCookieName = "ASP.NET_SESSIONID";
        }
      }
      catch (Exception)
      {

        throw;
      }
      string authParamName = "AUTHID";
      try
      {
        if ((HttpContext.Current.Request.Form[authParamName])!=null)
        {
          string authCookieName = FormsAuthentication.FormsCookieName;
          UpdateCookie(authCookieName, HttpContext.Current.Request.Form[authParamName]);
        }
      }
      catch (Exception)
      {

        throw;
      }

    }

    #endregion

    #region " Additional methods"

    public void UpdateCookie(string cookieName, string cookieValue)
    {
      var cookie = HttpContext.Current.Request.Cookies.Get(cookieName);
      if (cookie == null)
      {
        cookie = new HttpCookie(cookieName);
        HttpContext.Current.Request.Cookies.Add(cookie);
      }
      cookie.Value = cookieValue;
      HttpContext.Current.Request.Cookies.Set(cookie);
    }

    #endregion


  }
}