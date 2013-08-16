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
    public class SearchController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        //
        // GET: /Search/

        public ActionResult Index(int id = 0)
        {
            return View(RelevantClients());
        }
        //
        // GET: /Search/
        
        public List<Client> RelevantClients()
        {
          
            List<Client> relevantClients = new List<Client>();
            string searchString = "asd";
            foreach (Client item in db.Clients)
            {
                if (item.Name.Contains(searchString))
                {
                    relevantClients.Add(item);

                    ViewBag.test = item.ToString();
                }
            }
            return relevantClients;
        }

    }
}
