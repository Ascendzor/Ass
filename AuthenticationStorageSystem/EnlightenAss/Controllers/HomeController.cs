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
            ViewBag.Message = "LELELELE";
            return View();
        }

        //checks if a given string exists within each of the clients names.
        //The given string and clients names are set to lowercase to make the contains() case-Insensitive
        //returns a list of clients who contained the given string
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
                if (item.Name.ToLower().Contains(searchText))
                {
                    projectResults.Add(item);
                }
            }
            ViewData["projectResults"] = projectResults;

            List<Entry> entryResults = new List<Entry>();
            foreach (Entry item in db.Entries)
            {
                if (item.Username.ToLower().Contains(searchText))
                {
                    entryResults.Add(item);
                }
            }
            ViewData["entryResults"] = entryResults;

            return PartialView("PartialViewResults");
        }

    }
}
