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

        [HttpPost]
        public ActionResult Search(String searchText)
        {
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

            return View();
        }
    }
}
