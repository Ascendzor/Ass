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
            return View();
        }
        //
        
        /**
         * Search whole database for substring of 'searchText' parameter
         * Includes keywords such as 'client' to retrieve all client data
         * Return partial view containing the results
         */
        public ActionResult Results(String searchText)
        {
            // If empty text box get EVERYTHING
            if (searchText == null) searchText = "";

            searchText.ToLower();


            /* Search Client table */

            //if input client or clients then get all clients
            List<Client> clientResults = new List<Client>();
            if (searchText == "clients" || searchText == "client")
            {
                foreach (Client item in db.Clients)
                {
                    clientResults.Add(item);
                }
            }
            else
            {
                //get all data in Client table with substring of input
                foreach (Client item in db.Clients)
                {
                    if (item.Name.Contains(searchText))
                    {
                        clientResults.Add(item);
                    }
                }
            }
            ViewData["clientResults"] = clientResults;
            

            /* Search Project table */

            //if input project or projects then get all project
            List<Project> projectResults = new List<Project>();
            if (searchText == "projects" || searchText == "project")
            {
                foreach (Project item in db.Projects)
                {
                    projectResults.Add(item);
                }
            }
            else
            {
                //get all data in Project table with substring of input
                foreach (Project item in db.Projects)
                {
                    if (item.Name.Contains(searchText))
                    {
                        projectResults.Add(item);
                    }
                }
            }
            ViewData["projectResults"] = projectResults;
            

            /* Search Entry table */

            //if input entry or entries then get all entries
            List<Entry> entryResults = new List<Entry>();
            if (searchText == "entry" || searchText == "entries")
            {
                foreach (Entry item in db.Entries)
                {
                    entryResults.Add(item);
                }
            }
            else
            {
                //get all data in Entry table with substring of input
                foreach (Entry item in db.Entries)
                {
                    if (item.Username.Contains(searchText))
                    {
                        entryResults.Add(item);
                    }
                }
            }
            ViewData["entryResults"] = entryResults;

            return PartialView("PartialViewResults");
        }
    }
}
