using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zaza.Entities;
namespace Zaza.Classes.Managers
{
  public static class SuppliersManager
  {
    public static Supplier CreateNewSupplier(Supplier newSupplier)
    {

      var createdSupplier = new Supplier();
      createdSupplier = newSupplier;

      return createdSupplier;
    }

    public static Supplier GetSupplierById(int? id)
    {
      var exists = false;
      var query = (from u in Core.DataContext.Suppliers where u.ID == id select u).FirstOrDefault();
      return query;
    }
  }
}