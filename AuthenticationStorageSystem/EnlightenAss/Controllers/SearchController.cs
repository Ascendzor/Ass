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
            ViewBag.TESTING = "LOLOOLOOLLDLDLD";
            return View();
        }
        //
        
        /**
         * Search whole database for substring of 'searchText' parameter
         * Return partial view containing the results
         */
        public ActionResult Results(String searchText)
        {
            if (searchText == null) searchText = "";
            ViewBag.Numbera = searchText;
            List<Client> clientResults = new List<Client>();
            foreach (Client item in db.Clients)
            {
                if (item.Name.Contains(searchText))
                {
                    clientResults.Add(item);
                }
            }
            ViewData["clientResults"] = clientResults;

            List<Project> projectResults = new List<Project>();
            foreach (Project item in db.Projects)
            {
                if (item.Name.Contains(searchText))
                {
                    projectResults.Add(item);
                }
            }
            ViewData["projectResults"] = projectResults;

            List<Entry> entryResults = new List<Entry>();
            foreach (Entry item in db.Entries)
            {
                if (item.Username.Contains(searchText))
                {
                    entryResults.Add(item);
                }
            }
            ViewData["entryResults"] = entryResults;

            return PartialView("PartialViewResults");
            
        }
    }
}
