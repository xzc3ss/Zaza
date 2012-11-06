using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zaza.Classes;
using WebMatrix.WebData;
using Zaza.Classes;
using Zaza.Models;
using System.Security.Principal;
namespace Zaza.Areas.Admin.Controllers
{

  public class DefaultController : ZazaController
  {
    //
    // GET: /Admin/Home/

    public ActionResult Index()
    {
      return View();
    }

  }
}
