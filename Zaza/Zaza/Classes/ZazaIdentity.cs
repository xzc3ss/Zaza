using System;
using System.Collections.Generic;
using System.Web;
using Zaza.Classes;
using Zaza.Entities;
using User = Zaza.Entities.User;

namespace Zaza
{
  public class ZazaIdentity
  {
    public List<WebsiteStructure.WebsiteModulePagesMetadata> AllowedModulePages = new List<WebsiteStructure.WebsiteModulePagesMetadata>();

    public User CurrentUser
    {
      get;
      set;
    }

    public Role Role
    {
      get;
      set;
    }
    public Boolean IsSuperUser { get; set; }

    private string _language;
    public string Language { get; set; }

    public System.Web.SessionState.HttpSessionState Session
    {
      get
      {
        System.Web.SessionState.HttpSessionState mySession = null;
        if (System.Web.HttpContext.Current != null)
          mySession = System.Web.HttpContext.Current.Session;

        return mySession;
      }
    }

    public static ZazaIdentity Current
    {
      get
      {
        if (Core.Session["Identity"] == null)
        {
          Core.Session["Identity"] = new ZazaIdentity();
          //ZazaIdentity.Current.AllowedModulePages = new List<WebsiteStructure.WebsiteModulePagesMetadata>();
        }
        return (ZazaIdentity)Core.Session["Identity"];
      }
      set { Core.Session["Identity"] = value; }
    }


  }
}
