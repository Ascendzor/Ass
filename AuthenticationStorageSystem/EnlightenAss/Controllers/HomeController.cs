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

            return PartialView("PartialViewResults");
        }

    }
}
