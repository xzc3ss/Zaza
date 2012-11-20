using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zaza.Classes;

namespace Zaza.Areas.Admin.Controllers
{
    public class ProductController : ZazaController
    {
        //
        // GET: /Admin/Product/

        public ActionResult Index()
        {
            return View();
        }

    }
}
