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
        public ActionResult Index(String type = "", int id = 0)
        {
            
            if (type == "Entry")
            {
                ViewBag.Type = "test";
                ViewBag.Id = "3";
                return View("../Home/Index");
                //return RedirectToAction("EntryDisplay", "Entry", new { id = id });
            }
            else if (type == "Project")
            {

            }
            else if (type == "Client")
            {

            }
            

            return View();
        }
        
    }
}
