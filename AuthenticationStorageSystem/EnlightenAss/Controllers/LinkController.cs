using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssClassLibrary;

namespace EnlightenAss.Controllers
{
    public class LinkController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        //
        // GET: /Search/
        public ActionResult Index(string url = "", int id = 0)
        {
            return View();
        }
    }
}
