using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zaza.Classes;
using Zaza.Entities;
using Zaza.Classes.Managers;
namespace Zaza.Areas.Admin.Controllers
{

  public class UsersController : ZazaController
  {
    //
    // GET: /Admin/Users/

    public ActionResult Index()
    {

      return RedirectToAction("List");
    }

    public ActionResult List(Int32 compid = -1, int? page = 1, String sort = "Description", String sortdir = "ASC", bool showall = false)
    {
      CurrentPageAction = WebsiteStructure.WebsitePage.Users;
      if (string.IsNullOrEmpty(sort))
      {
        sort = "AddedDate";
        sortdir = "Desc";
        if (HttpContext.Request.Cookies.AllKeys.Contains("UsersSortColumn") && HttpContext.Request.Cookies.AllKeys.Contains("UsersSortDir"))
        {
          var cookie = HttpContext.Request.Cookies["UsersSortColumn"];
          if (!String.IsNullOrEmpty(cookie.Value))
          {
            sort = cookie.Value;
            cookie = HttpContext.Request.Cookies["UsersSortDir"];
            if (cookie != null) sortdir = cookie.Value;
          }
        }
      }


      //var datacontext = new Zaza.Entities.ZazaEntities();
      //var query = (from i in datacontext.Users
      //             select i).ToList();
      //  Dim query = From c In dc.Customers Where Not c.Deleted AndAlso _
      //              c.ProviderID IsNot Nothing AndAlso _
      //var query= FormMethod
      //translate columnHeaders
      //ViewData["Title"] = GenerateTitleFromBreadcrumb(Breadcrumb);


      // Dim pageNumber As Integer = 1
      //  If page.HasValue Then pageNumber = page
      //  ' translate column headers
      //  Dim columnHeaders As New Dictionary(Of String, String)
      //  columnHeaders.Add("Name", HttpContext.GetGlobalResourceObject("Common", "Name"))
      //  columnHeaders.Add("ExportID", HttpContext.GetGlobalResourceObject("Common", "ExportID"))
      //  columnHeaders.Add("Email", HttpContext.GetGlobalResourceObject("Common", "Email"))
      //  columnHeaders.Add("AddedDate", HttpContext.GetGlobalResourceObject("Common", "AddedDate"))
      //  ' other view data


      //  ViewData("ColumnHeaders") = columnHeaders
      //  ViewData("Title") = GenerateTitleFromBreadcrumb(Breadcrumb)

      //  GetCustomersQueryString = Request.Url.ToString
      //  ViewData("customersBack") = GetCustomersQueryString
      //  Return View(query.ToList)
      //var datacontext = new Zaza.Entities.ZazaEntities();

      var query = (from i in Core.DataContext.Users
                   select i).ToList();
      int pageNumber = 1;
      int totalContacts = query.Count;
      if (page.HasValue)
      {
        pageNumber = page.Value;
        ViewData["PagerData"] = PagerData.BuildPagerData(pageNumber, totalContacts, "pager-goto", "Page", showGoTo: true);
      }

      return View(query.ToList());
    }


    public ActionResult Edit(int? id)
    {
      var userToEdit = new User();
      if (id.HasValue)
      {
        userToEdit = (from i in Core.DataContext.Users where i.ID == id.Value select i).SingleOrDefault();

      }
      return View(userToEdit);

    }

    [HttpPost]
    public ActionResult Edit(User user)
    {
      if (ModelState.IsValid)
      {
        if (user.ID > 0)
        {
          User userToUpdate = UsersManager.GetUserByEmail(user.Email);
          if (userToUpdate != null)
          {
            TryUpdateModel(userToUpdate);
          }

        }
        else
        {
          Core.DataContext.Users.Add(UsersManager.CreateNewUser(user));
        }
        Core.DataContext.SaveChanges();

        return RedirectToAction("List");
      }

      return View();

    }

    //  ViewData("ColumnHeaders") = columnHeaders
    //  ViewData("Title") = GenerateTitleFromBreadcrumb(Breadcrumb)

    //  GetCustomersQueryString = Request.Url.ToString
    //  ViewData("customersBack") = GetCustomersQueryString
    //  Return View(query.ToList)
    //End Function
  }
}
