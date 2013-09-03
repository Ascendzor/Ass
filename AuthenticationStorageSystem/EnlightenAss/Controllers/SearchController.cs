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
        public ActionResult Results(string searchText)
        {
            searchText.ToLower();
            List<Client> clientResults = new List<Client>();
            foreach (Client item in db.Clients)
            {
                if (item.Name.Contains(searchText))
                {
                    clientResults.Add(item);
                }
            }            
            ViewData["clientResults"] = clientResults;

            return PartialView("PartialViewResults");
        }
    }
}
