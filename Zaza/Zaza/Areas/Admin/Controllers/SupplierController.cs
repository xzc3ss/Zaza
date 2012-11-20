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
  public class SupplierController : ZazaController
  {
    //
    // GET: /Admin/Supplier/

    public ActionResult List(int? page, string sort, string sortDir, string firstLetter)
    {
      CurrentPageAction = WebsiteStructure.WebsitePage.SupplierList;


      var query = (from i in Core.DataContext.Suppliers select i).ToList();

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
      var supplierToEdit = new Supplier();
      if (id.HasValue)
      {
        supplierToEdit = (from i in Core.DataContext.Suppliers where i.ID == id.Value select i).SingleOrDefault();
               

      }
      return View(supplierToEdit);

    }

    [HttpPost]
    public ActionResult Edit(Supplier supplier)
    {
      if (ModelState.IsValid)
      {
        if (supplier.ID > 0)
        {
          Supplier supplierToUpdate = SuppliersManager.GetSupplierById(supplier.ID);
          if (supplierToUpdate != null)
          {
            TryUpdateModel(supplierToUpdate);
          }

        }
        else
        {
          Core.DataContext.Suppliers.Add(SuppliersManager.CreateNewSupplier(supplier));
        }
        Core.DataContext.SaveChanges();

        return RedirectToAction("List");
      }

      return View();
    }
  }
}
