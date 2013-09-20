using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssClassLibrary;

namespace EnlightenAss.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View();
        }

        /**
         * Checks if a given string exists within each of the client/project/entry names.
         * If searchText matches a client or project name, return all projects or entries for that client/project
         * The given string and clients names are set to lowercase to make the contains() case-Insensitive
         * returns a list of clients who contained the given string
         */
        public ActionResult Search(string searchText)
        {
            searchText = searchText.ToLower();

            List<Client> clientResults = new List<Client>();
            foreach (Client item in db.Clients)
            {
                if (item.Name.ToLower().Contains(searchText))
                {
                    clientResults.Add(item);
                }
            }
            ViewData["clientResults"] = clientResults;

            List<Project> projectResults = new List<Project>();
            foreach (Project item in db.Projects)
            {
                if (item.Name.ToLower().Contains(searchText) || item.Client.Name.ToLower() == searchText)
                {
                    projectResults.Add(item);
                }
            }
            ViewData["projectResults"] = projectResults;

            List<Entry> entryResults = new List<Entry>();
            foreach (Entry item in db.Entries)
            {
                if (item.Username.ToLower().Contains(searchText) || item.Project.Name.ToLower() == searchText)
                {
                    entryResults.Add(item);
                }
            }
            ViewData["entryResults"] = entryResults;

            return PartialView("SearchResults");
        }

    }
}
