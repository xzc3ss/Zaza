using System.Web;
using Zaza.Entities;

namespace Zaza
{
  public static class Core
  {

    #region Properties


    #endregion

    public static HttpContext GlobalHttpContext { get; set; }

    public static System.Web.SessionState.HttpSessionState Session
    {
      get
      {

        System.Web.SessionState.HttpSessionState mySession = null;
        if (System.Web.HttpContext.Current != null)
        {
          mySession = System.Web.HttpContext.Current.Session;
        }
        return mySession;
      }
    }

    public static System.Web.Caching.Cache Cache
    {
      get
      {
        return System.Web.HttpContext.Current.Cache;
      }
    }

    public static System.Web.HttpApplicationState Application
    {
      get
      {
        return System.Web.HttpContext.Current.Application;
      }
    }

    public static System.Web.HttpServerUtility Server
    {
      get
      {
        return System.Web.HttpContext.Current.Server;
      }
    }

    public static ZazaEntities DataContext
    {
      get
      {
        if (HttpContext.Current.Session["DataContext"] != null)
        {
          return HttpContext.Current.Session["DataContext"] as ZazaEntities;
        }
        else
        {
          InitializeDataContext();
          return HttpContext.Current.Session["DataContext"] as ZazaEntities;
        }
      }
      set
      {
        HttpContext.Current.Session["DataContext"] = value;
      }
    }

    ////	Public Property LoginAttempts() As Integer
    ////		Get
    ////			Return Session("LoginAttempts")
    ////		End Get
    ////		Set(ByVal value As Integer)
    ////			Session("LoginAttempts") = value
    ////		End Set
    ////	End Property

    //#region SEO

    //		public List<RouteValueTranslation> GetPagesAliases()
    //		{
    //		}

    //		#endregion


    //#Region " SEO "

    //	Public Function GetPagesAliases() As List(Of RouteValueTranslation)
    //		Dim dataContext As New AutoccasionEntities

    //		Dim pages = (From a In dataContext.WebSiteLocalizations Where a.ClassKey = "Alias").ToList
    //		Dim translations As New List(Of RouteValueTranslation)

    //		For Each pageAlias In pages
    //			translations.Add(New RouteValueTranslation(CultureInfo.GetCultureInfo(pageAlias.CultureCode), pageAlias.ResourceKey, pageAlias.ResourceValue))
    //		Next

    //		Return translations
    //	End Function

    //#End Region

    #region "Methods"

    public static void InitializeDataContext()
    {
      //Dim dc As New AutoDialogEntities(System.Configuration.ConfigurationManager.ConnectionStrings("AutoDialogEntities").ConnectionString)
      var dc = new ZazaEntities();
      Session["DataContext"] = dc;
    }
    #endregion


  }
}
