using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zaza.Classes;
using Zaza.Entities;
namespace Zaza.Areas.Admin.Controllers
{
  public class SupplierController : ZazaController
  {
    //
    // GET: /Admin/Supplier/

    public ActionResult List()
    {
      return View();
    }

    public ActionResult Edit(int? id)
    {
      return View();
    }
    [HttpPost]
    public ActionResult Edit(Supplier supplier)
    {
      return View();
    }
  }
}
