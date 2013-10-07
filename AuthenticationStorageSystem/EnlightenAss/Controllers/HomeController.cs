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
         * Returns a list of all clients/projects/entries WHERE Name contains @param=searchText
         * If clients/projects/entries Name contains @param=searchText their children are added to the list
         * The given string and clients names are set to lowercase to make the contains() case-Insensitive
         * Matches are not added to the list IF the client/project/entry OR their parent is archived
         * Lists are trimmed to size 'const int numberOfResults'
         */
        public ActionResult Search(string searchText)
        {
            const int numberOfResults = 6;
            searchText = searchText.ToLower();
            
            /* Client */
            List<Client> clientResults = new List<Client>();
            foreach (Client item in db.Clients)
            {
                if (item.Name.ToLower().Contains(searchText))
                {
                    if (!item.isArchived) clientResults.Add(item);
                }
            }
            //trim results down to 6
            if (clientResults.Count > 6)
                clientResults.RemoveRange(numberOfResults - 1, clientResults.Count - numberOfResults);
            ViewData["clientResults"] = clientResults;

            /* Project */
            List<Project> projectResults = new List<Project>();
            foreach (Project item in db.Projects)
            {
                if (item.Name.ToLower().Contains(searchText) || item.Client.Name.ToLower().Contains(searchText))
                {
                    if (!item.isArchived && !item.Client.isArchived) projectResults.Add(item);
                }
            }
            //trim results down to 6
            if (projectResults.Count > 6)
                projectResults.RemoveRange(numberOfResults - 1, projectResults.Count - numberOfResults);
            ViewData["projectResults"] = projectResults;

            /* Entry */
            List<Entry> entryResults = new List<Entry>();
            foreach (Entry item in db.Entries)
            {
                if (item.Username.ToLower().Contains(searchText) || item.Project.Name.ToLower().Contains(searchText))
                {
                    if (!item.isArchived && !item.Project.isArchived && !item.Project.Client.isArchived) entryResults.Add(item);
                }
            }
            //trim results down to 6
            if (entryResults.Count > 6)
                entryResults.RemoveRange(numberOfResults-1, entryResults.Count - numberOfResults);
            ViewData["entryResults"] = entryResults;

            if (clientResults.Count == 0 && projectResults.Count == 0 && entryResults.Count == 0)
                return PartialView("NoResults");
            return PartialView("SearchResults");
        }

    }
}
